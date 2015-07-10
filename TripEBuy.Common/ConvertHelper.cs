using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script;
using System.Web.Script.Serialization;

namespace TripEBuy.Common
{
    public static class ConvertHelper
    {
        #region Methods

        public static System.Data.DataTable Clone(this System.Data.DataTable copyDt, string tableName)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable(tableName);
            foreach (System.Data.DataColumn dataColumn in copyDt.Columns)
            {
                dataTable.Columns.Add(dataColumn.ColumnName, dataColumn.DataType);
            }
            dataTable.AcceptChanges();
            return dataTable;
        }

        public static System.Data.DataTable Copy(this System.Data.DataTable copyDt, string tableName)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable(tableName);
            foreach (System.Data.DataColumn dataColumn in copyDt.Columns)
            {
                dataTable.Columns.Add(dataColumn.ColumnName, dataColumn.DataType);
            }
            foreach (System.Data.DataRow row in copyDt.Rows)
            {
                dataTable.BeginInit();
                dataTable.ImportRow(row);
                dataTable.EndInit();
            }
            dataTable.AcceptChanges();
            return dataTable;
        }

        public static object GetValue<T>(this object value, string propertyName)
        {
            object result = null;
            Type typeFromHandle = typeof(T);
            PropertyInfo[] properties = typeFromHandle.GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name == propertyName)
                {
                    result = properties[i].GetValue(value, null);
                    break;
                }
            }
            return result;
        }

        public static string OrderStatusConvert(string OrderStatus)
        {
            string result = null;
            if (OrderStatus == "待支付")
            {
                result = "1";
            }
            else if (OrderStatus == "处理中")
            {
                result = "2";
            }
            else if (OrderStatus == "已完成")
            {
                result = "3";
            }
            else if (OrderStatus == "已撤销")
            {
                result = "4";
            }
            else
            {
                result = "";
            }
            return result;
        }

        public static string PayStatusConvert(string OrderStatus)
        {
            string result = null;
            if (OrderStatus == "待支付")
            {
                result = "1";
            }
            else if ((OrderStatus == "处理中") || (OrderStatus == "已受理") || (OrderStatus == "已完成"))
            {
                result = "2";
            }
            else if (OrderStatus == "已撤销")
            {
                result = "3";
            }
            else
            {
                result = "";
            }
            return result;
        }

        public static string RefundConvert(string OrderStatus)
        {
            string result = null;
            if (OrderStatus == "不退款")
            {
                result = "1";
            }
            else if (OrderStatus == "退款中")
            {
                result = "2";
            }
            else if (OrderStatus == "已退款")
            {
                result = "3";
            }
            else
            {
                result = "1";
            }
            return result;
        }

        public static System.Data.DataTable SetAdded(this System.Data.DataTable dt)
        {
            System.Data.DataTable result;
            if (dt != null)
            {
                dt.AcceptChanges();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Data.DataRow dataRow = dt.Rows[i];
                    dataRow.SetAdded();
                }
                result = dt;
            }
            else
            {
                result = null;
            }
            return result;
        }

        public static bool ToBool(this object obj)
        {
            bool result = false;
            if (obj != null && obj.ToString() != string.Empty)
            {
                if (obj.ToString() == "0")
                {
                    result = Convert.ToBoolean("False");
                    return result;
                    
                }
                if (obj.ToString() == "1")
                {
                    result = Convert.ToBoolean("True");
                    return result;
                }

                result = Convert.ToBoolean(obj);
            }
            return result;
        }

        public static DateTime? ToDateTime(this object obj)
        {
            DateTime? result = null;
            if (obj != null && obj.ToString() != string.Empty)
            {
                result = new DateTime?(Convert.ToDateTime(obj));
                if (result.Value == DateTime.Parse("0001/1/1 0:00:00"))
                {
                    return null;
                }
            }
            else
            {
                return DateTime.Now;
            }
            return result;
        }

        public static DateTime ToDateTime(this string obj, bool isReturnDefault)
        {
            DateTime result;

            if (!DateTime.TryParse(obj, out result))
            {
                if (isReturnDefault)
                {
                    result = DateTime.Now;
                }
            }

            return result;
        }

        /// <summary></summary>
        /// <param name="obj"></param>
        /// <param name="format">
        /// 1: yyyy-MM-dd
        /// 2: yyyy-MM-dd HH mm
        /// </param>
        /// <returns></returns>
        public static string ToDateTime(this DateTime obj, int format)
        {
            string result = string.Empty;

            switch (format)
            {
                case 1:
                    result = obj.ToString("yyyy-MM-dd");
                    break;

                case 2:
                    result = obj.ToString("yyyy-MM-dd HH mm");
                    break;
            }

            return result;
        }

        public static decimal ToDecimal(this object obj)
        {
            decimal result = 0m;
            if (obj != null && obj.ToString() != string.Empty)
            {
                result = Convert.ToDecimal(obj);
            }
            return result;
        }

        public static double ToDouble(this object obj)
        {
            double result = 0.0;
            if (obj != null && obj.ToString() != string.Empty)
            {
                result = Convert.ToDouble(obj);
            }
            return result;
        }

        public static double ToFormatDiscount(this double obj)
        {
            return Math.Floor(obj * 10000.0) / 10000.0;
        }

        public static double ToFormatMoney(this double obj)
        {
            int num = Convert.ToInt32(obj);
            if (obj - (double)num >= 0.49)
            {
                num++;
            }
            return (double)num;
        }

        public static int ToInt32(this object obj)
        {
            int result = 0;
            if (obj != null && obj.ToString() != string.Empty)
            {
                try
                {
                    result = Convert.ToInt32(obj);
                }
                catch
                {
                }
            }
            return result;
        }

        public static string ToString(this int obj, int length, string chr)
        {
            string text = obj.ToString2();
            while (text.Length < length)
            {
                text = chr + text;
            }
            return text;
        }

        public static string ToString2(this object obj)
        {
            string result = string.Empty;
            if (obj != null && obj.ToString() != string.Empty)
            {
                try
                {
                    result = Convert.ToString(obj);
                }
                catch
                {
                }
            }
            return result;
        }

        public static System.Data.DataTable ToTable<T>(this List<T> list)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            System.Data.DataTable dataTable = new System.Data.DataTable(typeof(T).Name);
            for (int i = 0; i < properties.Length; i++)
            {
                dataTable.Columns.Add(properties[i].Name);
                dataTable.Columns[properties[i].Name].DataType = properties[i].PropertyType;
            }
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T t = list[i];
                    PropertyInfo[] properties2 = t.GetType().GetProperties();
                    System.Data.DataRow dataRow = dataTable.NewRow();
                    for (int j = 0; j < properties2.Length; j++)
                    {
                        dataRow[properties2[j].Name] = properties2[j].GetValue(list[i], null);
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }



        /// <summary>
        /// 将json数据反序列化为Dictionary
        /// </summary>
        /// <param name="jsonData">json数据</param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(string jsonData)
        {
            //实例化JavaScriptSerializer类的新实例
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                return jss.Deserialize<Dictionary<string, object>>(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
  
 
    } 
}
        #endregion Methods

