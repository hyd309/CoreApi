using System;
using CoreImportDataApp.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using CoreImportDataApp.Services;

namespace CoreImportDataApp
{
    public class TestDI
    {
        IServiceProvider serviceProvider;
        TableStoreModel tableStoreModel;
        public TestDI(IServiceProvider provider)
        {
            tableStoreModel = provider.GetService<IOptions<TableStoreModel>>().Value;
            serviceProvider = provider;
        }

        public void DoWork()
        {
            string aa = tableStoreModel.AccessKeyID;
            var ab = serviceProvider.GetService<ITest>();
            string bb = ab.Add();
            bb = "";
        }
    }
}
