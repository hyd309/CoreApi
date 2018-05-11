using CoreImportDataApp.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using CoreImportDataApp.Services;
using NLog;//NLog.Extensions.Logging  和NLog.Web.AspNetCore两个类库。
using Microsoft.Extensions.Logging;

namespace CoreImportDataApp
{
    class Program
    {
        public static string SqlConnecting { get; set; }
        static void Main(string[] args)
        {
            /**
             * 在ASP.NET Core中使用依赖注入中使用很简单，只需在Startup类的ConfigureServices()方法中，
             * 通过IServiceCollection接口进行注入即可，其它的无需关心
             * 
             * 在控制台程序中就不一样了，除了注入外，你还需要构建容器，解析注入。
             * 注入通过IServiceCollection接口，而构建容器需要调用IServiceCollection的扩展方法BuildServiceProvider()，
             * 解析需要调用IServiceProvider的扩展方法GetService<T>()
             **/
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appSetting.json");
            var configuration = builder.Build();

            SqlConnecting = configuration.GetConnectionString("DefaultConnection");

            IServiceCollection services = new ServiceCollection();
            services.AddOptions();
            services.Configure<TableStoreModel>(configuration.GetSection("TableStore"));
            services.AddSingleton<TableStoreModel>();
            services.AddTransient<ILoggerFactory, LoggerFactory>();
            services.AddTransient<ITest, Test>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            TestDI testDI = new TestDI(serviceProvider);
            testDI.StartWork();
            //var host = new WebHostBuilder().UseKestrel().UseStartup<Startup>().Build();
            //host.Run();
            Console.ReadLine();
        }
    }
}
