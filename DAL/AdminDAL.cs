using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class AdminDAL : BaseDAL<Admin>, IAdminDAL
    {


        public new List<Admin> Search(string Name, string Password)
        {
            return entities.Admin.Where(x => x.Name == Name && x.Password == Password).ToList();
        }
    }
}
