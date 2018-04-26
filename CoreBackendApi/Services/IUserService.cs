using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackendApi.Services
{
    public interface IUserService
    {
        bool Auth(string username, string password);
    }
}
