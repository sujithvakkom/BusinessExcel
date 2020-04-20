using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [Table(name: "delivery", Schema = "dbo")]
    public class delivery
    {
        [Column("id")]
        public int id { get; set; }

        [Column("ROWID")]
        public int ROWID { get; set; }

        [Column("TRANS_TYPE")]
        public string TRANS_TYPE { get; set; }

        [Column("RECEIPTID")]
        public string RECEIPTID { get; set; }

        [Column("TRANSACTIONID")]
        public string TRANSACTIONID { get; set; }

        [Column("TRANSDATE")]
        public DateTime? TRANSDATE { get; set; }

        [Column("LINENUM")]
        public decimal LINENUM { get; set; }

        [Column("ITEMID")]
        public string ITEMID { get; set; }

        [Column("DESCRIPTION")]
        public string DESCRIPTION { get; set; }

        [Column("QUANTITY")]
        public decimal? QUANTITY { get; set; }

        [Column("SEIINGPRICE")]
        public decimal? SEIINGPRICE { get; set; }

        [Column("COMMENT")]
        public string COMMENT { get; set; }

        [Column("DELIVARYTYPE")]
        public string DELIVARYTYPE { get; set; }

        [Column("ACCOUNTNUM")]
        public string ACCOUNTNUM { get; set; }

        [Column("NAME")]
        public string NAME { get; set; }

        [Column("STREET")]
        public string STREET { get; set; }

        [Column("ADDRESS")]
        public string ADDRESS { get; set; }

        [Column("DELIVERY_ADDRESS")]
        public string DELIVERY_ADDRESS { get; set; }

        [Column("EMIRATE")]
        public string EMIRATE { get; set; }

        [Column("GPS")]
        public string GPS { get; set; }

        [Column("REQ_NUMBER")]
        public string REQ_NUMBER { get; set; }

        [Column("INT_STATUS")]
        public decimal? INT_STATUS { get; set; }

        [Column("DT_STATUS")]
        public DateTime? DT_STATUS { get; set; }

        [Column("DT_SCHEDULED")]
        public DateTime? DT_SCHEDULED { get; set; }

        [Column("REMARKS")]
        public string REMARKS { get; set; }

        [Column("DT_ESTIMATED")]
        public DateTime? DT_ESTIMATED { get; set; }

        [Column("LPO_NUMBER")]
        public string LPO_NUMBER { get; set; }

        [Column("TIME_SLOT")]
        public string TIME_SLOT { get; set; }

        [Column("SCHEDULED_ASPER")]
        public string SCHEDULED_ASPER { get; set; }

        [Column("DRIVER_NAME")]
        public string DRIVER_NAME { get; set; }

        [Column("FAILED_REASON")]
        public string FAILED_REASON { get; set; }

        [Column("CUST_NO")]
        public int? CUST_NO { get; set; }

        [Column("SITE_NO")]
        public int? SITE_NO { get; set; }

        [Column("EMP_ID")]
        public int? EMP_ID { get; set; }

        [Column("CREATED_ON")]
        public DateTime? CREATED_ON { get; set; }

        [Column("WARRANTY")]
        public String WARRANTY { get; set; }

        [Column("RECEIPT_CREATED")]
        public int? RECEIPT_CREATED { get; set; }

        [Column("ORA_REQ_NUMBER")]
        public String ORA_REQ_NUMBER { get; set; }

        [Column("ORA_ORDER_NO")]
        public String ORA_ORDER_NO { get; set; }

        [Column("ORA_ORDER_STATUS")]
        public String ORA_ORDER_STATUS { get; set; }

        [Column("ORA_SHIPMENT_STATUS")]
        public String ORA_SHIPMENT_STATUS { get; set; }

        [Column("SHIPMENT_NUMBER")]
        public String SHIPMENT_NUMBER { get; set; }

        [Column("RECEIPT_NUMBER")]
        public String RECEIPT_NUMBER { get; set; }

        [Column("REQUISITION_HEADER_ID")]
        public decimal? REQUISITION_HEADER_ID { get; set; }

        [Column("REQUISITION_LINE_ID")]
        public decimal? REQUISITION_LINE_ID { get; set; }

        [Column("VEHICLE_CODE")]
        public String VEHICLE_CODE { get; set; }

        [Column("GPS_CORDINATES")]
        public String GPS_CORDINATES { get; set; }

        [Column("MAP_URL")]
        public String MAP_URL { get; set; }

    }
}