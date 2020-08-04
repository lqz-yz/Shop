using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using COMMON;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using JWT.Serializers;

namespace API.Models
{
    public class AuthFilterAttribute : AuthorizationFilterAttribute
    {

        string secret = ConfigurationManager.AppSettings["jwtSecret"].ToString();
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            IEnumerable<string> headValues;
            string token = null;
            if (actionContext.Request.Headers.TryGetValues("Authorization", out headValues)) {
                token = headValues.FirstOrDefault();
            }
            //var token = actionContext.Request.Headers.GetValues("Authorization")?.First();

            if (token != null)
            {

                try
                {
                    //验证jwt的合法性
                    IJsonSerializer serializer = new JsonNetSerializer();
                    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                    IDateTimeProvider provider = new UtcDateTimeProvider();
                    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                    IJwtValidator validator = new JwtValidator(serializer, provider);
                    IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                    var json = decoder.Decode(token, secret, verify: true);

                    //验证redis是否过期
                    var openid = RedisHelper.Get(token);
                    if (openid == null)
                    {
                        //HttpStatusCode.Unauthorized:401,未授权
                        HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        httpResponse.Content = new StringContent("签名已过期");
                        actionContext.Response = httpResponse;
                    }

                }
                catch (SignatureVerificationException)
                {
                    HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    httpResponse.Content = new StringContent("签名无效");
                    actionContext.Response = httpResponse;
                }

            }

        }



    }
}