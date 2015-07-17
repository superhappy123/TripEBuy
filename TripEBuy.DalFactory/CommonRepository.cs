using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripEBuy.Common;
using TripEBuy.Model;

namespace TripEBuy.DalFactory
{
    public class CommonRepository : IDisposable
    {
        public SQLHelper sqlHelper;
        /// <summary>
        /// 根据AccessToken获取会员ID
        /// </summary>
        /// <param name="Access_token"></param>
        /// <param name="AccountTpye"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        //public int GetCustomerid(string Access_token, ref int AccountTpye, SQLHelper sql)
        //{
        //    int Result = 0;

        //    string Commondtext = "USP_PCustomer_V2_ValidateAccessToken";
        //    SqlParameter[] Par =
        //  {
        //      new SqlParameter("@Accesstoken",Access_token),
        //      new SqlParameter("@CustomerID",SqlDbType.Int),
        //      new SqlParameter("@Accounttype",SqlDbType.VarChar,200),
        //      new SqlParameter("@ReturnMsg",SqlDbType.VarChar,200),
        //      new SqlParameter("@AccountName",SqlDbType.VarChar,200)

        //  };
        //    for (int i = 1; i < Par.Count(); i++)
        //    {
        //        Par[i].Direction = ParameterDirection.Output;
        //    }
        //    sql.ExecuteNonQuery(CommandType.StoredProcedure, Commondtext, Par);
        //    Result = ConvertHelper.ToInt32(Par[1].Value.ToString());
        //    AccountTpye = ConvertHelper.ToInt32(Par[2].Value.ToString());
        //    return Result;
        //}

        /// <summary>
        /// 获取AccessToken获取企业ID
        /// </summary>
        /// <param name="Access_token"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int GetPCustomerid(string Access_token, SQLHelper sql)
        {
            int Result = 0;

            const string Commondtext = "USP_PCustomer_V2_GetPCustomerID";
            SqlParameter[] Parameter =
          {
              new SqlParameter("@Accesstoken",Access_token),
              new SqlParameter("@PCustomerID",SqlDbType.Int),     

          };
            for (int i = 1; i < Parameter.Count(); i++)
            {
                Parameter[i].Direction = ParameterDirection.Output;
            }
            sql.ExecuteNonQuery(CommandType.StoredProcedure, Commondtext, Parameter);

            Result = ConvertHelper.ToInt32(Parameter[Parameter.Length - 1].Value);
            return Result;
        }

        /// <summary>
        /// 分页通用方法
        /// </summary>
        /// <param name="input"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public PaginationResponse ProcessPagination(PaginationRequest input, SQLHelper sql)
        {

            const string CmdText = "Usp_PCustomer_V2_Pagination";
            PaginationResponse Response = null;
            SqlParameter[] Parameter = PrepareParameters(input);

            try
            {
                DataTable dt = sql.ExecuteDataTable(CommandType.StoredProcedure, CmdText, "", Parameter);
                Response = new PaginationResponse();
                Response.dt = dt;
                Response.TotalPage = ConvertHelper.ToInt32(Parameter[Parameter.Length - 1].Value.ToString());
                Response.TotalRecord = ConvertHelper.ToInt32(Parameter[Parameter.Length - 2].Value.ToString());
                return Response;
            }
            catch (Exception ex)
            {
                sql.CloseConnection();

            }


            return Response;

        }
        /// <summary>
        /// 分页存储过程参数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private SqlParameter[] PrepareParameters(PaginationRequest input)
        {

            SqlParameter[] Parameter ={
                            new SqlParameter("@Tables",input.Tables),
                            new SqlParameter("@PrimaryKey",input.PrimaryKey),
                            new SqlParameter("@Sort",input.Sort),
                            new SqlParameter("@CurrentPage",input.CurrentPage),
                            new SqlParameter("@PageSize",input.PageSize),
                            new SqlParameter("@Fields",input.Fields),
                            new SqlParameter("@Filter",input.Filter),
                            new SqlParameter("@Group",input.Group),
                            new SqlParameter("@TotalRecord",SqlDbType.Int,10),
                            new SqlParameter("@TotalPage",SqlDbType.Int,10)
                                     };
            Parameter[8].Direction = ParameterDirection.Output;
            Parameter[9].Direction = ParameterDirection.Output;

            return Parameter;
        }

        //public int GetComPanyId(int Customerid, int Accountype, SQLHelper sql)
        //{
        //    int ComPanyId = 0;
        //    string Commondtext = "USP_PCustomer_V2_CompanyId";
        //    SqlParameter[] Parameter = ComPanyPrepareParameters(Customerid, Accountype);
        //    try
        //    {
        //        sql.ExecuteNonQuery(CommandType.StoredProcedure, Commondtext, Parameter);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.GetInstance().WriteLog("获取账户公司信息异常:" + ex.Message.ToString());
        //        Logger.GetInstance().WriteLog("Customerid:" + Customerid.ToString());
        //        Logger.GetInstance().WriteLog("Accountype:" + Accountype.ToString());

        //    }
        //    ComPanyId = ConvertHelper.ToInt32(Parameter[Parameter.Length - 1].Value);
        //    return ComPanyId;

        //}

        public SqlParameter[] ComPanyPrepareParameters(int AccountId, int Accountype)
        {

            SqlParameter[] Parameter ={
                            new SqlParameter("@AccountId",AccountId),
                            new SqlParameter("@AccountTpye",Accountype),
                              new SqlParameter("@CompanyId",SqlDbType.Int,10)
                                   };
            Parameter[Parameter.Length - 1].Direction = ParameterDirection.Output;
            return Parameter;

        }

        /// <summary>
        /// 根据账户名称获取账户信息
        /// </summary>
        /// <param name="appAccount"></param>
        /// <param name="sqlHelper"></param>
        /// <returns></returns>
        public UserAccountInfo GetCustomerInfoByAppAccount(string appAccount, SQLHelper sqlHelper)
        {
            UserAccountInfo uInfo = new UserAccountInfo();
            DataTable dt = null;

            if (appAccount == null || appAccount.Trim().Length == 0)
            {
                return uInfo;
            }

            const string procName = "dbo.Usp_PCustomer_V2_GetCustomerInfo";
            SqlParameter[] spars = new SqlParameter[] { 
                new SqlParameter("@appaccount",appAccount)};

            try
            {
                dt = sqlHelper.ExecuteDataTable(CommandType.StoredProcedure, procName, "", spars);
                if (dt != null && dt.Rows.Count > 0)
                {
                    uInfo = BindUserAccountInfo(uInfo, dt);
                }
            }
            catch (Exception ex)
            {

            }
            return uInfo;
        }

        /// <summary>
        /// 绑定用户账户信息
        /// </summary>
        /// <param name="uInfo"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private UserAccountInfo BindUserAccountInfo(UserAccountInfo uInfo, DataTable dt)
        {
            uInfo.AttributeHeadID = dt.Rows[0]["AttributeHeadID"].ToInt32();
            uInfo.CustomerID = dt.Rows[0]["CustomerID"].ToInt32();
            uInfo.ProtocolCustomerID = dt.Rows[0]["ProtocolCustomerID"].ToInt32();
            uInfo.Accounttpe = dt.Rows[0]["Accounttpe"].ToInt32();
            return uInfo;
        }

        /// <summary>
        /// 将标识转换为数字
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string ConvertAccessTokenCode(string accessToken)
        {
            const string valueList = "1234567890abcdefghijklmnopqrstuvwxyz";
            string tokenCode = accessToken.Substring(0, 5).ToLower();
            StringBuilder valueCode = new StringBuilder();

            foreach (char item in tokenCode)
            {
                valueCode.Append(valueList.IndexOf(item));
            }
            return valueCode.ToString();
        }

        /// <summary>
        /// 验证accessToken是否合法，返回账户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="sqlHelper"></param>
        /// <returns></returns>
        public bool ValidateAccessToken(string accessToken, SQLHelper sqlHelper, ref string appAcount)
        {
            bool result = false;
            string accessTokenCode = ConvertAccessTokenCode(accessToken);

            const string procName = "dbo.Usp_PCustomer_V2_GetUserInfoByAccesstoken";
            SqlParameter[] spars = new SqlParameter[] { 
                new SqlParameter("@Accesstoken",accessToken)
                ,new SqlParameter("@AccesstokenCode",accessTokenCode)};

            try
            {
                DataTable dt = sqlHelper.ExecuteDataTable(CommandType.StoredProcedure, procName, "", spars);
                if (dt != null && dt.Rows.Count > 0)
                {
                    appAcount = dt.Rows[0]["app_account"].ToString();
                    if (appAcount.Trim().Length > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 根据accessToken获取账户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="sqlHelper"></param>
        /// <returns></returns>
        public UserAccountInfo GetCustomerInfoByAccessToken(string accessToken, SQLHelper sqlHelper)
        {
            UserAccountInfo uInfo = new UserAccountInfo();
            string appAcount = "";

            if (ValidateAccessToken(accessToken, sqlHelper, ref appAcount))
            {
                uInfo = GetCustomerInfoByAppAccount(appAcount, sqlHelper);
                uInfo = UserAccountchange(uInfo);
            }
            else
            {
                throw new Exception("验证失败-错误的令牌。");

            }

            return uInfo;
        }

        /// <summary>
        /// 创建连接
        /// </summary>
        public void CreateConnecting()
        {
            sqlHelper = new SQLHelper();
            try
            {
                sqlHelper.CreateConnection(ConfigHelper.ConnStr_AutoRent);
            }
            catch (Exception ex)
            {
                CloseConnecting();
            }
            finally
            {

            }


        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseConnecting()
        {
            sqlHelper.CloseConnection();
            sqlHelper.Dispose();
        }
        public CommonRepository()
        {
            CreateConnecting();

        }

        public UserAccountInfo UserAccountchange(UserAccountInfo info)
        {
            UserAccountInfo Result = new UserAccountInfo();
            if (info.Accounttpe == 2)
            {
                Result.CustomerID = info.ProtocolCustomerID;
                Result.Accounttpe = 2;
                Result.AttributeHeadID = info.AttributeHeadID;
                Result.ProtocolCustomerID = info.ProtocolCustomerID;


            }
            if (info.Accounttpe == 1)
            {
                Result.CustomerID = info.CustomerID;
                Result.Accounttpe = 1;
                Result.AttributeHeadID = info.AttributeHeadID;
                Result.ProtocolCustomerID = info.ProtocolCustomerID;

            }
            //if (info.Accounttpe == 3)
            //{
            //    Result.Accounttpe = 3;
            //    Result.CustomerID = info.AttributeHeadID;
            //    Result.ProtocolCustomerID = info.ProtocolCustomerID;
            //    Result.AttributeHeadID = info.AttributeHeadID;

            //}
            return Result;

        }
        /// <summary>
        /// 查询订车人账号id
        /// </summary>
        /// <param name="Customerid"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Getstaffid(int Customerid, SQLHelper sql)
        {
            DataTable dt = new DataTable();
            string comondtxt = string.Format(@"SELECT PID FROM TB_PCustomerAssigner WHERE CustomerID={0}", Customerid);
            dt = sql.ExecuteDataTable(CommandType.Text, comondtxt, "", null);
            if (dt.Rows.Count > 0)
            {

                return dt.Rows[0][0].ToInt32();
            }
            else
            {
                return 0;
            }
        }

        public void Dispose()
        {
            if (sqlHelper != null)
            {
                if (sqlHelper != null)
                    sqlHelper.Dispose();
                sqlHelper = null;

            }
        }
    }
}