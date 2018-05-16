using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aliyun.OTS;

namespace CoreBackendApi.Common
{
    public class TableStoreModel
    {
        public string PublicEnvironment { get; set; }
        public string EndPointPublic { get; set; }
        public string EndPointPrivate { get; set; }
        public string AccessKeyID { get; set; }
        public string AccessKeySecret { get; set; }
        public string InstanceName_Location { get; set; }
        public string InstanceName_Event { get; set; }
        public string InstanceName_Route { get; set; }

        public static OTSClient _oTSClient_Event;
        public static OTSClient _oTSClient_Location;
        public static OTSClient _oTSClient_Route;
        public OTSClient GetOTSClientLocation()
        {
            if (_oTSClient_Event == null)
            {
                _oTSClient_Event = new OTSClient(GetEndPoint(InstanceName_Location),AccessKeyID, AccessKeySecret, InstanceName_Location);
            }
            return _oTSClient_Event;
        }

        public OTSClient GetOTSClientEvent()
        {
            if (_oTSClient_Event == null)
            {
                _oTSClient_Event = new OTSClient(GetEndPoint(InstanceName_Event), AccessKeyID, AccessKeySecret, InstanceName_Event);
            }
            return _oTSClient_Event;
        }

        public OTSClient GetOTSClientRoute()
        {
            if (_oTSClient_Event == null)
            {
                _oTSClient_Event = new OTSClient(GetEndPoint(InstanceName_Route), AccessKeyID, AccessKeySecret, InstanceName_Route);
            }
            return _oTSClient_Event;
        }

        private string GetEndPoint(string keyName)
        {
            string endPoint = PublicEnvironment == "1" ? EndPointPublic : EndPointPrivate;
            return string.Format(endPoint, keyName);
        }
    }
}
