using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ResponsMessage<T>
    {
        public T Data { get; set; }
        public int Code { get; set; }
        public  string  Message { get; set; }
        
    }
}