using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class TokenVModel
    {
        public string Token { get; set; }//请求令牌
        public string RefreshToken { get; set; }//刷新令牌
        public long Expire { get; set; }//请求令牌过期时间

        public long RefreshExpire { get; set; }//刷新令牌过期时间
    }
}