using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreBackendApi.Common;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace CoreBackendApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/Location")]
    public class LocationController : Controller
    {
        private readonly ILogger<LocationController> _logger;
        public LocationController(ILogger<LocationController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("{deviceId}/{start}/{end}")]
        public string GetLocation(string deviceId, string start,string end)
        {
            _logger.LogInformation("GetLocation(string deviceId, string start,string end)"+deviceId+" & "+start+" & "+end);
            DateTime dtStart = new DateTime();
            if(!DateTime.TryParse(start,out dtStart))
            {
                string errmsg = "start参数错误：正确格式：2018-01-01 09:57";
                _logger.LogDebug(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam,ErrorMsg= errmsg });
            }
            DateTime dtEnt = new DateTime();
            if (!DateTime.TryParse(end, out dtEnt))
            {
                string errmsg = "end参数错误：正确格式：2018-01-01 09:57";
                _logger.LogDebug(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = errmsg });
            }
            if (dtEnt<dtStart)
            {
                string errmsg = "结束时间大于开始时间";
                _logger.LogDebug(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = errmsg });
            }
            //允许的最大查询实际范围，默认2小时
            int maxTimes = 7200;//= 2 * 60 * 60;
            if ((dtEnt - dtStart).Seconds> maxTimes)
            {
                string errmsg = "时间查询范围应该在2小时内";
                _logger.LogDebug(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = errmsg });
            }
            return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.OK, Data=null});
        }
    }
}