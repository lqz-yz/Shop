using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
namespace API.Models
{
    public class OrderVModel
    {
        public List<OrderSchedule>  Goods { get; set; }
        public string Consignee_addr { get; set; }
        public int Order_price { get; set; }
        public string token { get; set; }
        

    }
}