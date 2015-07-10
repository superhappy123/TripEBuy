using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripEBuy.IDal;
using TripEBuy.Model;

namespace TripEBuy.SqlServerDal
{
    public class UserRepository : IUserRepository
    {
        public UserInfoResponse LogIn(UserInfoRequest user)
        {
            UserInfoResponse userR = new UserInfoResponse();
            userR.UserName = "登录成功，用户名：" + user.UserName;
            userR.UserGender = user.UserGender;
            return userR;
        }
    }
}