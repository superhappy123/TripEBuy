using System;
using System.Data;
using System.Data.OleDb;

namespace TripEBuy.Common
{
    /// <summary>
    /// Summary description for DBHelper
    /// </summary>
    public class DBHelper
    {
        private OleDbConnection DBConn;
        private string DBConnStr;
        private OleDbTransaction DBTran;
        private bool TranFlag;

        public DBHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "connect/close"

        public bool CreateConnection(string ConnStr)
        {
            try
            {
                DBConnStr = ConnStr + ";Connection TimeOut=60";

                DBConn = new OleDbConnection(DBConnStr);
                DBConn.Open();
                return (true);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CloseConnection()
        {
            if (!(DBConn == null))
                DBConn.Close();
        }

        #endregion "connect/close"

        #region "transaction"

        public void BeginTran()
        {
            DBTran = DBConn.BeginTransaction();
            TranFlag = true;
        }

        public void CommitTran()
        {
            DBTran.Commit();
            TranFlag = false;
        }

        public void RollbackTran()
        {
            DBTran.Rollback();
            TranFlag = false;
        }

        #endregion "transaction"

        #region"GetCmdParam"

        public OleDbParameter GetCmdParam(string paramName, object paramValue, OleDbType paramType)
        {
            return (this.GetCmdParam(paramName, paramValue, paramType, 0, ParameterDirection.Input));
        }

        public OleDbParameter GetCmdParam(string paramName, object paramValue, OleDbType paramType, int paramSize)
        {
            return (this.GetCmdParam(paramName, paramValue, paramType, paramSize, ParameterDirection.Input));
        }

        public OleDbParameter GetCmdParam(string paramName, object paramValue, OleDbType paramType, int paramSize, ParameterDirection direction)
        {
            OleDbParameter cmdParam = new OleDbParameter();
            cmdParam.ParameterName = paramName;
            cmdParam.Value = paramValue;
            cmdParam.Direction = direction;

            if (!(paramValue == null))
            {
                if (!(paramValue is DBNull))
                {
                    cmdParam.OleDbType = paramType;
                    if (paramSize > 0)
                        cmdParam.Size = paramSize;
                }
            }

            return (cmdParam);
        }

        #endregion

        #region"ExecuteDataset"

        public DataSet ExecuteDataset(CommandType commandType
            , string commandText
            )
        {
            return ExecuteDataset(commandType, commandText, "", null);
        }

        public DataSet ExecuteDataset(CommandType commandType
            , string commandText
            , string tableName)
        {
            return ExecuteDataset(commandType, commandText, tableName, null);
        }

        public DataSet ExecuteDataset(CommandType commandType
            , string commandText
            , string tableName
            , params OleDbParameter[] commandParameters)
        {
            //AppLog.PutAppLog(commandText, LogType.Trace, this, "TopOne.Web.APIs.EnterpriseAdminTrafficViolationAgency", EntryStatus.PROCESS);

            OleDbCommand cmd = new OleDbCommand();
            DataSet ds = new DataSet();
            OleDbDataAdapter dataAdatpter = new OleDbDataAdapter();

            try
            {
                PrepareCommand(cmd, commandType, commandText, commandParameters);

                dataAdatpter = new OleDbDataAdapter(cmd);
                if (tableName == "")
                    dataAdatpter.Fill(ds);
                else
                    dataAdatpter.Fill(ds, tableName);
            }
            finally
            {
                if (!(dataAdatpter == null))
                    dataAdatpter.Dispose();
            }
            return (ds);
        }

        public DataSet ExecuteDataset(CommandType commandType
            , string commandText
            , string tableName
            , ref DataSet ds
            , params OleDbParameter[] commandParameters)
        {
            //AppLog.PutAppLog(commandText, LogType.Trace, this, "TopOne.Web.APIs.EnterpriseAdminTrafficViolationAgency", EntryStatus.PROCESS);

            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter dataAdatpter = new OleDbDataAdapter();

            try
            {
                PrepareCommand(cmd, commandType, commandText, commandParameters);
                dataAdatpter = new OleDbDataAdapter(cmd);
                if (tableName == "")
                    dataAdatpter.Fill(ds);
                else
                    dataAdatpter.Fill(ds, tableName);
            }
            finally
            {
                if (!(dataAdatpter == null))
                    dataAdatpter.Dispose();
            }
            return (ds);
        }

        /*
         *should assign a certain table name
        public DataSet ExecuteDataset(int pageIndex
            , int pageSize
            , CommandType commandType
            , string commandText)
        {
            return ExecuteDataset(pageIndex, pageSize, commandType, commandText, "", null);
        }
        */

        public DataSet ExecuteDataset(int pageIndex
            , int pageSize
            , CommandType commandType
            , string commandText
            , string tableName)
        {
            return ExecuteDataset(pageIndex, pageSize, commandType, commandText, tableName, null);
        }

        public DataSet ExecuteDataset(int pageIndex
            , int pageSize, CommandType commandType
            , string commandText
            , string tableName
            , params OleDbParameter[] commandParameters)
        {
            //AppLog.PutAppLog(commandText, LogType.Trace, this, "TopOne.Web.APIs.EnterpriseAdminTrafficViolationAgency", EntryStatus.PROCESS);

            OleDbCommand cmd = new OleDbCommand();
            DataSet ds = new DataSet();
            OleDbDataAdapter dataAdatpter = new OleDbDataAdapter();

            PrepareCommand(cmd, commandType, commandText, commandParameters);
            try
            {
                dataAdatpter = new OleDbDataAdapter(cmd);
                dataAdatpter.Fill(ds, pageIndex * pageSize, pageSize, tableName);
            }
            finally
            {
                if (!(dataAdatpter == null))
                    dataAdatpter.Dispose();
            }
            return (ds);
        }

        #endregion

        #region "ExecuteReader"

        public OleDbDataReader ExecuteReader(CommandType commandType
            , string commandText
            , params OleDbParameter[] commandParameters)
        {
            //AppLog.PutAppLog(commandText, LogType.Trace, this, "TopOne.Web.APIs.EnterpriseAdminTrafficViolationAgency", EntryStatus.PROCESS);

            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader dataReader;
            PrepareCommand(cmd, commandType, commandText, commandParameters);
            dataReader = cmd.ExecuteReader();
            return (dataReader);
        }

        public OleDbDataReader ExecuteReader(CommandType commandType
            , string commandText)
        {
            return ExecuteReader(commandType, commandText, null);
        }

        #endregion

        public int ExecuteNonQuery(CommandType commandType
            , string commandText)
        {
            return ExecuteNonQuery(commandType, commandText, null);
        }

        public int ExecuteNonQuery(CommandType commandType
            , string commandText
            , params OleDbParameter[] commandParameters)
        {
            //AppLog.PutAppLog(commandText, LogType.Trace, this, "TopOne.Web.APIs.EnterpriseAdminTrafficViolationAgency", EntryStatus.PROCESS);

            OleDbCommand cmd = new OleDbCommand();
            int retval;
            PrepareCommand(cmd, commandType, commandText, commandParameters);
            retval = cmd.ExecuteNonQuery();
            return (retval);
        }

        public int ExecuteScalar(CommandType commandType
            , string commandText
            , params OleDbParameter[] commandParameters)
        {
            //AppLog.PutAppLog(commandText, LogType.Trace, this, "TopOne.Web.APIs.EnterpriseAdminTrafficViolationAgency", EntryStatus.PROCESS);

            OleDbCommand cmd = new OleDbCommand();
            PrepareCommand(cmd, commandType, commandText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return (Convert.ToInt32(val));
        }

        private void PrepareCommand(OleDbCommand command
            , CommandType commandType
            , string commandText
            , OleDbParameter[] commandParameters)
        {
            //Logger.GetInstance().WriteLog("Command: type - " & commandType & ";text - " & commandText, LogType.Trace)

            if (!(commandParameters == null || commandParameters.Length == 0))
            {
                int i;
                for (i = 0; i <= commandParameters.Length - 1; i++)
                {
                    //AppLog.WriteLog("Parameter: name - " & commandParameters(i).ParameterName & ";value - " & commandParameters(i).Value, LogType.APP, LogLevel.DEBUG)
                }
            }

            if (command == null)
                throw new ArgumentNullException("command");

            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            if (DBConn.State != ConnectionState.Open)
                CreateConnection(DBConnStr);

            command.Connection = DBConn;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (TranFlag)
                command.Transaction = DBTran;
            if (!(commandParameters == null || commandParameters.Length == 0))
            {
                DetachParameters(command);
                AttachParameters(command, commandParameters);
            }
            return;
        }

        private void AttachParameters(OleDbCommand command
            , OleDbParameter[] commandParameters)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            if (!(commandParameters == null || commandParameters.Length == 0))
            {
                command.Parameters.Clear();
                foreach (OleDbParameter p in commandParameters)
                {
                    if (!(p == null))
                    {
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input)
                             && p.Value == null)
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        private void DetachParameters(OleDbCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            while (command.Parameters.Count > 0)
            {
                command.Parameters.RemoveAt(0);
            }
        }

        public OleDbDataAdapter GetDataAdatpter(string commandText)
        {
            OleDbDataAdapter DataAdatpter;
            OleDbCommand command = new OleDbCommand();
            command.Transaction = DBTran;
            command.CommandType = CommandType.Text;
            command.CommandText = commandText;
            command.Connection = DBConn;
            DataAdatpter = new OleDbDataAdapter(command);
            return (DataAdatpter);
        }
    }
}