using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace TopOne.Web.APIs.EnterpriseAdminCommon
{
    public class XmlHelper
    {
        /// <summary>
        /// 读入一个文件，并按XML的方式反序列化对象。
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码方式，为null时默认为：Encoding.UTF8</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserializeFromFile<T>(string path, Encoding encoding)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            string xml = string.Empty;
            try
            {
                xml = File.ReadAllText(path, encoding);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("读取指定文件出错！错误消息为：{0}", ex.Message));
            }

            return XmlDeserialize<T>(xml, encoding);
        }

        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="s">包含对象的XML字符串</param>
        /// <param name="encoding">编码方式，为null时默认为：Encoding.UTF8</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserialize<T>(string strXml, Encoding encoding)
        {
            if (string.IsNullOrEmpty(strXml))
            {
                throw new ArgumentNullException("strXml");
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            XmlSerializer mySerializer = new XmlSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(strXml)))
            {
                using (StreamReader sr = new StreamReader(ms, encoding))
                {
                    return (T)mySerializer.Deserialize(sr);
                }
            }
        }
    }
}