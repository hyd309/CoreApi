using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackendApi.TableStore
{
    public class EventModel
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public long deviceCode { get; set; }
        /// <summary>
        /// 事件代码(报警代码)
        /// </summary>
        public int alarmNo { get; set; }
        /// <summary>
        /// 事件时间(报警时间)
        /// </summary>
        public DateTime alarmTime { get; set; }
        /// <summary>
        /// 事件参数
        /// </summary>
        public int alarmParameter { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public int longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public int latitude { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public int speed { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        public int direct { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int height { get; set; }
    }
}
