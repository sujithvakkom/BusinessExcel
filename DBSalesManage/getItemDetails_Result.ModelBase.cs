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
    using System.ComponentModel.DataAnnotations.Schema;


    [ComplexType()]
    public partial class getItemDetails
    {
        [Column("inventory_item_id",Order =1)]
        public int inventory_item_id { get; set; }
        [Column("item_code", Order = 2)]
        public string item_code { get; set; }
        [Column("description", Order = 3)]
        public string description { get; set; }
        [Column("price", Order = 4)]
        public Nullable<decimal> price { get; set; }
        [Column("model", Order = 5)]
        public string model { get; set; }
        [Column("model_description", Order = 6)]
        public string model_description { get; set; }
    }
}
/*
inventory_item_id	item_code	description	price	model	model_description
20913	101-1026-000384	JAA793 ** Jaa793df Af-S Dx Z 55-200mm Bk ** Camera & Acc.	NULL	AF-S DX Zoom-NIKKOR 55-200mm f/4-5.6G ED	AF-S DX Zoom-NIKKOR 55-200mm f/4-5.6G ED
  */
