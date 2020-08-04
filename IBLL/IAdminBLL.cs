using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
  public interface IAdminBLL : IBaseBLL<Admin>
    {
         List<Admin> Search(string Name, string Password);
    }
}
