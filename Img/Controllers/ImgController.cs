using COMMON;
using Constant;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace Img.Controllers
{
    public class ImgController : Controller
    {
        string AccessKey = ConfigurationManager.AppSettings["AccessKey"];
        string SecretKey = ConfigurationManager.AppSettings["SecretKey"];
        string qiniuScope = ConfigurationManager.AppSettings["qiniuScope"];
        //string AccessKey = "3Qr5dFBv3xnYHlTuF0uCpsDfI-8GiWVGKwqnJz-r";
        //string SecretKey = "yhEdZUzX4Q98H2hJ5GQCJOneRfmP6_XSj8K1KGco";
        public ActionResult Upload()
        {
            //List<string> filesNames = new List<string>();
            List<ImgInfo> filesNames = new List<ImgInfo>();
            //上传多张图片   
            var Imgs = Request.Files;
            for (int i = 0; i < Imgs.Count; i++)//Imgs.Count获取长度
            {
                var Img = Imgs[i];
                if (Img == null)
                {
                    return Json(new { state = false, msg = "图片不存在" });
                }
                byte[] bytes = Common.StreamToBytes(Img.InputStream);
                FileExtension fileExtension = FileHelper.CheckTextFile(bytes);

                if (fileExtension == FileExtension.VALIDFILE)
                {
                    return Json(new { state = false, msg = "上传文件已损坏" });
                }
                if (!(fileExtension == FileExtension.GIF || fileExtension == FileExtension.JPG || fileExtension == FileExtension.PNG))
                {
                    return Json(new { state = false, msg = "上传类型有误" });
                }
                //Server.MapPath 根据相对路径(项目中的路径)来获取绝对路径

                //file.SaveAs(path):保存图片必须使用绝对路径+图片名称
                var extension = Path.GetExtension(Img.FileName);
                Random random = new Random();
                int randomNum = random.Next(10000, 99999);
                var dataStr = DateTime.Now.ToString("yyyyMMddHHmmssfffff") + randomNum.ToString();
                //图片保存到服务器
                var imgType = Request["imgType"];//获取图片类型
                string upLoadPath = null;
                if (WebConstants.ImgPathDicts.ContainsKey(imgType))
                {
                    upLoadPath = WebConstants.ImgPathDicts[imgType];
                }
                else
                {
                    return Json(new { state = false });
                }
                string fullpath = upLoadPath + dataStr + Img.FileName;
                Img.SaveAs(Server.MapPath(fullpath));


                //4.上传到七牛云服务器
                Mac mac = new Mac(AccessKey, SecretKey);
                PutPolicy putPolicy = new PutPolicy();
                putPolicy.Scope = qiniuScope;//空间名称
                string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
                Config config = new Config();
                // 空间对应的机房
                config.Zone = Zone.ZONE_CN_North; 
                // 是否使用https域名
                config.UseHttps = false;
                // 上传是否使用cdn加速
                config.UseCdnDomains = true;

                FormUploader fu = new FormUploader(config);
                HttpResult result = fu.UploadData(bytes, fullpath, token,null);
                //var cloudURL =  "http://qdg4o41j2.bkt.clouddn.com/" + fullpath;
                var cloudURL = "http://myshop.statics.yangliu.ink/" + fullpath;
                
                var url = "http://localhost:17114" + fullpath;
                //filesNames.Add("http://localhost:17114"+fullpath);
                ImgInfo imgInfo = new ImgInfo
                {
                    cloudURL = cloudURL,
                    url = url
                };
                filesNames.Add(imgInfo);
            }
            return Json(filesNames);
        }
    }
}