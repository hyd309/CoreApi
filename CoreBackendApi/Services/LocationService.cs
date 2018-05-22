using System;
using CoreBackendApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlClient;
using CoreBackendApi.TableStore;
using Microsoft.Extensions.Options;
using CoreBackendApi.Common;

namespace CoreBackendApi.Services
{
    public class LocationService : ILocationService
    {
        private readonly BidoContext _bidoContext;
        private readonly TableStoreModel tableStoreModel;

        public LocationService(BidoContext bidoContext, IOptions<TableStoreModel> option)
        {
            _bidoContext = bidoContext;
            tableStoreModel = option.Value;
        }

        public dynamic GetLocation(string deviceId, DateTime dtStart, DateTime dtEnd)
        {
            //string sql = "select top 2 * from Device_Location_"+ dtStart.ToString("yyyyMMdd")+ " where device_code =@deviceId ";
            //SqlParameter[] param = new SqlParameter[] {
            //    new SqlParameter("@deviceId", deviceId)
            //};
            //return _bidoContext.Set<DeviceLocation>().FromSql(sql,param).ToList();

            OtsLocation location = new OtsLocation(tableStoreModel.GetOTSClientLocation());
            return location.GetLocation(Convert.ToInt64(deviceId),TimeHelper.ConvertDateTimeToInt(dtStart), TimeHelper.ConvertDateTimeToInt(dtEnd));
        }
    }
}
