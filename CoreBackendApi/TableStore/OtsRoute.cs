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
    public class OtsRoute
    {
        private OTSClient otsClient;

        #region 表格存储表明、字段名定义

        string tableName = "Route";

        string pkDeviceID = "d";
        string pkStartTime = "s";
        /// <summary>
        /// 结束时间
        /// </summary>
        string attEndTime = "e";
        /// <summary>
        /// 行程数据
        /// </summary>
        string attRoute = "r";
        /// <summary>
        /// 行驶时长统计
        /// </summary>
        string attDurationStats = "ds";
        /// <summary>
        /// 事件次数统计
        /// </summary>
        string attEventStats = "es";
        #endregion

        #region 事件代码常量定义

        /// <summary>
        /// 18 为急加速事件代码
        /// </summary>
        const int rapidAcceleration_Code = 18;
        /// <summary>
        /// 19 急减速的事件代码
        /// </summary>
        const int rapidDeceleration_Code = 19;
        /// <summary>
        /// 20 急转弯的事件代码
        /// </summary>
        const int sharpTurn_Code = 20;
        #endregion

        public OtsRoute(OTSClient client)
        {
            otsClient = client;
        }

        public dynamic GetRoutes(long deviceId, long start, long end)
        {
            PrimaryKey inclusiveStartPrimaryKey = new PrimaryKey();
            inclusiveStartPrimaryKey.Add(pkDeviceID, new ColumnValue(deviceId));
            inclusiveStartPrimaryKey.Add(pkStartTime, new ColumnValue(start));

            PrimaryKey exclusiveEndPrimaryKey = new PrimaryKey();
            exclusiveEndPrimaryKey.Add(pkDeviceID, new ColumnValue(deviceId));
            exclusiveEndPrimaryKey.Add(pkStartTime, new ColumnValue(end));

            List<RouteModel> list = new List<RouteModel>();
            try
            {
                var cu = new CapacityUnit(0, 0);
                var request = new GetIteratorRequest(tableName, GetRangeDirection.Forward,inclusiveStartPrimaryKey,exclusiveEndPrimaryKey, cu);
                var iterator = otsClient.GetRangeIterator(request);

                // 遍历迭代器，读取数据
                foreach (var row in iterator)
                {
                    RouteModel model = new RouteModel();
                    model.deviceCode = deviceId;
                    // 处理逻辑
                    var pks=row.PrimaryKey;
                    foreach (var pk in pks)
                    {
                        if (pk.Key == pkStartTime)
                        {
                            model.startTime = TimeHelper.ConvertLongToDateTime(pk.Value.IntegerValue);
                        }
                    }
                    foreach (var attr in row.Attribute)
                    {
                        if (attr.Key == attEndTime)
                        {
                            model.endTime = TimeHelper.ConvertLongToDateTime(attr.Value.IntegerValue);
                        }
                        else if (attr.Key == attRoute)
                        {
                            byte[] lbyte = attr.Value.BinaryValue;
                            Dictionary<string, int> dictionary = ByteIntHelper.GetRouteByByte(lbyte);
                            model.startLongitude = dictionary["startLongitude"];
                            model.startLatitude = dictionary["startLatitude"];
                            model.endLongitude = dictionary["endLongitude"];
                            model.endLatitude = dictionary["endLatitude"];
                            model.drivingTime = dictionary["drivingTime"];
                            model.mileage = dictionary["mileage"];
                            model.topSpeed = dictionary["topSpeed"];
                        }
                        else if (attr.Key == attDurationStats)
                        {
                            byte[] lbyte = attr.Value.BinaryValue;
                            Dictionary<string, int> dictionary = ByteIntHelper.GetDurationstatsByByte(lbyte);
                            model.idleTime = dictionary["idleTime"];
                            model.lowSpeedTime = dictionary["lowSpeedTime"];
                            model.mediumSpeedTime = dictionary["mediumSpeedTime"];
                            model.highSpeedTime = dictionary["highSpeedTime"];
                            model.speedingTime = dictionary["speedingTime"];
                        }
                        else if (attr.Key == attEventStats)
                        {
                            byte[] lbyte = attr.Value.BinaryValue;
                            Dictionary<int, int> dictionary = ByteIntHelper.GetEventStatsByByte(lbyte);
                            if (dictionary.ContainsKey(rapidAcceleration_Code))
                            {
                                model.rapidAccelerationTimes = dictionary[rapidAcceleration_Code];
                            }
                            else
                            {
                                model.rapidAccelerationTimes = 0;
                            }
                            if (dictionary.ContainsKey(rapidAcceleration_Code))
                            {
                                model.rapidDecelerationTimes = dictionary[rapidDeceleration_Code];
                            }
                            else
                            {
                                model.rapidDecelerationTimes = 0;
                            }
                            if (dictionary.ContainsKey(sharpTurn_Code))
                            {
                                model.sharpTurnTimes = dictionary[sharpTurn_Code];
                            }
                            else
                            {
                                model.sharpTurnTimes = 0;
                            }
                        }
                    }
                    list.Add(model);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return list.OrderBy(p=>p.startTime).ToList();
        }
    }
}
