using DAL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
namespace BLL
{
    public class ProductBLL : BaseBLL<Product, ProductDAL>, IProductBLL
    {
        IProductAttrDAL attrDAL = new ProductAttrDAL();
        IProductSkuDAL skuDAL = new ProductSkuDAL();
        //IProductSkuImgDAL skuImgDAL = new ProductSkuImgDAL();

        public int Add(Product Product, List<ProductSku> Skus, List<ProductAttr> Attrs)
        {
            ////插入Product表
            //dal.Add(Product);
            ////提交获取ProductID
            //SaveChanges();
            ////插入ProductSku表
            //foreach (var sku in Skus)
            //{
            //    sku.ProductID = Product.ID;
            //    skuDAL.Add(sku);
            //}
            ////ProductAttr
            //foreach (var attr in Attrs)
            //{
            //    attr.ProductID = Product.ID;
            //    attrDAL.Add(attr);
            //}
            //return SaveChanges();
            var result = 0;
            var tran = dal.BeginTrane();
            try
            {
                //插入Product表
                dal.Add(Product);
                //提交获取ProductID
                result += SaveChanges();
                //插入ProductSku表
                foreach (var sku in Skus)
                {
                    sku.ProductID = Product.ID;
                    skuDAL.Add(sku);
                }
                //ProductAttr
                foreach (var attr in Attrs)
                {
                    attr.ProductID = Product.ID;
                    attrDAL.Add(attr);
                }
                ////ProductSkuImg
                //foreach (var skuImg in SkuImg)
                //{
                //    skuImg.ProductID = Product.ID;
                //    skuImgDAL.Add(skuImg);
                //}
                result += SaveChanges();
                tran.Commit();//总提交
            }
            catch (Exception)
            {
                tran.Rollback();
            }

            return result;
        }
        public override int Delete(int id)
        {
            //删除商品表
            dal.Delete(id);
            //删除sku表
            var skus = skuDAL.Search(x => x.ProductID == id);
            foreach (var sku in skus)
            {
                skuDAL.Delete(sku);
            }
            //删除attr表
            var attrs = attrDAL.Search(x => x.ProductID == id);
            foreach (var attr in attrs)
            {
                attrDAL.Delete(attr);
            }
            int result = SaveChanges();
            return result;//返回受影响行数
        }
        //public Product Getone(int id, out List<ProductAttr> attrs, out List<ProductSku> skus)
        //{
        //    //默认情况下c#方法返回值只有一个,为了弥补这种缺陷,应使用out参数变相增加返回值
        //    //out 参数:必须在方法体内为其赋值
        //    var product = dal.Getone(id);
        //    attrs = attrDAL.Search(x => x.ProductID == id);
        //    skus = skuDAL.Search(x => x.ProductID == id);
        //    return product;
        //}
        public Product Getone(int id, out List<ProductAttr> attrs, out List<ProductSku> skus)
        {
            //默认情况下c#方法返回值只有一个,为了弥补这种缺陷,应使用out参数变相增加返回值
            //out 参数:必须在方法体内为其赋值
            var product = dal.Getone(id);
            attrs = attrDAL.Search(x => x.ProductID == id);
            skus = skuDAL.Search(x => x.ProductID == id);
            return product;
        }
        public int Update(Product product, List<ProductSku> Skus, List<ProductAttr> Attrs)//, int id
        {
            product.UpdateTime = DateTime.Now;
            //修改商品表
            dal.Update(product);
            //删除sku表
            var skus = skuDAL.Search(x => x.ProductID == product.ID);
            foreach (var sku in skus)
            {
                
                skuDAL.Delete(sku);
            }
            foreach (var sku in Skus)
            {
                sku.ProductID = product.ID;
                skuDAL.Add(sku);//对sku表的insert操作
            }
            //删除attr表
            var attrs = attrDAL.Search(x => x.ProductID == product.ID);
            foreach (var attr in attrs)
            {
               
                attrDAL.Delete(attr);
            }
            foreach (var attr in Attrs)
            {
                attr.ProductID = product.ID;
                attrDAL.Add(attr);//对sku表的insert操作
            }
            int result = SaveChanges();
            return result;

        }
    }
}
