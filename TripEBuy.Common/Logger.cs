using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace TripEBuy.Common
{
    public enum LogType
    {
        Error,
        Performance,
        Trace
    }

    public enum LogLevel
    {
        ERR,
        PER,
        TRA
    }

    public enum EntryStatus
    {
        ENTRY,
        EXIT,
        PROCESS
    }

    public class Logger
    {
        private static Mutex _mu = new Mutex();
        private static Logger _logger;
        private static string _LogFileName;
        private static string _LogPath;
        private static bool _IsEnabled;
        private static bool _IsBuffered;
        private static int _MaxBufferCount;
        private static bool _IsOverSize;
        private static MemoryStream Mem = new MemoryStream();
        private static int _BufferCount;
        private const string Err_Log_Type = "Incorrect log type";
        private const long DefaultFileSize = (1024 * 1000 * 10);

        public class MsgPackInfo
        {
            public string ThreadId;
            public LogType LogType;
            public string Functional;
            public string classMethod;
            public string Message;
            public string SystemName;
            public EntryStatus EntryStatus;
        }

        public bool IsBuffered
        {
            get
            {
                return _IsBuffered;
            }
            set
            {
                _IsBuffered = value;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _IsEnabled;
            }
            set
            {
                _IsEnabled = value;
            }
        }

        public int MaxBufferCount
        {
            get
            {
                return _MaxBufferCount;
            }
            set
            {
                _MaxBufferCount = value;
            }
        }

        public int BufferCount
        {
            get
            {
                return _BufferCount;
            }
            set
            {
                _BufferCount = value;
            }
        }

        public string LogPath
        {
            get
            {
                return _LogPath;
            }
            set
            {
                _LogPath = value;
            }
        }

        private void InitalConfig()
        {
            _IsEnabled = System.Convert.ToBoolean(ConfigurationSettings.AppSettings["IsEnabled"]);
            _IsBuffered = System.Convert.ToBoolean(ConfigurationSettings.AppSettings["IsBuffered"]);
            _MaxBufferCount = System.Convert.ToInt32(ConfigurationSettings.AppSettings["MaxBufferCount"]);
            _LogPath = System.Convert.ToString(ConfigurationSettings.AppSettings["LogPath"]);
        }

        private Logger()
        {
            InitalConfig();
            _LogFileName = GetLogFileName();
        }

        public static Logger GetInstance()
        {
            _mu.WaitOne();
            try
            {
                if (_logger == null) _logger = new Logger();
            }
            finally
            {
                _mu.ReleaseMutex();
            }
            return _logger;
        }

        public bool WriteLog(Exception ex)
        {
            return WriteLog(ex.Message, LogType.Trace, null, null, null, EntryStatus.PROCESS);
        }

        public bool WriteLog(string Message)
        {
            return WriteLog(Message, LogType.Trace, null, null, null, EntryStatus.PROCESS);
        }

        public bool WriteLog(string Message, LogType lt, string Functional, string ClassMethod, string SystemName, EntryStatus Entry)
        {
            string ThreadId = "";

            if (Functional == null) Functional = "";
            if (ClassMethod == null) ClassMethod = "";
            if (SystemName == null) SystemName = "";

            StackTrace st = new StackTrace(true);

            if (st.FrameCount > 0)
            {
                StackFrame sf = st.GetFrame(1);
                Functional = sf.GetMethod().Name;
                ClassMethod = sf.GetMethod().ReflectedType.Name;
                SystemName = this.GetType().Namespace;
            }
            if (!this.IsEnabled) return false;

            _LogFileName = GetLogFileName();

            if (ThreadId == string.Empty) ThreadId = System.Convert.ToString(System.Threading.Thread.CurrentThread.ManagedThreadId);

            string msg = "";
            switch (lt)
            {
                case LogType.Error:
                    msg = msg + GetESC(ThreadId);
                    msg = msg + GetESC(System.Convert.ToString(LogLevel.ERR));
                    msg = msg + GetESC(Functional);
                    msg = msg + GetESC(ClassMethod);
                    msg = msg + GetESC(Message);
                    break;

                case LogType.Performance:
                    msg = msg + GetESC(ThreadId);
                    msg = msg + GetESC(System.Convert.ToString(LogLevel.PER));
                    msg = msg + GetESC(Functional);
                    msg = msg + GetESC(ClassMethod);
                    msg = msg + GetESC(SystemName);
                    msg = msg + GetESC(System.Convert.ToString(Entry));
                    break;

                case LogType.Trace:
                    msg = msg + GetESC(ThreadId);
                    msg = msg + GetESC(System.Convert.ToString(LogLevel.TRA));
                    msg = msg + GetESC(Functional);
                    msg = msg + GetESC(ClassMethod);
                    msg = msg + GetESC(Message);
                    break;

                default:
                    msg = msg + GetESC(Err_Log_Type);
                    break;
            }

            msg = GetESC(System.Convert.ToString(lt)) + msg;
            msg = GetMessage(msg);
            return WriteFile(msg);
        }

        //<MethodImpl(MethodImplOptions.Synchronized)> _
        private bool WriteMemory(string Msg)
        {
            BinaryWriter bw = new BinaryWriter(Mem);
            try
            {
                bw.Write(Msg);
                BufferCount = BufferCount + 1;
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        //<MethodImpl(MethodImplOptions.Synchronized)> _
        private bool WriteFile(string Msg)
        {
            string sLine;
            StreamWriter sw = null;

            try
            {
                if (this.IsBuffered)
                {
                    WriteMemory(Msg);
                    if (this.BufferCount == this.MaxBufferCount)
                    {
                        Mem.Seek(0, SeekOrigin.Begin);
                        BinaryReader br = new BinaryReader(Mem);
                        sw = returnStreamWriter();
                        while (br.PeekChar() != -1)
                        {
                            sLine = br.ReadString();
                            sw.WriteLine(sLine);
                        }
                        Mem.Close();
                        Mem = new MemoryStream();
                        this.BufferCount = 0;
                        if (br != null) br.Close();
                    }
                }
                else
                {
                    sw = returnStreamWriter();
                    sw.WriteLine(Msg);
                }
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            finally
            {
                if (sw != null) sw.Close();
            }
        }

        //<MethodImpl(MethodImplOptions.Synchronized)> _
        private StreamWriter returnStreamWriter()
        {
            FileStream fs;
            StreamWriter sw;

            while (true)
            {
                try
                {
                    fs = new FileStream(_LogFileName, FileMode.Append, FileAccess.Write, FileShare.Write);
                    sw = new StreamWriter(fs);
                    return sw;
                }
                catch (System.Exception ex)
                {
                    //System.Threading.Thread.CurrentThread.Suspend();
                    //throw(ex.Message);
                }
            }
        }

        //<MethodImpl(MethodImplOptions.Synchronized)> _
        private string GetESC(string Value)
        {
            if (Value == string.Empty) return string.Empty;
            Value = "[" + Value + "]";
            return Value;
        }

        //<MethodImpl(MethodImplOptions.Synchronized)> _
        private string GetMessage(string Msg)
        {
            Msg = "[" + System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss:ffff") + "]" + Msg;
            return Msg;
        }

        //<MethodImpl(MethodImplOptions.Synchronized)> _
        private bool CompareFileSize(string FileName)
        {
            FileInfo fileinfo = new FileInfo(FileName);

            if (fileinfo.Length > DefaultFileSize)
            {
                _IsOverSize = true;
            }
            else
            {
                _IsOverSize = false;
            }
            return _IsOverSize;
        }

        //<MethodImpl(MethodImplOptions.Synchronized)> _
        private string GetLogFileName()
        {
            if (!System.IO.Directory.Exists(_LogPath)) System.IO.Directory.CreateDirectory(_LogPath);

            _LogFileName = System.DateTime.Now.ToString("yyyyMMdd") + ".log";
            _LogFileName = _LogPath + "\\" + _LogFileName;
            _LogFileName = GetNextLogFile(_LogFileName);
            return _LogFileName;
        }

        //<MethodImpl(MethodImplOptions.Synchronized)> _
        private string GetNewLogFileName(string OldFileName)
        {
            string logfilename = OldFileName;

            int SerialNo = GetFileSerialNo(OldFileName) + 1;
            if (GetReplaceChar(OldFileName) != string.Empty) logfilename = logfilename.Replace(GetReplaceChar(logfilename), "");

            logfilename = logfilename.Replace(".log", "");
            logfilename = logfilename + "_" + System.Convert.ToString(SerialNo) + ".log";
            _LogFileName = logfilename;
            return _LogFileName;
        }

        //<MethodImpl(MethodImplOptions.Synchronized)> _
        private int GetFileSerialNo(string LogFileName)
        {
            int iBegin;
            string SerialNo;

            iBegin = LogFileName.LastIndexOf("_");
            if (iBegin <= 0) return 0;
            SerialNo = LogFileName.Substring(iBegin + 1);
            SerialNo = SerialNo.Replace(".log", "");
            return System.Convert.ToInt32(SerialNo);
        }

        //<MethodImpl(MethodImplOptions.Synchronized)> _
        private string GetNextLogFile(string LogFileName)
        {
            int SerialNo = 1;
            while (true)
            {
                if (!File.Exists(LogFileName))
                {
                    _LogFileName = LogFileName;
                    return _LogFileName;
                }
                else
                {
                    if (!CompareFileSize(LogFileName))
                    {
                        _LogFileName = LogFileName;
                        return _LogFileName;
                    }
                    if (GetReplaceChar(LogFileName) != string.Empty) LogFileName = LogFileName.Replace(GetReplaceChar(LogFileName), "");

                    LogFileName = LogFileName.Replace(".log", "");
                    LogFileName = LogFileName + "_" + System.Convert.ToString(SerialNo) + ".log";
                }
                SerialNo = SerialNo + 1;
            }
        }

        //<MethodImpl(MethodImplOptions.Synchronized)> _
        private string GetReplaceChar(string Value)
        {
            if (Value.LastIndexOf("_") < 0) return string.Empty;
            return Value.Substring(Value.LastIndexOf("_"));
        }
    }
}