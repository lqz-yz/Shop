//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderSchedule:BaseModel
    {
        
        public Nullable<int> OrderMasterID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> ProductNum { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public Nullable<int> SkuID { get; set; }
    }
}
