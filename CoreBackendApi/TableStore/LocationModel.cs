using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackendApi.TableStore
{
    public class LocationModel
    {
        public long device_code { get; set; }
        public DateTime gps_time { get; set; }
        public int latitude { get; set; }
        public int longitude { get; set; }
        public int speed { get; set; }
        public int direct { get; set; }
        public int height { get; set; }
    }
}
