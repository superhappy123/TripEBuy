using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TripEBuy.Bll;
using TripEBuy.Model;

namespace TripEBuy.WebApi.Controllers
{
    public class UserInfoController : ApiController
    {
        private object ProcessUserInfoRequest(object input)
        {
            object output = null;

            try
            {
                UserBll userBll = new UserBll();
                output = userBll.ProcessRequest(input, Common.RequestMode.Add);
            }
            catch (Exception ex)
            {

            }

            return output;
        }

        [HttpPost]
        [ActionName("LogIn")]
        public UserInfoResponse LogIn(UserInfoRequest request)
        {
            UserInfoResponse response = new UserInfoResponse();

            try
            {
                response = ProcessUserInfoRequest(request) as UserInfoResponse;
            }
            catch (Exception ex)
            {

            }

            return response;
        }
    }
}