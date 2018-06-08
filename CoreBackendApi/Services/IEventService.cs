using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackendApi.Services
{
    public interface IEventService
    {
        dynamic GetEvents(string deviceId, DateTime dtStart, DateTime dtEnd,int eventId);
    }
}
