using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreBackendApi.Common;
using Newtonsoft.Json;

namespace CoreBackendApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/Location")]
    public class LocationController : Controller
    {
        [HttpGet]
        [Route("{deviceId}/{start}/{end}")]
        public string GetLocation(string deviceId, string start,string end)
        {
            DateTime dtStart = new DateTime();
            if(!DateTime.TryParse(start,out dtStart))
            {
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam,ErrorMsg="start参数错误：正确格式：2018-01-01 09:57"});
            }
            DateTime dtEnt = new DateTime();
            if (!DateTime.TryParse(end, out dtEnt))
            {
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = "end参数错误：正确格式：2018-01-01 09:57" });
            }
            if (dtEnt<dtStart)
            {
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = "结束时间大于开始时间" });
            }
            //允许的最大查询实际范围，默认2小时
            int maxTimes = 7200;//= 2 * 60 * 60;
            if ((dtEnt - dtStart).Seconds> maxTimes)
            {
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = "时间查询范围应该在2小时内" });
            }
            return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.OK, Data=null});
        }
    }
}