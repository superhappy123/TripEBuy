using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripEBuy.Model
{
    public class UserAccountInfo
    {
        [Display(Name = "企业会员")]
        public int CustomerID { get; set; }

        [Display(Name = "企业id")]
        public int ProtocolCustomerID { get; set; }

        [Display(Name = "企业总部id")]
        public int AttributeHeadID { get; set; }

        //账号类型  1：订车人 2：分公司账号 3：总部账号
        public int Accounttpe { get; set; }
    }
}
