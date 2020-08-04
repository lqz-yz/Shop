using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class SearchVModel
    {
        public string keyWord { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}