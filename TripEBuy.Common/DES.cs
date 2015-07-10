using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TripEBuy.Common
{
    public class DES
    {
        private static byte[] Keys = new byte[]
		{
			18,
			52,
			86,
			120,
			144,
			171,
			205,
			239
		};

        private static string keys = "123qwe#$";

        public static string EncryptDES(string encryptString, string encryptKey)
        {
            string result;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = DES.Keys;
                byte[] bytes2 = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write);
                cryptoStream.Write(bytes2, 0, bytes2.Length);
                cryptoStream.FlushFinalBlock();
                result = Convert.ToBase64String(memoryStream.ToArray());
            }
            catch
            {
                result = "";
            }
            return result;
        }

        public static string EncryptDES(string encryptString)
        {
            return DES.EncryptDES(encryptString, DES.keys);
        }

        public static string DecryptDES(string decryptString, string decryptKey)
        {
            string result;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = DES.Keys;
                byte[] array = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Write);
                cryptoStream.Write(array, 0, array.Length);
                cryptoStream.FlushFinalBlock();
                result = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch
            {
                result = "";
            }
            return result;
        }

        public static string DecryptDES(string decryptString)
        {
            return DES.DecryptDES(decryptString, DES.keys);
        }
    }
}