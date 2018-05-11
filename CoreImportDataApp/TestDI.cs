using System;
using CoreImportDataApp.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using CoreImportDataApp.Services;
using System.Timers;

namespace CoreImportDataApp
{
    public class TestDI
    {
        IServiceProvider serviceProvider;
        TableStoreModel tableStoreModel;
        Timer timer =new Timer(2000);
        public TestDI(IServiceProvider provider)
        {
            tableStoreModel = provider.GetService<IOptions<TableStoreModel>>().Value;
            serviceProvider = provider;
        }
        public void StartWork()
        {
            timer.Elapsed += _timer_Elapsed;
            timer.Start();
        }
        public void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;
            string aa = tableStoreModel.AccessKeyID;
            var ab = serviceProvider.GetService<ITest>();
            string bb = ab.Add();
            
            Console.WriteLine(bb);
            timer.Enabled = true;
        }
    }
}
