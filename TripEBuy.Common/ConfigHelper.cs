using System.Configuration;
using System.Threading;

namespace TripEBuy.Common
{
    /// <summary>
    /// Summary description for ConfigHelper
    /// </summary>
    public class ConfigHelper
    {
        private static Mutex _mu = new Mutex();
        private static ConfigHelper _config;
        private static string strDBConn;

        public string DBConnStr { get; set; }

        public string SignVerificationInd { get; set; }

        public string KeyString { get; set; }

        public string DistributorID { get; set; }

        public ConfigHelper()
        {
            LoadInitialParameters();
        }

        private void LoadInitialParameters()
        {
            SignVerificationInd = ConfigurationManager.AppSettings["SignVerificationInd"].ToString();
            //KeyString = ConfigurationManager.AppSettings["KeyString"].ToString();
            //DistributorID = ConfigurationManager.AppSettings["DistributorID"].ToString();
        }

        public static ConfigHelper GetInstance()
        {
            _mu.WaitOne();
            try
            {
                if (_config == null)
                    _config = new ConfigHelper();
            }
            finally
            {
                _mu.ReleaseMutex();
            }

            return (_config);
        }

        /// <summary>
        /// 数据库连接字符串(数据库：192.168.1.5 AutoRent)
        /// </summary>
        public static string ConnStr_AutoRent
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConnStr_AutoRent"].ToString();
            }
        }

        /// <summary>
        /// 数据库连接字符串(192.168.1.26 ToponePeccancyAgent)
        /// </summary>
        public static string ConnStr_ToponePeccancyAgent
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConnStr_ToponePeccancyAgent"].ToString();
            }
        }

        /// <summary>
        /// 根据web.config里配置的connectionStrings节点下的name获取数据库连接字符串
        /// </summary>
        /// <param name="connectionStringsName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string connectionStringsName)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringsName].ToString();
        }

        /// <summary>
        /// 根据web.config里配置的appSettings节点下的key获取AppSettings
        /// </summary>
        /// <param name="appSettingKey"></param>
        /// <returns></returns>
        public static string GetAppSettings(string appSettingKey)
        {
            return ConfigurationManager.AppSettings[appSettingKey];
        }

        /// <summary>
        /// 获取添加汽车时的ChannelId
        /// </summary>
        /// <returns></returns>
        public static string GetCarChannel()
        {
            return ConfigurationManager.AppSettings["ChannelId"];
        }

        /// <summary>
        /// 获取分销商支付的标识
        /// </summary>
        /// <returns></returns>
        public static string GetPaymentMode()
        {
            return ConfigurationManager.AppSettings["PaymentMode"];
        }
    }
}