using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackendApi.TableStore
{
    public class RouteModel
    {
        /// <summary>
        /// 行程id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public long deviceCode { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endTime { get; set; }
        /// <summary>
        /// 开始经度
        /// </summary>
        public int startLongitude { get; set; }
        /// <summary>
        /// 结束经度
        /// </summary>
        public int endLongitude { get; set; }
        /// <summary>
        /// 开始纬度
        /// </summary>
        public int startLatitude { get; set; }
        /// <summary>
        /// 结束纬度
        /// </summary>
        public int endLatitude { get; set; }
        /// <summary>
        /// 行驶时长（秒）
        /// </summary>
        public int drivingTime { get; set; }
        /// <summary>
        /// 行驶里程（米）
        /// </summary>
        public int mileage { get; set; }
        /// <summary>
        /// 最高时速KM/H
        /// </summary>
        public int topSpeed { get; set; }
        /// <summary>
        /// 超速行驶时间>120km/h
        /// </summary>
        public int speedingTime { get; set; }
        /// <summary>
        /// 高速行驶时间80-120km/h
        /// </summary>
        public int highSpeedTime { get; set; }
        /// <summary>
        /// 中速行驶的时间40-80km/h
        /// </summary>
        public int mediumSpeedTime { get; set; }
        /// <summary>
        /// 低速行驶的时间(1km/h-40km/h)
        /// </summary>
        public int lowSpeedTime { get; set; }
        /// <summary>
        /// 怠速的时间
        /// </summary>
        public int idleTime { get; set; }
        /// <summary>
        /// 急加速次数
        /// </summary>
        public int rapidAccelerationTimes { get; set; }
        /// <summary>
        /// 急减速次数
        /// </summary>
        public int rapidDecelerationTimes { get; set; }
        /// <summary>
        /// 急转弯次数
        /// </summary>
        public int sharpTurnTimes { get; set; }
    }
}
