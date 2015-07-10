using System.Web;
using System.Security;
namespace TripEBuy.Common
{
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/"),
     System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public class SOAPHeader : System.Web.Services.Protocols.SoapHeader
    {
        public SOAPHeader()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string useraname;
        public string password;
    }
}