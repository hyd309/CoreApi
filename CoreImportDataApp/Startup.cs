using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace CoreImportDataApp
{
    public class Startup
    {
        public static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");
            app.Run(context=>
            {
                return context.Response.WriteAsync("bido-Nlog");
            });
        }
    }
}
