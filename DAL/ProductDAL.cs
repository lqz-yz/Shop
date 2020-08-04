using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductDAL : BaseDAL<Product>, IProductDAL
    {
        public override List<Product> Search<TKey>(int pageSize, int pageIndex, bool isDesc, Func<Product, TKey> orderkey, Expression<Func<Product, bool>> where, out int count)
        {
            count = entities.Set<Product>().Where(where).Count();
            if (isDesc)
            {
                var list = entities.Product.Join(entities.ProductBrand, pc => pc.ProductBrandID, pb => pb.ID, (pc, pb) => new ProductModel()
                {
                    ID = pc.ID,
                    ProductTitle = pc.ProductTitle,
                    ProductMainImg = pc.ProductMainImg,
                    ProductSlideImgs = pc.ProductSlideImgs,
                    ProductCategoryID = pc.ProductCategoryID,
                    Price = pc.Price,
                    Stock = pc.Stock,
                    keyWords = pc.keyWords,
                    ProductCategoryPath = pc.ProductCategoryPath,
                    ProductSkuValues = pc.ProductSkuValues,
                    ProductDetail = pc.ProductDetail,
                    ProductBrandID = pc.ProductBrandID,
                    BrandName = pb.BrandName

                }
                    ).Where(where).OrderByDescending(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return list;
            }
            else
            {
                var list = entities.Product.Join(entities.ProductBrand,
                   pc => pc.ProductBrandID,
                   pb => pb.ID,
                   (pc, pb) => new ProductModel()
                   {
                       ID = pc.ID,
                       ProductTitle = pc.ProductTitle,
                       ProductMainImg = pc.ProductMainImg,
                       ProductSlideImgs = pc.ProductSlideImgs,
                       ProductCategoryID = pc.ProductCategoryID,
                       Price = pc.Price,
                       Stock = pc.Stock,
                       keyWords = pc.keyWords,
                       ProductCategoryPath = pc.ProductCategoryPath,
                       ProductSkuValues = pc.ProductSkuValues,
                       ProductDetail = pc.ProductDetail,
                       ProductBrandID = pc.ProductBrandID,
                       BrandName = pb.BrandName

                   }
                   ).Where(where).OrderBy(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return list;
            }
        }
    }
}
