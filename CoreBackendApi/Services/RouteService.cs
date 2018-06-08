using CoreBackendApi.Common;
using CoreBackendApi.Models;
using Microsoft.Extensions.Options;
using System;
using CoreBackendApi.TableStore;

namespace CoreBackendApi.Services
{
    public class RouteService: IRouteService
    {
        private readonly BidoContext _bidoContext;
        private readonly TableStoreModel tableStoreModel;

        public RouteService(BidoContext bidoContext, IOptions<TableStoreModel> option)
        {
            _bidoContext = bidoContext;
            tableStoreModel = option.Value;
        }

        public dynamic GetRoutes(string deviceId, DateTime dtStart, DateTime dtEnd)
        {
            OtsRoute ots = new OtsRoute(tableStoreModel.GetOTSClientRoute());
            return ots.GetRoutes(Convert.ToInt64(deviceId), TimeHelper.ConvertDateTimeToInt(dtStart), TimeHelper.ConvertDateTimeToInt(dtEnd));
        }
    }
}
