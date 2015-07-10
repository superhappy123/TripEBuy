using System;
using System.Text;

/// <summary>
/// Summary description for Utilities
/// </summary>

namespace TripEBuy.Common
{
    public class Utilities
    {
        public Utilities()
        {
        }

        public const string OK = "0000";
        public const string SYSTEM_ERROR = "9999";
        public const string SESSTIONTIMEOUT = "1000";
        public const string INVALIDPATH = "1001";
        public const string DUPLICATERECORD = "1002";

        public DetailInfo getFuncStatus(DetailInfoSeverity serv, string stsCode, string desc)
        {
            DetailInfo status = new DetailInfo();

            status.Severity = serv;
            status.StatusCode = stsCode;
            status.StatusDesc = desc;

            return status;
        }

        public CommonRs getCommonRs()
        {
            CommonRs rs = new CommonRs();

            rs.ProcMode = ProcMode.ONLINE;
            return rs;
        }

        public TraceServiceProvider getSvcProviderInfo()
        {
            TraceServiceProvider result = new TraceServiceProvider();

            result.SPDateTime = DateTime.Now;
            result.SPId = "TopOne.Web.APIs.EnterpriseAdminTrafficViolationAgency";
            result.SPUID = "TopOne.Web.APIs.EnterpriseAdminTrafficViolationAgency";

            return result;
        }

        public string RemoveSoapPrefix(string EmsMsg)
        {
            StringBuilder sBd = new StringBuilder(EmsMsg);

            sBd.Replace("<soap:", "<");
            sBd.Replace("</soap:", "</");
            sBd.Replace("xmlns:soap=", "xmlns=");

            return System.Convert.ToString(sBd);
        }

        public enum SeverityLevel
        {
            Info,
            Warn,
            Error
        }
    }
}