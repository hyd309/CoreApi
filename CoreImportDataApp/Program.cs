using CoreImportDataApp.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using CoreImportDataApp.Services;

namespace CoreImportDataApp
{
    class Program
    {
        public static string SqlConnecting { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appSetting.json");
            var configuration = builder.Build();

            SqlConnecting = configuration.GetConnectionString("DefaultConnection");

            IServiceCollection services = new ServiceCollection();
            services.AddOptions();
            services.Configure<TableStoreModel>(configuration.GetSection("TableStore"));
            services.AddSingleton<TableStoreModel>();
            services.AddTransient<ITest, Test>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            
            TestDI testDI = new TestDI(serviceProvider);
            testDI.DoWork();
        }
    }
}
