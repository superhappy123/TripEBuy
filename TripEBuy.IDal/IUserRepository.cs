using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripEBuy.Model;

namespace TripEBuy.IDal
{
    public interface IUserRepository
    {
        UserInfoResponse LogIn(UserInfoRequest user);
    }
}
