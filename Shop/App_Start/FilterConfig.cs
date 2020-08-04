using System.Web;
using System.Web.Mvc;
using Model;
using Shop.Models;

namespace Shop
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LoginCheckAttribute());
        }
    }
}
