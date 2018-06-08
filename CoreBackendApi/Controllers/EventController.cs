using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBackendApi.Common;
using CoreBackendApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreBackendApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/Event")]
    public class EventController : Controller
    {
        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        private readonly IEventService _service;

        public EventController(IEventService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        [Route("{deviceId}/{start}/{end}/{alarmNo?}")]
        public string GetEvents(string deviceId, string start, string end,int alarmNo=int.MinValue)
        {
            log.Info("GetEvents(string deviceId=" + deviceId + " , string start= " + start + " ,string end=  " + end + " ,string alarmNo=  " + alarmNo);

            if (string.IsNullOrEmpty(deviceId))
            {
                string errmsg = "deviceId参数错误，deviceId不能为空";
                log.Warn(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = errmsg });
            }

            DateTime dtStart = new DateTime();
            if (!DateTime.TryParse(start, out dtStart))
            {
                string errmsg = "start参数错误：不能为空,正确格式：2018-01-01 09:57";
                log.Warn(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = errmsg });
            }
            DateTime dtEnt = new DateTime();
            if (!DateTime.TryParse(end, out dtEnt))
            {
                string errmsg = "end参数错误：不能为空,正确格式：2018-01-01 09:57";
                log.Warn(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = errmsg });
            }
            if (dtEnt < dtStart)
            {
                string errmsg = "参数错误：end结束时间小于开始时间start";
                log.Warn(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = errmsg });
            }
            //允许的最大查询实际范围，默认2小时
            int maxTimes = 86400;//= 24 * 60 * 60;
            if ((dtEnt - dtStart).Seconds > maxTimes)
            {
                string errmsg = "时间查询范围应该在24小时内";
                log.Warn(errmsg);
                return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.ErrParam, ErrorMsg = errmsg });
            }
            var data = _service.GetEvents(deviceId, dtStart, dtEnt, alarmNo);
            return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.OK, ResultTimes = DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss"), Data = data });
        }
    }
}