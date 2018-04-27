using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackendApi.Auth
{
    public class TokenProviderOptions
    {

        /// <summary>
        /// 请求路径 http://localhost:51587/api/Token
        /// body里面，添加参数：appcode；或者 用户名、密码两种新式进行访问授权
        /// </summary>
        public string Path { get; set; } = "/Api/Token";

        public string Issuer { get; set; }

        public string Audience { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromHours(36);

        public SigningCredentials SigningCredentials { get; set; }
    }
}
