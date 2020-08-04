using DAL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class AdminBLL : BaseBLL<Admin, AdminDAL>, IAdminBLL
    {
        public  List<Admin> Search(string Name,string Password)
        {
            return dal.Search( Name, Password);
        }
    }
}
