using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackendApi.Services
{
    public class UserService : IUserService
    {
        public bool AuthUserPwd(string username, string password)
        {

            return true;
        }

        public bool AuthApp(string appcode)
        {

            return true;
        }
    }
}
