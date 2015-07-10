using System;
using System.Data;
using System.Data.SqlClient;

namespace TripEBuy.Common
{
    public class SQLHelper : IDisposable, ISQLHelper
    {
        #region Fields

        private int CommandTimeout = 300;

        // Private MinPoolSize As Integer Private MaxPoolSize As Integer 
        private SqlConnection DBConn;

        private string DBConnStr;

        private SqlTransaction DBTran;

        private bool TranFlag;

        #endregion Fields

        #region Constructors

        public SQLHelper()
        {
        }

        public SQLHelper(int intCommandTimeout)
        {
            CommandTimeout = intCommandTimeout;
        }

        #endregion Constructors

        #region Properties

        public bool IsTransaction
        {
            get
            {
                return TranFlag;
            }
        }

        #endregion Properties

        #region Methods

        public void CloseConnection()
        {
            if (DBConn != null)
            {
                if (DBConn.State != ConnectionState.Closed)
                {
                    DBConn.Close();
                }
                DBConn.Dispose();
            }
        }

        public void CommitTran()
        {
            DBTran.Commit();
            TranFlag = false;
        }

        public bool CreateConnection(string ConnStr)
        {
            try
            {
                DBConnStr = (ConnStr + ";Connection TimeOut=30");
                DBConn = new SqlConnection(DBConnStr);

                DBConn.Open();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            if (!(DBTran == null))
            {
                DBTran.Dispose();
            }
            if (!(DBConn == null))
            {
                if (!(DBConn.State == ConnectionState.Closed))
                {
                    DBConn.Close();
                    DBConn.Dispose();
                }
            }
        }

        public DataSet ExecuteDataset(CommandType commandType, string commandText, string tableName = "")
        {
            return ExecuteDataset(commandType, commandText, tableName, ((SqlParameter[])(null)));
        }

        public DataSet ExecuteDataset(CommandType commandType, string commandText, string tableName, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdatpter = null;
            PrepareCommand(cmd, commandType, commandText, ref commandParameters);

            try
            {
                dataAdatpter = new SqlDataAdapter(cmd);
                if ((tableName == ""))
                {
                    dataAdatpter.Fill(ds);
                }
                else
                {
                    dataAdatpter.Fill(ds, tableName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!(dataAdatpter == null))
                {
                    dataAdatpter.Dispose();
                }
            }
            return ds;
        }

        public DataSet ExecuteDataset(int pageIndex, int pageSize, CommandType commandType, string commandText, string tableName = "")
        {
            return ExecuteDataset(pageIndex, pageSize, commandType, commandText, tableName, ((SqlParameter[])(null)));
        }

        public DataSet ExecuteDataset(int pageIndex, int pageSize, CommandType commandType, string commandText, string tableName, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdatpter = null;
            PrepareCommand(cmd, commandType, commandText, ref commandParameters);
            try
            {
                dataAdatpter = new SqlDataAdapter(cmd);
                dataAdatpter.Fill(ds, (pageIndex * pageSize), pageSize, tableName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!(dataAdatpter == null))
                {
                    dataAdatpter.Dispose();
                }
            }
            return ds;
        }

        public DataSet ExecuteDataset(string commandText, string tableName, ref SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdatpter = null;
            PrepareCommand(cmd, CommandType.StoredProcedure, commandText, ref commandParameters);
            try
            {
                dataAdatpter = new SqlDataAdapter(cmd);
                dataAdatpter.Fill(ds, tableName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!(dataAdatpter == null))
                {
                    dataAdatpter.Dispose();
                }
            }
            return ds;
        }

        public DataTable ExecuteDataTable(CommandType commandType, string commandText, string tableName = "")
        {
            return ExecuteDataTable(commandType, commandText, tableName, ((SqlParameter[])(null)));
        }

        public DataTable ExecuteDataTable(CommandType commandType, string commandText, string tableName, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            SqlDataAdapter dataAdatpter = null;
            PrepareCommand(cmd, commandType, commandText, ref commandParameters);
            try
            {
                dataAdatpter = new SqlDataAdapter(cmd);
                dataAdatpter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!(dataAdatpter == null))
                {
                    dataAdatpter.Dispose();
                }
            }
            cmd.Parameters.Clear();
            return dt;
        }

        public int ExecuteInsert(DataTable dt, string tableName, string primaryKeyName)
        {
            int result = 0;

            string selectCommandText = string.Format("Select top 1 * from {0} Where {1}=0", tableName, primaryKeyName);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommandText, DBConn);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            sqlDataAdapter.InsertCommand = sqlCommandBuilder.GetInsertCommand();
            SqlCommand cmdText = sqlDataAdapter.InsertCommand;
            cmdText.CommandText += ";Select @@IDENTITY";
            InitSqlCommandParameters(sqlDataAdapter.InsertCommand, dt.Rows[0]);
            object obj = sqlDataAdapter.InsertCommand.ExecuteScalar();
            if (obj != null && obj.ToString() != string.Empty)
            {
                result = Convert.ToInt32(obj);
            }
            sqlDataAdapter.Dispose();

            return result;
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(commandType, commandText, ((SqlParameter[])(null)));
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            int retval;
            PrepareCommand(cmd, commandType, commandText, ref commandParameters);
            retval = cmd.ExecuteNonQuery();
            return retval;
        }

        public SqlDataReader ExecuteReader(CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                SqlDataReader dataReader;
                PrepareCommand(cmd, commandType, commandText, ref commandParameters);
                dataReader = cmd.ExecuteReader();
                return dataReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return ExecuteReader(commandType, commandText, ((SqlParameter[])(null)));
        }

        public int ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, commandType, commandText, ref commandParameters);
            var val = (int)cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public object ExecuteScalar2(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, commandType, commandText, ref commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public void ExecuteSqlBulkCopy(DataTable dataTable, string desTableName)
        {
            SqlBulkCopy bulkCopy = new SqlBulkCopy(DBConnStr, SqlBulkCopyOptions.UseInternalTransaction);
            bulkCopy.DestinationTableName = desTableName;
            try
            {
                bulkCopy.WriteToServer(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExecuteSqlBulkCopy(DataTable dataTable, string desTableName, SqlBulkCopyOptions copyOption)
        {
            SqlBulkCopy bulkCopy = new SqlBulkCopy(DBConnStr, copyOption);
            bulkCopy.DestinationTableName = desTableName;
            try
            {
                bulkCopy.WriteToServer(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExecuteSqlBulkCopyInExistingTran(DataTable dataTable, string desTableName)
        {
            SqlBulkCopy bulkCopy = new SqlBulkCopy(DBConn, SqlBulkCopyOptions.Default, DBTran);
            bulkCopy.DestinationTableName = desTableName;
            try
            {
                bulkCopy.WriteToServer(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExecuteUpdate(DataTable dt, string tableName, string primaryKeyName)
        {
            string selectCommandText = string.Format("Select top 1 * from {0} Where {1}=0", tableName, primaryKeyName);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommandText, DBConn);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            sqlCommandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
            sqlDataAdapter.InsertCommand = sqlCommandBuilder.GetInsertCommand();
            sqlDataAdapter.UpdateCommand = sqlCommandBuilder.GetUpdateCommand();
            sqlDataAdapter.DeleteCommand = sqlCommandBuilder.GetDeleteCommand();
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow.RowState == DataRowState.Added)
                {
                    InitSqlCommandParameters(sqlDataAdapter.InsertCommand, dataRow);
                    sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                }
                if (dataRow.RowState == DataRowState.Modified)
                {
                    InitSqlCommandParameters(sqlDataAdapter.UpdateCommand, dataRow);
                    sqlDataAdapter.UpdateCommand.ExecuteNonQuery();
                }
                if (dataRow.RowState == DataRowState.Deleted)
                {
                    InitSqlCommandParameters(sqlDataAdapter.DeleteCommand, dataRow);
                    sqlDataAdapter.DeleteCommand.ExecuteNonQuery();
                }
            }
        }

        public SqlParameter GetCmdParam(string paramName, object paramValue, DbType paramType, int paramSize = 0)
        {
            SqlParameter cmdParam = new SqlParameter();

            cmdParam.ParameterName = paramName;

            if ((paramValue == null))
            {
                paramValue = DBNull.Value;
            }

            cmdParam.Value = paramValue;
            if (!(paramValue == null))
            {
                if (!System.Convert.IsDBNull(paramValue))
                {
                    cmdParam.DbType = paramType;
                    if ((paramSize > 0))
                    {
                        cmdParam.Size = paramSize;
                    }
                }
            }
            return cmdParam;
        }

        public void RollbackTran()
        {
            DBTran.Rollback();
            TranFlag = false;
        }

        private void AttachParameters(SqlCommand command, ref SqlParameter[] commandParameters)
        {
            if ((command == null))
            {
                throw new ArgumentNullException("command");
            }
            if (!(commandParameters == null))
            {
                //SqlParameter p;
                command.Parameters.Clear();
                foreach (SqlParameter p in commandParameters)
                {
                    if (!(p == null))
                    {
                        if ((((p.Direction == ParameterDirection.InputOutput)
                                    || (p.Direction == ParameterDirection.Input))
                                    && (p.Value == null)))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        private void BeginTran()
        {
            DBTran = DBConn.BeginTransaction();
            TranFlag = true;
        }

        private void DetachParameters(SqlCommand command)
        {
            if ((command == null))
            {
                throw new ArgumentNullException("command");
            }
            while ((command.Parameters.Count > 0))
            {
                command.Parameters.RemoveAt(0);
            }
        }

        private void InitSqlCommandParameters(SqlCommand comm, DataRow dr)
        {
            foreach (SqlParameter sqlParameter in comm.Parameters)
            {
                try
                {
                    int num = dr.Table.Columns.IndexOf(sqlParameter.SourceColumn);
                    if (num != -1)
                    {
                        if (dr.RowState != DataRowState.Deleted)
                        {
                            sqlParameter.Value = dr[num];
                        }
                        else
                        {
                            sqlParameter.Value = dr[num, DataRowVersion.Original];
                        }
                    }
                    else
                    {
                        sqlParameter.Value = DBNull.Value;
                    }
                }
                catch (Exception)
                {
                    throw new Exception(sqlParameter.SourceColumn);
                }
            }
        }

        private void PrepareCommand(SqlCommand command, CommandType commandType, string commandText, ref SqlParameter[] commandParameters)
        {
            if (!(commandParameters == null))
            {
                int i;
                for (i = 0; (i
                            <= (commandParameters.Length - 1)); i++)
                {
                    // Logger.GetInstance.WriteLog 
                }
            }
            if ((command == null))
            {
                throw new ArgumentNullException("command");
            }
            if (((commandText == null)
                        || (commandText.Length == 0)))
            {
                throw new ArgumentNullException("commandText");
            }
            if ((DBConn.State != ConnectionState.Open))
            {
                CreateConnection(DBConnStr);
            }
            command.Connection = DBConn;
            command.CommandText = commandText;
            command.CommandType = commandType;
            command.CommandTimeout = CommandTimeout;
            if (TranFlag)
            {
                command.Transaction = DBTran;
            }
            if (!(commandParameters == null))
            {
                DetachParameters(command);
                AttachParameters(command, ref commandParameters);
            }
            return;
        }

        #endregion Methods
    }
}