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
    [Route("api/v{version:apiVersion}/Route")]
    public class RouteController : Controller
    {
        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        private readonly IRouteService _routeService;

        public RouteController(IRouteService route)
        {
            _routeService = route;
        }
        [HttpGet]
        [Authorize]
        [Route("{deviceId}/{start}/{end}")]
        public string GetRoutes(string deviceId, string start, string end)
        {
            log.Info("GetRoute(string deviceId=" + deviceId + " , string start= " + start + " ,string end=  " + end);

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
            var data = _routeService.GetRoutes(deviceId, dtStart, dtEnt);
            return JsonConvert.SerializeObject(new ResultMsg() { ResultCode = (int)CodeEnum.OK, ResultTimes = DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss"), Data = data });
        }
    }
}