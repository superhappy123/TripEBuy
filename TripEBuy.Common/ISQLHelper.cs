using System.Data;
using System.Data.SqlClient;

namespace TripEBuy.Common
{
    public interface ISQLHelper
    {
        bool CreateConnection(string ConnStr);

        void CloseConnection();

        DataSet ExecuteDataset(CommandType commandType, string commandText, string tableName = "");

        DataSet ExecuteDataset(CommandType commandType, string commandText, string tableName, params SqlParameter[] commandParameters);

        DataSet ExecuteDataset(int pageIndex, int pageSize, CommandType commandType, string commandText, string tableName = "");

        DataSet ExecuteDataset(int pageIndex, int pageSize, CommandType commandType, string commandText, string tableName, params SqlParameter[] commandParameters);

        DataTable ExecuteDataTable(CommandType commandType, string commandText, string tableName = "");

        DataTable ExecuteDataTable(CommandType commandType, string commandText, string tableName, params SqlParameter[] commandParameters);

        int ExecuteNonQuery(CommandType commandType, string commandText);

        int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters);
    }
}