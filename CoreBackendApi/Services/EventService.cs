using CoreBackendApi.Common;
using CoreBackendApi.Models;
using CoreBackendApi.TableStore;
using Microsoft.Extensions.Options;
using System;

namespace CoreBackendApi.Services
{
    public class EventService : IEventService
    {
        private readonly BidoContext _bidoContext;
        private readonly TableStoreModel tableStoreModel;

        public EventService(BidoContext bidoContext, IOptions<TableStoreModel> option)
        {
            _bidoContext = bidoContext;
            tableStoreModel = option.Value;
        }

        public dynamic GetEvents(string deviceId, DateTime dtStart, DateTime dtEnd, int eventId)
        {
            OtsEvent location = new OtsEvent(tableStoreModel.GetOTSClientEvent());
            return location.GetEvents(Convert.ToInt64(deviceId), TimeHelper.ConvertDateTimeToInt(dtStart), TimeHelper.ConvertDateTimeToInt(dtEnd), eventId);
        }
    }
}
