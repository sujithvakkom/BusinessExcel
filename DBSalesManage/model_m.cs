//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBSalesManage
{
    using System;
    using System.Collections.Generic;
    
    public partial class model_m
    {
        public model_m()
        {
            this.inventory_item_m = new HashSet<inventory_item_m>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public int model_id { get; set; }
        public string description { get; set; }
        public string model { get; set; }
        public decimal unit_selling_price { get; set; }
    
        public virtual ICollection<inventory_item_m> inventory_item_m { get; set; }
    }
}
