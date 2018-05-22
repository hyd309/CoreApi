using System;
using Microsoft.AspNetCore.Mvc;
using CoreBackendApi.Common;
using Newtonsoft.Json;
using CoreBackendApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace CoreBackendApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/Location")]
    public class LocationController : Controller
    {
        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        private readonly ILocationService _locationService;

        public LocationController(ILocationService location)
        {
            _locationService = location;
        }

        [HttpGet]
        [Authorize]
        [Route("{deviceId}/{start}/{end}")]
        public string GetLocation(string deviceId, string start,string end)
        {
            log.Info("GetLocation(string deviceId="+deviceId+ " , string start= " + start+ " ,string end=  " + end);

            if (string.IsNullOrEmpty(deviceId))
            {
                string errmsg = "deviceId参数错误，deviceId不能为空";
                log.Warn(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = errmsg });
            }

            DateTime dtStart = new DateTime();
            if(!DateTime.TryParse(start,out dtStart))
            {
                string errmsg = "start参数错误：不能为空,正确格式：2018-01-01 09:57";
                log.Warn(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam,ErrorMsg= errmsg });
            }
            DateTime dtEnt = new DateTime();
            if (!DateTime.TryParse(end, out dtEnt))
            {
                string errmsg = "end参数错误：不能为空,正确格式：2018-01-01 09:57";
                log.Warn(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = errmsg });
            }
            if (dtEnt<dtStart)
            {
                string errmsg = "参数错误：end结束时间小于开始时间start";
                log.Warn(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = errmsg });
            }
            //允许的最大查询实际范围，默认2小时
            int maxTimes = 7200;//= 2 * 60 * 60;
            if ((dtEnt - dtStart).Seconds> maxTimes)
            {
                string errmsg = "时间查询范围应该在2小时内";
                log.Warn(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = errmsg });
            }
            var data = _locationService.GetLocation(deviceId,dtStart,dtEnt);
            return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.OK, Data= data });

        }
    }
}