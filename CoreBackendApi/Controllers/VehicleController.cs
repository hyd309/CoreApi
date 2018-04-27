using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CoreBackendApi.Common;

namespace CoreBackendApi.Controllers
{
    [ApiVersion("1.0")]
    //[Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VehicleController : Controller
    {
        [HttpGet()]
        [Route("limit")]
        public string GetLimit(string deveiceId, string longitude, string latitude, string date)
        {
            AddressComponent addCom = BaiDuGeoCodingHelper.GetBaiDuCity(longitude, latitude);//根据坐标获取所属城市信息

            //string url = "http://jisuclwhxx.market.alicloudapi.com/vehiclelimit/city?cityname="+addCom.City;
            //string appcode = "65f40fff29564e74a036efbf22179e25";
            //Dictionary<string, string> header = new Dictionary<string, string>();
            //header.Add("Authorization", "APPCODE "+ appcode);
            //string str= HttpHelper.HttpGet(url, header);
            return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = 0, Data = addCom });
        }


    }
}