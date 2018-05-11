
namespace CoreImportDataApp.Services
{
    public class Test:ITest
    {
        public string Add()
        {
            Startup.log.Info("运行中....");
            return "ITest => test /add()";
        }
    }
}
