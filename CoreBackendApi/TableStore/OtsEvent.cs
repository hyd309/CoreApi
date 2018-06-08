using Aliyun.OTS;
using Aliyun.OTS.DataModel;
using Aliyun.OTS.Request;
using CoreBackendApi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackendApi.TableStore
{
    public class OtsEvent
    {
        private OTSClient otsClient;

        string tableName = "E_100000000";
        string pkDeviceID = "d";
        string pkEventTime = "et";
        string pkEventId = "ei";
        string attrEventParameter = "ep";
        string attrTime = "t";
        string attrLocation = "l";
        public OtsEvent(OTSClient client)
        {
            otsClient = client;
        }

        public dynamic GetEvents(long deviceId, long start, long end,int eventId)
        {
            PrimaryKey inclusiveStartPrimaryKey = new PrimaryKey();
            inclusiveStartPrimaryKey.Add(pkDeviceID, new ColumnValue(deviceId));
            inclusiveStartPrimaryKey.Add(pkEventTime, new ColumnValue(start));

            PrimaryKey exclusiveEndPrimaryKey = new PrimaryKey();
            exclusiveEndPrimaryKey.Add(pkDeviceID, new ColumnValue(deviceId));
            exclusiveEndPrimaryKey.Add(pkEventTime, new ColumnValue(end));
            bool isFilter = false;
            byte[] byteEventId = ByteIntHelper.intToBytes2(eventId, 1);
            if (eventId == int.MinValue)
            {
                inclusiveStartPrimaryKey.Add(pkEventId, ColumnValue.INF_MIN);
                exclusiveEndPrimaryKey.Add(pkEventId, ColumnValue.INF_MAX);
            }
            else
            {
                isFilter = true;
                inclusiveStartPrimaryKey.Add(pkEventId, new ColumnValue(byteEventId));
                exclusiveEndPrimaryKey.Add(pkEventId, new ColumnValue(byteEventId));
            }

            //_logger.LogDebug(string.Format("deviceId={0},start={1},end={2}",deviceId,start,end));
            List<EventModel> list = new List<EventModel>();
            try
            {
                var cu = new CapacityUnit(0, 0);
                var request = new GetIteratorRequest(tableName, GetRangeDirection.Forward, inclusiveStartPrimaryKey, exclusiveEndPrimaryKey, cu);
                var iterator = otsClient.GetRangeIterator(request);
                // 遍历迭代器，读取数据
                foreach (var row in iterator)
                {
                    bool isFilterEvent = false;
                    EventModel model = new EventModel();
                    model.deviceCode = deviceId;
                    // 处理逻辑
                    var pks = row.PrimaryKey;
                    foreach (var pk in pks)
                    {
                        if (pk.Key == pkEventTime)
                        {
                            model.alarmTime = TimeHelper.ConvertLongToDateTime(pk.Value.IntegerValue);
                        }
                        else if (pk.Key == pkEventId)
                        {
                            //if (isFilter && eventId != ByteIntHelper.bytesToInt2(pk.Value.BinaryValue, 0, 1))//等同于下面的判断语句
                            if (isFilter && byteEventId[0] != pk.Value.BinaryValue[0])//判断时间id是否一致;
                            {
                                isFilterEvent = true;
                                break;
                            }
                            model.alarmNo = ByteIntHelper.bytesToInt2(pk.Value.BinaryValue,0,1);
                        }
                    }
                    if (isFilterEvent)
                    {
                        continue;
                    }
                    foreach (var attr in row.Attribute)
                    {
                        if (attr.Key == attrEventParameter)
                        {
                            //事件参数暂未处理；
                        }
                        else if (attr.Key==attrTime)
                        {
                            model.createTime= TimeHelper.ConvertLongToDateTime(attr.Value.IntegerValue);
                        }
                        else if (attr.Key==attrLocation)
                        {
                            byte[] lbyte = attr.Value.BinaryValue;
                            Dictionary<string, int> dictionary = ByteIntHelper.GetLocationByByte(lbyte);
                            model.latitude = dictionary["latitude"];
                            model.longitude = dictionary["longitude"];
                            model.speed = dictionary["speed"];
                            model.direct = dictionary["direct"];
                            model.height = dictionary["height"];
                        }
                    }
                    list.Add(model);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return list.OrderBy(p => p.alarmTime).ToList();
        }
    }
}
