using DAL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductBrandBLL:BaseBLL<ProductBrand, ProductBrandDAL>, IProductBrandBLL
    {
    }
}
