using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripEBuy.Common;
using TripEBuy.IDal;
using TripEBuy.Model;

namespace TripEBuy.Bll
{
    public class UserBll
    {
        private IUserRepository dal;

        public UserBll()
        {
            dal = TripEBuy.DalFactory.DataAccess.UserInfo();
        }

        public object ProcessRequest(object input, RequestMode reqMod)
        {
            object output = null;

            switch (reqMod)
            {
                case RequestMode.Add:
                    output = Userlogin(input);
                    break;
            }

            return output;
        }

        public UserInfoResponse Userlogin(object input)
        {
            UserInfoRequest inputM = input as UserInfoRequest;
            return dal.LogIn(inputM);
        }
    }
}