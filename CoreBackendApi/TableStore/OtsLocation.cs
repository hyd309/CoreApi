using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Aliyun.OTS.DataModel;
using Aliyun.OTS;
using CoreBackendApi.Common;
using Microsoft.Extensions.Options;
using Aliyun.OTS.Request;

namespace CoreBackendApi.TableStore
{
    public class OtsLocation
    {
        private OTSClient otsClient;

        string tableName = "L_100000000";
        string pkDeviceID = "d";
        string pkTime = "t";
        string attribute = "l";
        public OtsLocation(OTSClient client)
        {
            otsClient = client;
        }

        public dynamic GetLocation(long deviceId, long start, long end)
        {
            PrimaryKey inclusiveStartPrimaryKey = new PrimaryKey();
            inclusiveStartPrimaryKey.Add(pkDeviceID, new ColumnValue(deviceId));
            inclusiveStartPrimaryKey.Add(pkTime, new ColumnValue(start));

            PrimaryKey exclusiveEndPrimaryKey = new PrimaryKey();
            exclusiveEndPrimaryKey.Add(pkDeviceID, new ColumnValue(deviceId));
            exclusiveEndPrimaryKey.Add(pkTime, new ColumnValue(end));
            //_logger.LogDebug(string.Format("deviceId={0},start={1},end={2}",deviceId,start,end));
            List<LocationModel> list = new List<LocationModel>();
            try
            {
                var cu = new CapacityUnit(0, 0);
                var request = new GetIteratorRequest(tableName, GetRangeDirection.Forward,inclusiveStartPrimaryKey,exclusiveEndPrimaryKey, cu);
                var iterator = otsClient.GetRangeIterator(request);

                // 遍历迭代器，读取数据
                foreach (var row in iterator)
                {
                    LocationModel locationModel = new LocationModel();
                    locationModel.device_code = deviceId;
                    // 处理逻辑
                    var pks=row.PrimaryKey;
                    foreach (var pk in pks)
                    {
                        if (pk.Key == pkTime)
                        {
                            locationModel.gps_time = TimeHelper.ConvertLongToDateTime(pk.Value.IntegerValue);
                        }
                    }
                    foreach (var attr in row.Attribute)
                    {
                        if (attr.Key == attribute)
                        {
                            byte[] lbyte = attr.Value.BinaryValue;
                            Dictionary<string, int> dictionary = ByteIntHelper.GetLocationByByte(lbyte);
                            locationModel.latitude = dictionary["latitude"];
                            locationModel.longitude = dictionary["longitude"];
                            locationModel.speed = dictionary["speed"];
                            locationModel.direct = dictionary["direct"];
                            locationModel.height = dictionary["height"];
                        }
                    }
                    list.Add(locationModel);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return list.OrderBy(p=>p.gps_time).ToList();
        }
    }
}
