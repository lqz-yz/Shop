using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constant
{
    public class WebConstants
    {
        //用来存放图片类型和上传路径的映射关系
        public static Dictionary<string, string> ImgPathDicts = new Dictionary<string, string>();
        static WebConstants()//静态构造函数,只执行一次(当访问静态变量或创建第一次实例时执行),一般用于初始化静态变量
        {
            ImgPathDicts.Add("ProductCategoryImg", "/Upload/Product/ProductCategoryImg/");
            ImgPathDicts.Add("ProductSlideImgs", "/Upload/Product/ProductSlideImgs/");
            ImgPathDicts.Add("ProductMainImg", "/Upload/Product/ProductMainImg/");
            ImgPathDicts.Add("ProductDetail", "/Upload/Product/ProductDetail/");
            ImgPathDicts.Add("BrandImg", "/Upload/Product/BrandImg/");
            ImgPathDicts.Add("BrandLogo", "/Upload/Product/BrandLogo/");
            ImgPathDicts.Add("SkuImg", "/Upload/Product/SkuImg/");
        }
    }
}
