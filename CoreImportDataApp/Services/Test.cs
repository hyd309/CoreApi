using System;
using System.Collections.Generic;
using System.Text;

namespace CoreImportDataApp.Services
{
    public class Test:ITest
    {
        public string Add()
        {
            return "ITest => test /add()";
        }
    }
}
