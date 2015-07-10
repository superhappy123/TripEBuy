using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripEBuy.Common;

namespace TripEBuy.Model
{
    public class UserInfoResponse : BaseResponseModel
    {
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Display(Name = "用户性别")]
        public bool UserGender { get; set; }
    }
}