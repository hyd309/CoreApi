using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackendApi.Models
{
    public class DeviceLocation
    {
        public long id { get; set; }
        public long device_code { get; set; }
        public int route_id { get; set; }
        public DateTime gps_time { get; set; }
        public int latitude { get; set; }
        public int longitude { get; set; }
        public bool is_location { get; set; }
        public bool n_s { get; set; }
        public bool e_w { get; set; }
        public bool gps_or_bsl { get; set; }
        public short speed { get; set; }
        public short direct { get; set; }
        public byte gps_number { get; set; }
        public byte gms_signal_quality { get; set; }
        public int mileage { get; set; }
        public DateTime create_time { get; set; }
    }
}
