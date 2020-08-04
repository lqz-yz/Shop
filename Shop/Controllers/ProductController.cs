using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using IBLL;
using BLL;
using Shop.Models;

namespace Shop.Controllers
{
    public class ProductController : BaseController<Product,IProductBLL>
    {
        public override IProductBLL Bll
        {
            get {
                return new ProductBLL();
            }
        }

        // GET: Product
        //IProductBLL bll = new ProductBLL();
        //public ActionResult Add()
        //{
        //    return View();
        //}
        [HttpPost]
        public ActionResult Add(ProductVModel VModel)
        {
            Product Product = VModel.Product;
            List<ProductSku> Skus = VModel.Skus;
            List<ProductAttr> Attrs = VModel.Attrs;
            //List<ProductSkuImg> SkuImg = VModel.SkuImg;
            Bll.Add(Product, Skus, Attrs);
            return Json(new { State = true, msg = "添加成功" });
        }

        [HttpPost]
        public ActionResult GetAll(int draw,int pageSize,int pageIndex)
        {
            int count; 
            var list = Bll.Search(pageSize, pageIndex, false,x=>x.ID, x=>1==1,out count);
            //var count = Bll.GetCount(x => 1 == 1);
            //    //构造返回josn数据{"draw","data":}
            var result = new {
                draw = draw,
                data = list,
                recordsTotal = count,
                recordsFiltered= count
            };
            return Json(result);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(ProductVModel vModel)//, int id
        {//Product product, List<ProductSku> Skus, List<ProductAttr> Attrs
            Product product = vModel.Product;
            List<ProductSku> Skus = vModel.Skus;
            List<ProductAttr> Attrs = vModel.Attrs;
            int result = Bll.Update(product,
             Skus,
             Attrs);
            return Json(new { State = true, msg = "修改成功" });
        }
        [HttpGet]
        public virtual ActionResult Getone(int id) {
            List<ProductAttr> attrs;
            List< ProductSku > skus;
            //List<ProductSkuImg> SkuImg;
            var product = Bll.Getone(id,out attrs, out skus);
            var result = new
            {
                product = product,
                attrs = attrs,
                skus = skus,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}