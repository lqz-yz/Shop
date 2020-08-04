using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace COMMON
{
    public class HMACSHA
    {

        private static string HmacSHA256(string secret, string signKey)
        {
            string signRet = string.Empty;
            using (HMACSHA256 mac = new HMACSHA256(Encoding.UTF8.GetBytes(signKey)))
            {
                byte[] hash = mac.ComputeHash(Encoding.UTF8.GetBytes(secret));
                signRet = Convert.ToBase64String(hash);
                //signRet = ToHexString(hash); ;
            }
            return signRet;
        }

        ///// <summary>
        ///// HmacSHA256算法,返回的结果始终是32位
        ///// </summary>
        ///// <param name="key">加密的键，可以是任何数据</param>
        ///// <param name="content">待加密的内容</param>
        ///// <returns></returns>
        //public static byte[] HmacSHA256(byte[] key, byte[] content)
        //{
        //    using (var hmacsha256 = new HMACSHA256(key))
        //    {
        //        byte[] hashmessage = hmacsha256.ComputeHash(content);
        //        return hashmessage;
        //    }
        //}

        //public static string Encrypt(string message, string secret)
        //{
        //    secret = secret ?? "";
        //    var encoding = new System.Text.UTF8Encoding();
        //    byte[] keyByte = encoding.GetBytes(secret);
        //    byte[] messageBytes = encoding.GetBytes(message);
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

        ////byte[]转16进制格式string
        //public static string ToHexString(byte[] bytes)
        //{
        //    string hexString = string.Empty;
        //    if (bytes != null)
        //    {
        //        StringBuilder strB = new StringBuilder();
        //        foreach (byte b in bytes)
        //        {
        //            strB.AppendFormat("{0:x2}", b);
        //        }
        //        hexString = strB.ToString();
        //    }
        //    return hexString;
        //}


    }
}
