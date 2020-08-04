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
    public class ProductAttrKeySkuController : Controller
    {
        // GET: ProductAttrKeySku

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
                OrderNum = productAttrKeyVModel.OrderNum,
                EnterType = productAttrKeyVModel.EnterType,
                IsImg = productAttrKeyVModel.IsImg,
                IsSku = 1,
                ProductCategoryID = productAttrKeyVModel.ProductCategoryID
            };
            attrKeyBll.Add(productAttrKey);
            if (productAttrKeyVModel.AttrValues != null)
            {
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

        public ActionResult GetByCategoryID(int draw, int categoryId)
        {

            var list = attrKeyBll.GetByCategoryID(categoryId).Where(x => x.IsSku == 1).ToList();//.Where(x => x.IsSku == 1)获取IsSku == 1的数据
            //构造返回josn数据{"draw","data":}
            var result = new { draw = draw, data = list };
            return Json(result);
        }
        [HttpGet]
        public ActionResult GetOne(int id)
        {
            //获取Attrkey数据
            var attrkey = attrKeyBll.Getone(id);
            //获取attrvalue的数据
            var attrValue = attrValueBll.GetAllByAttrKeyID(id);
            var data = new { attrkey = attrkey, attrValue = attrValue };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit(ProductAttrKeyVModel productAttrKeyVModel)
        {
            //插入attr表
            ProductAttrKey productAttrKey = new ProductAttrKey()
            {
                ID = productAttrKeyVModel.ID,
                AttrName = productAttrKeyVModel.AttrName,
                OrderNum = productAttrKeyVModel.OrderNum,
                EnterType = productAttrKeyVModel.EnterType,
                IsImg = productAttrKeyVModel.IsImg,
                IsSku = 1,
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
        [HttpGet]
        public ActionResult GetByCategoryID(int categoryID) {
            //获取attrkey表的数据
            var list = attrKeyBll.Search(x => x.ProductCategoryID == categoryID && x.IsSku == 1);
            List<ProductAttrKeyVModel> vList = new List<ProductAttrKeyVModel>();
            foreach (var item in list) {
                var vModel = new ProductAttrKeyVModel();
                vModel.ID = item.ID;
                vModel.AttrName = item.AttrName;
                vModel.EnterType = item.EnterType; 
                vModel.IsImg = item.IsImg;
                vModel.AttrValues = new List<string>();
                //获取attrValue表的数据
                var attrvalus = attrValueBll.Search(x => x.ProductAttrKeyID == item.ID);
                foreach (var valuesItem in attrvalus) {
                    vModel.AttrValues.Add(valuesItem.AttrValue);
                }
                vList.Add(vModel);
            }
            return Json(vList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ByCategoryID(int categoryID)
        {
            //获取attrkey表的数据
            var list = attrKeyBll.Search(x => x.ProductCategoryID == categoryID && x.IsSku == 0);
            List<ProductAttrKeyVModel> vList = new List<ProductAttrKeyVModel>();
            foreach (var item in list)
            {
                var vModel = new ProductAttrKeyVModel();
                vModel.ID = item.ID;
                vModel.AttrName = item.AttrName;
                vModel.EnterType = item.EnterType;
                vModel.IsImg = item.IsImg;
                vModel.AttrValues = new List<string>();
                //获取attrValue表的数据
                var attrvalus = attrValueBll.Search(x => x.ProductAttrKeyID == item.ID);
                foreach (var valuesItem in attrvalus)
                {
                    vModel.AttrValues.Add(valuesItem.AttrValue);
                }
                vList.Add(vModel);
            }
            return Json(vList, JsonRequestBehavior.AllowGet);
        }

    }
}