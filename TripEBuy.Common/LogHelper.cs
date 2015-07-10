using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.App;
using System.Diagnostics;
using System.IO;
using System.Threading;



namespace TripEBuy.Common
{
   
   public  class LogHelper
    {
        private static Mutex _mu = new Mutex();
        private static LogHelper _logger;
        public static LogHelper GetInstance()
        {
            _mu.WaitOne();
            try
            {
                if (_logger == null) _logger = new LogHelper();
            }
            finally
            {
                _mu.ReleaseMutex();
            }
            return _logger;
        }
       /// <summary>
       /// 保存异常日志
       /// </summary>
       /// <param name="Exceptlg"></param>
      public void WriteExceptlg( Exceptlg  Exceptlg)
       {

           Logs.WriteLog(Exceptlg.module,Exceptlg.Errormessage,Exceptlg.user,Exceptlg.level,Exceptlg.custom_field1,Exceptlg.custom_field2,Exceptlg.used_time);
       }
       /// <summary>
       /// 保存调用日志
       /// </summary>
       /// <param name="Calllog"></param>
      public  void  WriteCalllog(Calllog Calllog)
      {
          Logs.WriteEvent(Calllog.module,Calllog.paramater,Calllog.result,Calllog.user,Calllog.custom_field1,Calllog.custom_field2,Calllog.used_time);
          
      }
    }
    /// <summary>
    /// 异常日志
    /// </summary>
   public class  Exceptlg
    {

        public string module ;
        public  string  Errormessage;
        public string user ;
        public string  level ;
        public  string  custom_field1;
        public  string  custom_field2;
        public  int used_time;
        public Exceptlg()
        {
        }
        public Exceptlg (string module, string Errormessage, string user, string level, string custom_field1, string custom_field2, int used_time)
        {
            this.module = module;
            this.Errormessage = Errormessage;
            this.user = user;
            this.level = level;
            this.custom_field1 = custom_field1;
            this.custom_field2 = custom_field2;
            this.used_time = used_time;
        }
    }
    /// <summary>
    /// 调用记录日志
    /// </summary>
   public  class  Calllog
    {
     public string module;
	 public string user;
	 public string result;
	 public string paramater;
	 public string custom_field1;
	 public string custom_field2;
	 public int used_time;

     public Calllog(string module, string user, string result, string paramater, string custom_field1, string custom_field2, int used_time)
     {
         this.module = module;
         this.user = user;
         this.result = result;
         this.paramater = paramater;
         this.custom_field1 = custom_field1;
         this.custom_field2 = custom_field2;
         this.used_time = used_time;
     }

  

     public Calllog()
     {
     }

     
    }

   
    

   
}
