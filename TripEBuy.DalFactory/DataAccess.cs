using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TripEBuy.Common;
using TripEBuy.IDal;

namespace TripEBuy.DalFactory
{
    public class DataAccess
    {
        private static readonly string path = "TripEBuy";
        private DataAccess()
        {
        }
        private static void SetAssemblyBySQLMode(ref string className, out string classPath)
        {
            classPath = null;
            string sqlmodename = ConfigurationManager.AppSettings["sqlmode"];

            switch (sqlmodename)
            {
                case "SQLServer":
                    classPath = path + ".SQLServerDAL";
                    className = string.Format("{0}.SQLServerDAL.{1}", path, className);
                    break;
                case "Mongodb":
                    classPath = path + ".MongodbDAL";
                    className = string.Format("{0}.MongodbDAL{1}", path, className);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="SQLMode"></param>
        /// <returns></returns>
        public static IUserRepository UserInfo()
        {
            string className = "UserRepository";
            string classPath;

            SetAssemblyBySQLMode(ref className, out classPath);

            try
            {
                return Assembly.Load(classPath).CreateInstance(className) as IUserRepository;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}