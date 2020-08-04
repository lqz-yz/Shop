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
        public ResponsMessage<string> GetToken(MemberVModel memberVModel)
        {
            //获取oppid
            //C#6.0字符串插值
            string url = $"https://api.weixin.qq.com/sns/jscode2session?appid={AppID}&secret={AppSecret}&js_code={memberVModel.code}&grant_type=authorization_code";
            //服务端发起Http请求（爬虫）
            //声明一个http请求对象
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            //发起请求
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //获取响应流GetResposeStream
            Stream myResponseStream = response.GetResponseStream();
            //冲响应流中读取数据
            StreamReader mystreamReader = new StreamReader(myResponseStream, System.Text.Encoding.UTF8);
            string retString = mystreamReader.ReadToEnd();
            //关闭流
            mystreamReader.Close();
            myResponseStream.Close();
            //序列化:将对象转化为json串
            //反序列化:jiangjson串转化为对象 
            //var obj = JsonConvert.DeserializeObject<dynamic>(retString);
            //var openid = obj.openid.ToString();
            string openid = JsonConvert.DeserializeObject<dynamic>(retString).openid.ToString();
            //JObject jObject = JObject.Parse(retString);
            //var openid = jObject["openid"].ToString();


            //2.根据oppenid检测数据库是否包含该用户,没有测添加到数据库中
            //判断数据库中是否存在该用户(根据openid查询)
            //如果没有,使用ef往member表中添加用户数据
            var result = Bll.Search(x => x.OpenId == openid).ToList();
            if (result.Count == 0)
            {
                mem = memberVModel.userInfo;
                mem.OpenId = openid;
                Bll.Add(mem);
            }
            //else
            //{

            //}



            ////1.生成头部（Header）
            //var header = new Dictionary<string, object>
            // {
            //     { "alg", "HmacSHA256" },
            //     { "typ","JWT" },
            // };
            //string headerjson = JsonConvert.SerializeObject(header);
            //string Header = Base64Helper.EncodeBase64(headerjson);

            ////2.生成载荷（payload）

            ////将系统时间转换成UNIX时间戳
            ////DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            ////DateTime dtNow = DateTime.Parse(DateTime.Now.ToString());
            ////TimeSpan toNow = dtNow.Subtract(dtStart);
            ////string timeStamp = toNow.Ticks.ToString();
            ////timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            ////Response.Write(timeStamp);
            ////生成时间Unix时间戳
            //DateTime issued = DateTime.Parse(DateTime.Now.ToString());
            //string iat = issued.Ticks.ToString();
            //iat = iat.Substring(0, iat.Length - 7);

            ////结束时间Unix时间戳
            //DateTime expires = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now.AddDays(7));
            //string exp = expires.Ticks.ToString();
            //exp = exp.Substring(0, exp.Length - 7);


            ////DateTime expires = DateTime.Parse((DateTime.Now.AddDays(7) - DateTime.Now).ToString());
            //var payload = new Dictionary<string, object> {
            //     // 包括需要传递的用户信息；

            //    { "iss", "Online JWT Builder"},//iss: 该JWT的签发者，一般是服务器，是否使用是可选的；
            //    { "iat", iat},//iat(issued at): 在什么时候签发的(UNIX时间)，是否使用是可选的；
            //    { "exp", exp},//exp(expires): 什么时候过期，这里是一个Unix时间戳，是否使用是可选的；
            //    { "aud", "www.baidu.com"},//aud: 接收该JWT的一方，是否使用是可选的；
            //    { "sub", "uid"},//sub: 该JWT所面向的用户，userid，是否使用是可选的；
            //    { "nickname", memberVModel.userInfo.NickName},
            //    { "username", memberVModel.userInfo.NickName}
            //};

            //string payloadjson = JsonConvert.SerializeObject(payload);
            //string Payload = Base64Helper.EncodeBase64(payloadjson);


            ////3.生成签名（signature）
            //string secret = "~!@$%&*()";
            //var encodedString = Payload + '.' + Header;
            //var signature = HmacSHA256(secret,encodedString);


            //var token= encodedString+'.'+signature;


            ////3.生成token(最常用的两种方式:MD5加密,jwt)
            //string yan = "~!@$%&*()";
            //string time = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
            //string guid = Guid.NewGuid().ToString("N");
            //string random = new Random().Next(10000, 99999).ToString();
            //string nickName = memberVModel.userInfo.NickName;

            //string str = yan + time + guid + random + nickName;
            //string token = Md5Helper.Md5(Md5Helper.Md5(str));

            var payload = new Dictionary<string, string> //模拟到时候服务器想接收的数据
            {
                { "username", memberVModel.userInfo.NickName+ Guid.NewGuid().ToString("N")}
            };
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, secret);


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

            RedisHelper.Set(token, openid, DateTime.Now.AddDays(7) - DateTime.Now);

            //将token存在redis中


            return new ResponsMessage<string>
            {
                Code = 200,
                Data = token
            };

        }

        [AuthFilter]
        [Route("api/auth/text")]
        [HttpGet]
        public string AuthText() {
            return "auth ok";
        }
    }
}
