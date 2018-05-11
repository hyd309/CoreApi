using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackendApi.Common
{
    public class ResultMsg
    {
        public int ResultCode { get; set; }
        public string ErrorMsg { get; set; }
        public string ResultTimes { get; set; }
        public object Data { get; set; }

        public static ResultMsg Failure(string errmsg)
        {
            return new ResultMsg() { ResultCode=(int)CodeEnum.ErrOther,ErrorMsg=errmsg,ResultTimes=DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss")};
        }
    }

    public enum CodeEnum
    {
        OK=0,
        ErrParam=100,
        ErrOther=200
    }
}
