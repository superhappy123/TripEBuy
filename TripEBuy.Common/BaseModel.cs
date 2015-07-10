using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization;
namespace TripEBuy.Common
{

    public class BaseRequestModel
    {
        /// <summary>
        /// 系统级（必选）入参：每次调用服务的请求号
        /// </summary>
        public string req_id { get; set; }

        /// <summary>
        /// 系统级（必选）入参：商户标识号，渠道ChannelID加密后的字符串
        /// </summary>

        public string access_token { get; set; }

        /// <summary>
        /// 系统级（必选）入参：API输入参数签名结果
        /// </summary>
        public string sign { get; set; }


    }

    public class LoginBaseRequestModel
    {
        /// <summary>
        /// 系统级（必选）入参：每次调用服务的请求号
        /// </summary>
        public string req_id { get; set; }



        /// <summary>
        /// 系统级（必选）入参：API输入参数签名结果
        /// </summary>
        public string sign { get; set; }


    }

    //[DataContract]
    public class BaseResponseModel
    {
        /// <summary>
        /// 系统、业务、方法各级别的响应、处理结果的状态代码（不对系统外用户公开）
        /// </summary>
        [IgnoreDataMember]
        public StatusCode _StatusCode { get; set; }

        /// <summary>
        /// 系统级返参：响应请求后的返回代码
        /// </summary>
        public string code { get { if (_StatusCode == null) { return string.Empty; } else { return _StatusCode.Code.ToString(); }; } }

        /// <summary>
        /// 系统级返参：响应请求后的返回消息（对ReturnCode的说明）
        /// </summary>
        public string message { get { if (_StatusCode == null) { return string.Empty; } else { return _StatusCode.Description; }; } }
    }

    public enum EnumResult
    {
        OK = 0,

        ERROR_PRICEMARK,

        ERROR,
    }

    public enum SQLMode
    {
        SQLServer,
        Mongodb,
    }

    public enum RequestMode
    {
        Add,
        Update,
        Delete,
        QuerybyID,
        QuerybyItem,
        QuerybyAll,
        Detail,
        Approve,
        Reject,
        Report,
    }

    public static class EntityData
    {
        public static void BindData<T>(T entity, DataRow dr)
        {
            if (entity == null || dr == null || dr.Table == null)
            {
                return;
            }
            Type objType = typeof(T);
            PropertyInfo[] pList = objType.GetProperties();

            foreach (PropertyInfo item in pList)
            {
                if (dr.Table.Columns.Contains(item.Name) == false)
                {
                    continue;
                }

                object value = dr[item.Name];
                if (value == DBNull.Value)
                {
                    continue;
                }

                BindPropertyData<T>(entity, dr, item, value);
            }
        }

        private static void BindPropertyData<T>(T entity, DataRow dr, PropertyInfo item, object value)
        {
            Type dataType = item.PropertyType;
            if (dataType == dr.Table.Columns[item.Name].DataType)
            {
                item.SetValue(entity, value);
            }
            else if (dataType == typeof(string))
            {
                item.SetValue(entity, value.ToString());
            } 
            else if (dataType == typeof(DateTime))
            {
                item.SetValue(entity, value.ToDateTime());
            }
            else if (dataType == typeof(decimal))
            {
                item.SetValue(entity, value.ToDecimal());
            }
            else if (dataType == typeof(double))
            {
                item.SetValue(entity, value.ToDouble());
            }
            else if (dataType == typeof(bool))
            {
                item.SetValue(entity, value.ToBool());
            }
            else if (dataType == typeof(int))
            {
                item.SetValue(entity, value.ToInt32());
            }
            else
            {
                try
                {
                    item.SetValue(entity, value);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}