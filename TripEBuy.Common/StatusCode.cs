using System;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;

namespace TripEBuy.Common
{
    ///<summary>
    /// 错误类型
    ///</summary>
    public class StatusCode
    {

        public StatusCode()
        {

        }

        public StatusCode(int code)
            : this(code, string.Empty, string.Empty)
        {

        }


        ///<summary>
        /// 构造方法
        ///</summary>
        ///<param name="code">错误编码</param>
        ///<param name="name">错误标题</param>
        ///<param name="description">错误描述</param>
        public StatusCode(long code, string name, string description)
        {
            Code = code;
            Name = name;
            Description = description;
        }

        #region "DIM"

        ///<summary>
        /// 错误的十六进制错误代码
        ///</summary>
        public long Code { get; set; }

        public string Name { get; set; }

        ///<summary>
        /// 错误的描述
        ///</summary>
        public string Description { get; set; }
        #endregion



        ///<summary>
        ///   请求处理成功
        ///</summary>
        public static StatusCode SYS_SUCCESS
        {
            get
            {
                return new StatusCode(0000000, "SYS_SUCCESS", "请求处理成功");
            }
        }

        ///<summary>
        ///   请求处理失败
        ///</summary>
        public static StatusCode SYSTEM_EXCEPTION
        {
            get
            {
                return new StatusCode(9999999, "SYSTEM_EXCEPTION", "请求处理失败");
            }
        }

        ///<summary>
        ///   参数验证失败
        ///</summary>
        public static StatusCode VIOLATION_FAIL
        {
            get
            {
                return new StatusCode(9999998, "VIOLATION_FAIL", "参数验证失败");
            }
        }

        ///<summary>
        ///   签名验证失败
        ///</summary>
        public static StatusCode SIGN_EXCEPTION
        {
            get
            {
                return new StatusCode(9999997, "SIGN_EXCEPTION", "签名验证失败");
            }
        }

        ///<summary>
        ///   没有查询到车牌品牌信息
        ///</summary>
        public static StatusCode DID_NOT_FIND_THIS_BRAND
        {
            get
            {
                return new StatusCode(1000001, "DID_NOT_FIND_THIS_BRAND", "没有查询到车牌品牌信息");
            }
        }

        public static StatusCode DID_NOT_FIND_THIS_Admin
        {
            get
            {
                return new StatusCode(1000002, "DID_NOT_FIND_THIS_Admin", "没有获取到此管理员所在企业的账户信息");
            }
        }

        ///<summary>
        ///   没有获取到可查询违章的城市列表
        ///</summary>
        public static StatusCode DID_NOT_FIND_THIS_VIOLATION_CITY
        {
            get
            {
                return new StatusCode(1000003, "DID_NOT_FIND_THIS_VIOLATION_CITY", "没有获取到可查询违章的城市列表");
            }
        }

        ///<summary>
        ///   添加员工信息失败
        ///</summary>
        public static StatusCode Add_Staff_FAILED
        {
            get
            {
                return new StatusCode(1000004, "Add_Staff_FAILED", "添加员工信息失败");
            }
        }

        ///<summary>
        ///   删除员工失败
        ///</summary>
        public static StatusCode Del_Staff_FAILED
        {
            get
            {
                return new StatusCode(1000005, "Del_Staff_FAILED", "删除员工失败");
            }
        }

        ///<summary>
        ///   修改员工失败
        ///</summary>
        public static StatusCode UPDATE_staff_FAILED
        {
            get
            {
                return new StatusCode(1000006, "UPDATE_staff_FAILED", "修改员工失败");
            }
        }
        ///<summary>
        ///   退出
        ///</summary>
        public static StatusCode User_LoGin_Out_FAILED
        {
            get
            {
                return new StatusCode(1000007, "User_LoGin_Out_FAILED", "退出失败");
            }
        }


        ///<summary>
        ///   修改员工失败
        ///</summary>
        public static StatusCode Find_staff_Fail
        {
            get
            {
                return new StatusCode(1000070, "Find_staff_Fail", "没有找到对应的账号信息");
            }
        }

        ///<summary>
        ///   修改员工失败
        ///</summary>
        public static StatusCode DID_NOT_FIND_THIS_STAFF_ACCOUNT_INFO
        {
            get
            {
                return new StatusCode(1000071, "DID_NOT_FIND_THIS_STAFF_ACCOUNT_INFO", "没有找到员工信息");
            }
        }

        ///<summary>
        ///   删除员工失败
        ///</summary>
        public static StatusCode DELETE_Staff_FAILED
        {
            get
            {
                return new StatusCode(1000007, "DELETE_Staff_FAILED", "删除员工失败");
            }
        }

        ///<summary>
        ///   没有找到用户列表信息
        ///</summary>
        public static StatusCode DID_NOT_FIND_Stafflist
        {
            get
            {
                return new StatusCode(1000008, "DID_NOT_FIND_Stafflist", "没有找到用户列表信息");
            }
        }

        ///<summary>
        ///   查询审核人列表失败
        ///</summary>
        public static StatusCode DID_NOT_FIND_AuditorList
        {
            get
            {
                return new StatusCode(1000009, "DID_NOT_FIND_AuditorList", "查询审核人列表失败");
            }
        }

        ///<summary>
        ///   没有查询到车牌订单信息
        ///</summary>
        public static StatusCode DID_NOT_FIND_THIS_ORDER
        {
            get
            {
                return new StatusCode(1000010, "DID_NOT_FIND_THIS_ORDER", "没有查询到车牌订单信息");
            }
        }

        ///<summary>
        ///   没有查询到用户订单信息
        ///</summary>
        public static StatusCode DID_NOT_FIND_THIS_USER_ORDER
        {
            get
            {
                return new StatusCode(1000011, "DID_NOT_FIND_THIS_USER_ORDER", "没有查询到用户订单信息");
            }
        }

        ///<summary>
        ///   没有查询到对应的员工信息
        ///</summary>
        public static StatusCode DID_NOT_FIND_THIS_Staff
        {
            get
            {
                return new StatusCode(1000012, "DID_NOT_FIND_THIS_Staff", "没有查询到对应的员工信息");
            }
        }

        ///<summary>
        ///   没有查询到用户车辆列表信息
        ///</summary>
        public static StatusCode DID_NOT_FIND_THIS_CAR_LIST
        {
            get
            {
                return new StatusCode(1000013, "DID_NOT_FIND_THIS_CAR_LIST", "没有查询到用户车辆列表信息");
            }
        }

        ///<summary>
        ///   没有找到对应的车辆信息
        ///</summary>
        public static StatusCode DID_NOT_FIND_THIS_CAR
        {
            get
            {
                return new StatusCode(1000014, "DID_NOT_FIND_THIS_CAR", "没有找到对应的车辆信息");
            }
        }

        ///<summary>
        ///   更新订单状态失败
        ///</summary>
        public static StatusCode UPDATE_ORDER_STATUS_FAILED
        {
            get
            {
                return new StatusCode(1000015, "UPDATE_ORDER_STATUS_FAILED", "更新订单状态失败");
            }
        }

        ///<summary>
        ///   更新车辆拓展信息失败
        ///</summary>
        public static StatusCode UPDATE_VOITURE_INFO_FAILED
        {
            get
            {
                return new StatusCode(1000016, "UPDATE_VOITURE_INFO_FAILED", "更新车辆拓展信息失败");
            }
        }

        ///<summary>
        ///   查询车辆拓展信息失败
        ///</summary>
        public static StatusCode QUERY_VOITURE_INFO_FAILED
        {
            get
            {
                return new StatusCode(1000017, "QUERY_VOITURE_INFO_FAILED", "查询车辆拓展信息失败");
            }
        }

        ///<summary>
        ///   查询长租城市信息失败
        ///</summary>
        public static StatusCode QUERY_CITY_INFO_FAILED
        {
            get
            {
                return new StatusCode(1000018, "QUERY_CITY_INFO_FAILED", "查询长租城市信息失败");
            }
        }

        ///<summary>
        ///   查询长租车型信息失败
        ///</summary>
        public static StatusCode QUERY_CAR_MODEL_INFO_FAILED
        {
            get
            {
                return new StatusCode(1000019, "QUERY_CAR_MODEL_INFO_FAILED", "查询长租车型信息失败");
            }
        }

        ///<summary>
        ///   查询长租订单详情失败
        ///</summary>
        public static StatusCode QUERY_LONGRENT_ORDER_DETAIL_FAILED
        {
            get
            {
                return new StatusCode(1000020, "QUERY_LONGRENT_ORDER_DETAIL_FAILED", "查询长租车型信息失败");
            }
        }

        ///<summary>
        ///   新增长租订单失败
        ///</summary>
        public static StatusCode QUERY_LONGRENT_ADD_ORDER_FAILED
        {
            get
            {
                return new StatusCode(1000021, "QUERY_LONGRENT_ADD_ORDER_FAILED", "新增长租订单失败");
            }
        }

        ///<summary>
        ///   查询长租订单列表失败
        ///</summary>
        public static StatusCode QUERY_LONGRENT_ORDER_LIST_FAILED
        {
            get
            {
                return new StatusCode(1000022, "QUERY_LONGRENT_ORDER_LIST_FAILED", "查询长租订单列表失败");
            }
        }
        ///<summary>
        ///   查询长租订单列表失败
        ///</summary>
        public static StatusCode QUERY_MonthlyReport_FAILED
        {
            get
            {
                return new StatusCode(1000023, "QUERY_MonthlyReport_FAILED", "没有获取月度消费报表数据");
            }
        }
        public static StatusCode QUERY_Department_FAILED
        {
            get
            {
                return new StatusCode(1000024, "QUERY_Department_FAILED", "没有获取部门列表数据");
            }
        }
        public static StatusCode QUERY_BranchList_FAILED
        {
            get
            {
                return new StatusCode(1000025, "QUERY_BranchList_FAILED", "没有获取分公司数据");
            }
        }
        public static StatusCode QUERY_AccountDetail_FAILED
        {
            get
            {
                return new StatusCode(1000026, "QUERY_AccountDetail_FAILED", "获取活期账户明细");
            }
        }
        public static StatusCode QUERY_SelfDriverOrderList_FAILED
        {
            get
            {
                return new StatusCode(1000027, "QUERY_SelfDriverOrderList_FAILED", "获取自驾订单列表失败");
            }
        }
        public static StatusCode QUERY_ChauffeurDriveOrderList_FAILED
        {
            get
            {
                return new StatusCode(1000028, "QUERY_ChauffeurDriveOrderList_FAILED", "获取代驾订单列表失败");
            }
        }

        public static StatusCode QUERY_TransactionList_FAILED
        {
            get
            {
                return new StatusCode(1000029, "QUERY_TransactionList_FAILED", "获取消费列表失败");
            }
        }
       
       




        public class StatusCodeHelper
        {
            public static StatusCode GetStatusCode(string code)
            {
                try
                {
                    Assembly classSampleAssembly = null;
                    classSampleAssembly = Assembly.GetExecutingAssembly();

                    Type classSampleType = classSampleAssembly.GetType("TripEBuy.Common");
                    StatusCode s1 = Activator.CreateInstance(classSampleType) as StatusCode;

                    StatusCode returnValue4 = classSampleType.InvokeMember(code,
                    BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Static,
                    null,
                    s1,
                    new object[] { }) as StatusCode;

                    return returnValue4;
                }
                catch
                {

                }
                return StatusCode.SYSTEM_EXCEPTION;
            }
        }
    }
}