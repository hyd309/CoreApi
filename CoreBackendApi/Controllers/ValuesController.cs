using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace CoreBackendApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ValuesController : Controller
    {
        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        // GET api/values
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
       [Route("index")]
        public string Index()
        {
            return "Bido API接口";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            log.Info("id="+id);
            log.Debug("Debug=" + id);
            log.Trace("Trace=" + id);
            log.Warn("Warn=" + id);
            log.Error("Error=" + id);
            log.Fatal("崩溃");
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
