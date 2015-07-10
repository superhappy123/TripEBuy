
namespace TripEBuy.Common
{
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class EMSSOAPHeader : System.Web.Services.Protocols.SoapHeader
    {
        //<remarks/>
        public EMSDetail EMSDetail;

        //<remarks/>
        public Trace Trace;

        //<remarks/>
        public MsgRecCtrl MsgRecCtrl;

        //<remarks/>
        public Security Security;

        //<remarks/>
        public InfoWarn InfoWarn;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class EMSDetail
    {
        //<remarks/>
        public string MsgVersion;

        //<remarks/>
        public string MsgUID;

        //<remarks/>
        public string SvcVersion;

        //<remarks/>
        //[System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        // System.ComponentModel.DefaultValueAttribute(false)]
        //public bool mustUnderstand = false;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class DetailInfo
    {
        //<remarks/>
        public DetailInfoSeverity Severity;

        //<remarks/>
        //public StatusCode As DetailInfoStatusCode
        public string StatusCode;

        //<remarks/>
        public string StatusDesc;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public enum DetailInfoSeverity
    {
        //<remarks/>
        Warn,

        //<remarks/>
        Error,

        //<remarks/>
        Info
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public enum DetailInfoStatusCode
    {
        //<remarks/>
        S001,

        //<remarks/>
        S002,

        //<remarks/>
        S003,

        //<remarks/>
        S004,

        //<remarks/>
        S998,

        //<remarks/>
        S999,

        //<remarks/>
        S005,

        //<remarks/>
        A086
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class Fault
    {
        //<remarks/>
        public FaultFaultcode faultcode;

        //<remarks/>
        public string faultstring;

        //<remarks/>
        public string faultactor;

        //<remarks/>
        public DetailInfo detail;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public enum FaultFaultcode
    {
        //<remarks/>
        VersionMismatch,

        //<remarks/>
        MustUnderstand,

        //<remarks/>
        Client,

        //<remarks/>
        Server
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class InfoWarn
    {
        //<remarks/>
        public Fault Fault;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SecuritySecurityContext
    {
        //<remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "hexBinary")]
        public byte[] SecurityToken;

        //<remarks/>
        public SecuritySecurityContextPassword Password;

        //<remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PasswordSpecified;

        //<remarks/>
        public string Algorithm;

        //<remarks/>
        public SecuritySecurityContextDigestAlgorithm DigestAlgorithm;

        //<remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DigestAlgorithmSpecified;

        //<remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "hexBinary")]
        public byte[] DigestValue;

        //<remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "hexBinary")]
        public byte[] SignatureValue;

        //<remarks/>
        public string KeyInfo;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public enum SecuritySecurityContextPassword
    {
        //<remarks/>
        DSA,

        //<remarks/>
        RSA
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public enum SecuritySecurityContextDigestAlgorithm
    {
        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("SHA-1")]
        SHA1,

        //<remarks/>
        MD5
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public  partial  class Security
    {
        //<remarks/>
        public System.SByte MsgIntegrity;

        //<remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MsgIntegritySpecified;

        //<remarks/>
        public SecuritySecurityContext SecurityContext;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class RecCtrlOut
    {
        //<remarks/>
        public long MatchedRec;

        //<remarks/>
        public long SentRec;

        //<remarks/>
        public string Cursor;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class RecCtrlIn
    {
        //<remarks/>
        public long MaxRec;

        //<remarks/>
        public string Cursor;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class MsgRecCtrl
    {
        //<remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RecCtrlIn", typeof(RecCtrlIn))]
        [System.Xml.Serialization.XmlElementAttribute("RecCtrlOut", typeof(RecCtrlOut))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public System.Object Item;

        //<remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public RecCtrlChoiceType ItemElementName;

        //<remarks/>
        [System.Xml.Serialization.XmlTypeAttribute()]
        public enum RecCtrlChoiceType
        {
            //<remarks/>
            RecCtrlIn,

            //<remarks/>
            RecCtrlOut
        }
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class TraceServiceProvider
    {
        //<remarks/>
        public string SPUID;

        //<remarks/>
        public string SPId;

        //<remarks/>
        public System.DateTime SPDateTime;

        //<remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool mustUnderstand = true;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class Operator
    {
        //<remarks/>
        public string OpInternalId;

        //<remarks/>
        public string OpLoginId;

        //<remarks/>
        public OperatorOpRole OpRole;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public enum OperatorOpRole
    {
        //<remarks/>
        TELLER,

        //<remarks/>
        OFFICER1,

        //<remarks/>
        OFFICER2,

        //<remarks/>
        AUTHOFFICER,

        //<remarks/>
        CUSTOMER,

        //<remarks/>
        SYSTEM
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class RqClient
    {
        //<remarks/>
        public string RqClientId;

        //<remarks/>
        public RqClientRqClientOrg RqClientOrg;

        //<remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RqClientOrgSpecified;

        //<remarks/>
        public string RqClientCtry;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public enum RqClientRqClientOrg
    {
        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0001")]
        Item0001,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3001")]
        Item3001
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class Trace
    {
        //<remarks/>
        public System.DateTime RqDateTime;

        //<remarks/>
        public RqClient RqClient;

        //<remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Operator", typeof(Operator))]
        [System.Xml.Serialization.XmlElementAttribute("ServiceProvider", typeof(TraceServiceProvider))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public System.Object Item;

        //<remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName;

        //<remarks/>
        [System.Xml.Serialization.XmlTypeAttribute()]
        public enum ItemChoiceType
        {
            //<remarks/>
            Operator,

            //<remarks/>
            ServiceProvider
        }

        //<remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool mustUnderstand = false;
    }

    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class CommonRq
    {
        //<remarks/>
        public CommonRqChannelId ChannelId;

        //<remarks/>
        public bool EnableSAF;

        //<remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EnableSAFSpecified;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public enum CommonRqChannelId
    {
        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("00")]
        Item00,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06")]
        Item06,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07")]
        Item07,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("08")]
        Item08,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("09")]
        Item09,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("97")]
        Item97,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("98")]
        Item98,

        //<remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("99")]
        Item99
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.dbs.com/icc/mb")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.dbs.com/icc/mb", IsNullable = false)]
    public enum LangCode
    {
        en_US,
        zh_CN,
        zh_TW
    }

    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class CommonRs
    {
        //<remarks/>
        public ProcMode ProcMode;
    }

    //<remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public enum ProcMode
    {
        //<remarks/>
        ONLINE,

        //<remarks/>
        OFFLINE
    }

    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class Phone
    {
        //<remarks/>
        public string PhoneType;

        //<remarks/>
        public string PhoneTypeDesc;

        //<remarks/>
        public string PhoneCtryCode;

        //<remarks/>
        public string PhoneNumber;
    }

    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class Amount
    {
        //<remarks/>
        public decimal Amt;

        //<remarks/>
        public string Cur;
    }

    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class UserInfo
    {
        public string UserID;
        public string UserName;
        public string RoleID;
        public string LocationCode;
        public string UserType;
        public string LocationDesc;
    }

    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public enum ActionTypeInfo
    {
        //<remarks/>
        Inq,

        //<remarks/>
        Del,

        //<remarks/>
        Upd,

        //<remarks/>
        Add
    }
}