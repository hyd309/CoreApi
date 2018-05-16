using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackendApi.Services
{
    public interface ILocationService
    {
        dynamic GetLocation(string deviceId, DateTime dtStart, DateTime dtEnd);
    }
}
