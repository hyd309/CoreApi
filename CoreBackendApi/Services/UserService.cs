using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackendApi.Services
{
    public class UserService : IUserService
    {
        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public bool AuthUserPwd(string username, string password)
        {
            log.Info(string.Format("username={0};password={1}", username, password));
            return true;
        }

        public bool AuthApp(string appcode)
        {
            log.Info(string.Format("AuthApp={0}", appcode));
            return true;
        }
    }
}
