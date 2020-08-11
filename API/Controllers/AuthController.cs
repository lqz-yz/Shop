using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using API.Models;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using COMMON;
using StackExchange.Redis;
using IBLL;
using BLL;
using Model;
using System.Security.Cryptography;
using System.Text;
using JWT;
using JWT.Serializers;
using JWT.Algorithms;

namespace API.Controllers
{
    public class AuthController : ApiController
    {
        IMemberBLL Bll = new MemberBLL();
        Member mem = new Member();
        string AppID = ConfigurationManager.AppSettings["AppID"];
        string AppSecret = ConfigurationManager.AppSettings["AppSecret"];
        string secret = ConfigurationManager.AppSettings["jwtSecret"].ToString();

        //public static string HmacSHA256(string encodedString, string secret)
        //{
        //    secret = secret ?? "";
        //    var encoding = new System.Text.UTF8Encoding();
        //    byte[] keyByte = encoding.GetBytes(secret);
        //    byte[] messageBytes = encoding.GetBytes(encodedString);
        //    using (var hmacsha256 = new HMACSHA256(keyByte))
        //    {
        //        byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
        //        StringBuilder builder = new StringBuilder();
        //        for (int i = 0; i < hashmessage.Length; i++)
        //        {
        //            builder.Append(hashmessage[i].ToString("x2"));
        //        }
        //        return builder.ToString();
        //    }
        //}

        [Route("api/auth/gettoken")]
        [HttpPost]
        public ResponsMessage<TokenVModel> GetToken(MemberVModel memberVModel)
        {
            HttpWebResponse response = null;
            Stream Responsestream = null;
            StreamReader streamReader = null;
            try
            {
                //获取oppid
                //C#6.0字符串插值
                string url = $"https://api.weixin.qq.com/sns/jscode2session?appid={AppID}&secret={AppSecret}&js_code={memberVModel.code}&grant_type=authorization_code";
                //服务端发起Http请求（爬虫）
                //声明一个http请求对象
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                //发起请求
                 response = (HttpWebResponse)request.GetResponse();
                //获取响应流GetResposeStream
                Stream myResponseStream = response.GetResponseStream();
                //冲响应流中读取数据
                StreamReader mystreamReader = new StreamReader(myResponseStream, System.Text.Encoding.UTF8);
                string retString = mystreamReader.ReadToEnd();
                //关闭流
                //mystreamReader.Close();
                //myResponseStream.Close();
                //序列化:将对象转化为json串
                //反序列化:jiangjson串转化为对象 
                //var obj = JsonConvert.DeserializeObject<dynamic>(retString);
                //var openid = obj.openid.ToString();
              
                //JObject jObject = JObject.Parse(retString);
                //var openid = jObject["openid"].ToString();

                if (JObject.Parse(retString)["openid"] == null)
                {
                    return new ResponsMessage<TokenVModel>()
                    {
                        Code = 500,
                        Message = "code不正确"
                    };
                }
                //2.根据oppenid检测数据库是否包含该用户,没有测添加到数据库中
                //判断数据库中是否存在该用户(根据openid查询)
                //如果没有,使用ef往member表中添加用户数据
                string openid = JsonConvert.DeserializeObject<dynamic>(retString).openid.ToString();
                int uid;
                var memberlist = Bll.Search(x => x.OpenId == openid);
                if (memberlist.Count == 0)
                {
                    mem = memberVModel.userInfo;
                    mem.OpenId = openid;
                    Bll.Add(mem);
                    //memberlist = mem;
                    uid = mem.ID;
                }
                else
                {
                    uid = memberlist[0].ID;
                }
                string token, refreshToken;
                CreateToken(uid, out token, out refreshToken);


                ////4.将token存入redis
                //// c#操作redis
                ////redis:一般存储kv类型的键值对数据(字典数据库)
                ////1.声明一个链接
                //var conn = ConnectionMultiplexer.Connect("192.168.230.130:6379,password=123456");
                ////1.指定操作的数据库
                //var db = conn.GetDatabase(0);
                ////3.写入数据
                ////设置name的有效期为15天
                //db.StringSet(token, openid, DateTime.Now.AddDays(7) - DateTime.Now);
                ////redis如果不设置有效期,默认是永远有效(内村足够用)

                DateTime expire = DateTime.Now.AddDays(7);
                DateTime now = DateTime.Now;
                var tokenSet = RedisHelper.Set(token, uid, expire - now);
                var refreshTokenSet = RedisHelper.Set(refreshToken, uid, expire.AddDays(7) - now);
                //将token存在redis中（refreshToken过期时间一般为token的两倍）
                if (!tokenSet && !refreshTokenSet)
                {
                    return new ResponsMessage<TokenVModel>()
                    {
                        Code = 500,
                        Message = "获取token失败，请重新授权"
                    };
                }
                return new ResponsMessage<TokenVModel>()
                {
                    Code = 200,
                    Message = "请求成功",
                    Data = new TokenVModel()
                    {
                        Token = token,
                        RefreshToken = refreshToken,
                        Expire = (int)(expire - now).TotalMilliseconds,
                        RefreshExpire = (int)(expire.AddDays(7) - now).TotalMilliseconds
                    }
                };
            }
            catch (Exception)
            {

                return new ResponsMessage<TokenVModel>()
                {
                    Code = 500,
                    Message = "获取个人信息失败"
                };
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (Responsestream != null)
                {
                    Responsestream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }


        }

        [Route("api/auth/gettokenByrefreshToken")]
        [HttpGet]
        public ResponsMessage<TokenVModel> GetTokenByRefeshToken(string rToken) {
            //验证refreshToken是否过期
            var memberID = RedisHelper.Get(rToken).ToString();
            if (memberID==null) {
                return new ResponsMessage<TokenVModel>()
                {
                    Code = 401,
                    Message = "refreshToken失效,请重新授权",
                };
            }
            string token, refreshToken; 
            CreateToken(Int32.Parse(memberID), out token, out refreshToken);


            DateTime expire = DateTime.Now.AddDays(7);
            DateTime now = DateTime.Now;
            RedisHelper.Set(token, Int32.Parse(memberID), expire - now);
            RedisHelper.Set(refreshToken, Int32.Parse(memberID), expire.AddDays(7) - now);
            return new ResponsMessage<TokenVModel>()
            {
                Code = 200,
                Message = "请求成功",
                Data = new TokenVModel()
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Expire = (int)(expire - now).TotalMilliseconds,
                    RefreshExpire= (int)(expire.AddDays(7) - now).TotalMilliseconds
                }
            };
        }
        private void CreateToken(int memberID,out string token, out string refreshToken)
        {
            Random random = new Random();
            var payload = new Dictionary<string, string> //模拟到时候服务器想接收的数据
            {
                { "username", memberID+random.Next(10000,99999).ToString()+ Guid.NewGuid().ToString("N")}
            };
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
             token = encoder.Encode(payload, secret);
            //RefreshToken
             payload = new Dictionary<string, string> //模拟到时候服务器想接收的数据
            {
                { "username", memberID+random.Next(100000,999999).ToString()+ Guid.NewGuid().ToString("N")}
            };
            refreshToken = encoder.Encode(payload, secret);
            //return token,RefreshToken;
        }

        [AuthFilter]
        [Route("api/auth/text")]
        [HttpGet]
        public string AuthText() {
            return "auth ok";
        }



        //public ResponsMessage<TokenVModel> GetToken(MemberVModel memberVModel)
        //{
        //    HttpWebResponse response = null;
        //    Stream Responsestream = null;
        //    StreamReader streamReader = null;
        //    try
        //    {
        //        string openIdUrl = $"https://api.weixin.qq.com/sns/jscode2session?appid={ConfigurationManager.AppSettings["AppID"]}&secret={ConfigurationManager.AppSettings["AppSecret"]}&js_code={memberVModel.Code}&grant_type=authorization_code";
        //        //服务器发起http请求（爬虫）
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(openIdUrl);
        //        //获取返回头
        //        response = (HttpWebResponse)request.GetResponse();
        //        request.Method = "GET";
        //        //获取响应流
        //        Responsestream = response.GetResponseStream();
        //        streamReader = new StreamReader(Responsestream, System.Text.Encoding.UTF8);
        //        string retString = streamReader.ReadToEnd();

        //        if (JObject.Parse(retString)["openid"] == null)
        //        {
        //            return new ResponsMessage<TokenVModel>()
        //            {
        //                Code = 500,
        //                Message = "code不正确"
        //            };
        //        }

        //        string openId = JObject.Parse(retString)["openid"].ToString();
        //        //判断数据库中是否存在该用户（根据openId）
        //        var member = Bll.Search(x => x.OpenId == openId).First();
        //        if (member == null)
        //        {
        //            memberVModel.UserInfo.OpenId = openId;
        //            Bll.Add(memberVModel.UserInfo);
        //            member = memberVModel.UserInfo;
        //        }

        //        //生成token
        //        CreateToken(member.ID.ToString(), out string token, out string refreshToken);
        //        //将token存入redis(tkoen设置时间不宜过长,refreshToken过期时间一般是token的两倍)
        //        var tokenSet = RedisHelper.Set(token, (member.ID), expire.AddMinutes(1) - expire);
        //        var refreshTokenSet = RedisHelper.Set(refreshToken, (member.ID), expire.AddMinutes(2) - expire);
        //        if (!tokenSet && !refreshTokenSet)
        //        {
        //            return new ResponsMessage<TokenVModel>()
        //            {
        //                Code = 500,
        //                Message = "获取token失败，请重新授权"
        //            };
        //        }

        //        return new ResponsMessage<TokenVModel>()
        //        {
        //            Code = 200,
        //            Data = new TokenVModel()
        //            {
        //                Token = token,
        //                RefreshToken = refreshToken,
        //                //Expire = (int)(expire - now).TotalMilliseconds
        //                TokenExpire = (expire.AddMinutes(1).ToUniversalTime().Ticks - 621355968000000000) / 10000,
        //                RefreshTokenExpire = (expire.AddMinutes(2).ToUniversalTime().Ticks - 621355968000000000) / 10000
        //            }
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponsMessage<TokenVModel>()
        //        {
        //            Code = 500,
        //            Message = "获取个人信息失败"
        //        };
        //    }
        //    finally
        //    {
        //        if (streamReader != null)
        //        {
        //            streamReader.Close();
        //        }
        //        if (Responsestream != null)
        //        {
        //            Responsestream.Close();
        //        }
        //        if (response != null)
        //        {
        //            response.Close();
        //        }
        //    }
        //}


    }
}
