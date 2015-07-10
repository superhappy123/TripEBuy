using System.Configuration;

namespace TripEBuy.Common
{
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;

    public enum ToponeMD5Type
    {
        Upper,

        Lower
    }

    public class Sign
    {
        #region Methods

        public static string GetSign(SortedDictionary<string, string> dicArray, ref string linkString)
        {
            linkString = string.Empty;

            linkString = CreateLinkString(dicArray);

            string vendorsecret = ConfigurationManager.AppSettings["secret"];
            string linkStringKey = string.Format("{0}{1}{0}", vendorsecret, linkString);

           //参数中bool类型的转换
            linkStringKey = linkStringKey.Replace("True", "true");
            linkStringKey = linkStringKey.Replace("False", "false");


            return ToponeMD5.GetUpper(linkStringKey);
        }

        private static string CreateLinkString(SortedDictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                if (temp.Key.ToLower() != "sign")
                {
                    prestr.Append(temp.Key);
                    prestr.Append(temp.Value);
                }
            }
            return prestr.ToString();
        }

        #endregion Methods
    }

    public class ToponeMD5
    {
        #region Methods

        public static string Get(string encryptString, ToponeMD5Type type)
        {
            MD5 md = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(encryptString);
            byte[] buffer2 = md.ComputeHash(bytes);
            string str2 = null;
            for (int i = 0; i < buffer2.Length; i++)
            {
                string str3 = buffer2[i].ToString("x2");
                str2 = str2 + str3;
            }
            if (type == ToponeMD5Type.Upper)
            {
                return str2.ToUpper();
            }
            else
            {
                return str2.ToLower();
            }
        }

        public static string GetLower(string encryptString)
        {
            return Get(encryptString, ToponeMD5Type.Lower);
        }

        public static string GetUpper(string encryptString)
        {
            return Get(encryptString, ToponeMD5Type.Upper);
        }

        #endregion Methods
    }
}