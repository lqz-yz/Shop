using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using IBLL;
using BLL;
namespace Shop.Controllers
{
    //线程和进程
    //当请求到达服务端之后,服务器会分配一个线程去处理该请求,该线程会有一个容器,可以将EF上下文放到该容器中
    public class ProductAttrKeyController : Controller
    {
        IProductAttrKeyBLL attrKeyBll = new ProductAttrKeyBLL();
        IProductAttrValueBLL attrValueBll = new ProductAttrValueBLL();

        // GET: ProductAttrKey
        public ActionResult List(int id)
        {
            //ViewBag.ProductCategoryID = id;
            return View();
        }
        [HttpPost] 
        public ActionResult Add(ProductAttrKeyVModel productAttrKeyVModel)
        {
            //插入attr表
            ProductAttrKey productAttrKey = new ProductAttrKey()
            {
                AttrName = productAttrKeyVModel.AttrName,
                OrderNum= productAttrKeyVModel.OrderNum,
                EnterType= productAttrKeyVModel.EnterType,
                IsSku=0,
                ProductCategoryID= productAttrKeyVModel.ProductCategoryID
            };
            attrKeyBll.Add(productAttrKey);
            if (productAttrKeyVModel.AttrValues != null) {
                foreach (var item in productAttrKeyVModel.AttrValues)
                {
                    ProductAttrValue productAttrValue = new ProductAttrValue()
                    {
                        AttrValue = item,
                        ProductAttrKeyID = productAttrKey.ID
                    };
                    attrValueBll.Add(productAttrValue);
                }
            }
            //插入value表
           
            return Json(new { State = true, msg = "添加成功" });
        }

        public ActionResult GetByCategoryID(int draw,int categoryId) {

            var list = attrKeyBll.GetByCategoryID(categoryId).Where(x => x.IsSku == 0).ToList();
            //构造返回josn数据{"draw","data":}
            var result = new { draw = draw, data = list };
            return Json(result);
        }
        [HttpGet]
        public ActionResult GetOne(int id) {
            //获取Attrkey数据
            var attrkey= attrKeyBll.Getone(id);
            //获取attrvalue的数据
            var attrValue = attrValueBll.GetAllByAttrKeyID(id);
            var data = new { attrkey = attrkey, attrValue = attrValue };
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit(ProductAttrKeyVModel productAttrKeyVModel) {
            //插入attr表
            ProductAttrKey productAttrKey = new ProductAttrKey()
            {
                ID=productAttrKeyVModel.ID,
                AttrName = productAttrKeyVModel.AttrName,
                OrderNum = productAttrKeyVModel.OrderNum,
                EnterType = productAttrKeyVModel.EnterType,
                IsSku = 0,
                ProductCategoryID = productAttrKeyVModel.ProductCategoryID
            };
            List<ProductAttrValue> attrValues = new List<ProductAttrValue>();
            if (productAttrKeyVModel.AttrValues != null)
            {
                foreach (var item in productAttrKeyVModel.AttrValues)
                {
                    ProductAttrValue productAttrValue = new ProductAttrValue()
                    {
                        AttrValue = item,
                        ProductAttrKeyID = productAttrKey.ID
                    };
                    attrValues.Add(productAttrValue);
                }
            }
            attrKeyBll.Update(productAttrKey, attrValues);
            return Json(new { State = true, msg = "修改成功" });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = attrKeyBll.Delete(id);
            return Json(new { state = result > 0 ? true : false });
        }

        //public ActionResult GetCity(string id)
        //{
        //    AreaService _areaSvc = new AreaService();
        //    List<SyArea> syAreaList = _areaSvc.GetList(id);
        //    return Json(syAreaList, JsonRequestBehavior.AllowGet);
        //}


        //public ActionResult GetCityList()
        //{
        //    return View();
        //}
    }
}