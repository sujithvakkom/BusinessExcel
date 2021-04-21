using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Hosting;

namespace DataProvider.Entities.DeliveryJob
{
    public class DeliveryHeader : DeliverySchedule
    {
        [Display(Name = "HeaderId")]
        public Int32 HeaderId { get; set; }

        [Display(Name = "Receipt")]
        public String Receipt { get; set; }

        [Display(Name = "OrderNumber")]
        public String OrderNumber { get; set; }

        [Display(Name = "CustomerName")]
        public String CustomerName { get; set; }

        [Display(Name = "SaleDate")]
        public DateTime? SaleDate { get; set; }

        [Display(Name = "Phone")]
        public String Phone { get; set; }

        [Display(Name = "DeliveryAddress")]
        public String DeliveryAddress { get; set; }

        [Display(Name = "DeliveryLines")]
        public List<DeliveryLine> DeliveryLines { get; set; }

        [Display(Name = "saleType")]
        public String saleType { get; set; }

        [Display(Name = "deliveryType")]
        public String deliveryType { get; set; }

        [Display(Name = "attachmentName")]
        public String attachmentName { get; set; }

        [Display(Name = "Retailer")]
        public String Retailer { get; set; }

        [Display(Name = "Price")]
        public Decimal Price { get; set; }

        public string getPlainText()
        {

            var order_mail_plane = HostingEnvironment.MapPath("~/assets/order_mail_plane.txt");
            StringBuilder textContent = new StringBuilder();
            try
            {
                String line = "";
                using (System.IO.StreamReader textReader = new System.IO.StreamReader(order_mail_plane))
                {

                    while ((line = textReader.ReadLine()) != null)
                    {
                        textContent.Append(line);
                        textContent.Append(Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                textContent.Append(ex.Message);
            }

            textContent.Replace("{Order_Number}", this.OrderNumber);
            //textContent.Replace("{Customer_Code}", status[1]);
            textContent.Replace("{Customer_Name}", this.CustomerName);
            //textContent.Replace("{Site_Name}", status[3]);
            textContent.Replace("{Remarks}", this.Remarks);
            //textContent.Replace("{Total_Qty}", status[5]);
            //textContent.Replace("{Total_Amt}", status[6]);
            textContent.Replace("{Table_Body}", GetOrderItemDetaisAsTabFormatedText());
            return textContent.ToString();
        }

        public string getHtml(String Receipt, string UserFullName)
        {

            /*
             
                    Server.MapPath(),
                    Server.MapPath("~/order_mail_plane.txt"),
             */

            var order_mail = HostingEnvironment.MapPath("~/assets/order_mail.txt");
            StringBuilder htmlContent = new StringBuilder();
            try
            {
                String line = "";
                using (System.IO.StreamReader htmlReader = new System.IO.StreamReader(order_mail))
                {

                    while ((line = htmlReader.ReadLine()) != null)
                    {
                        htmlContent.Append(line);
                        htmlContent.Append(Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                htmlContent.Append(ex.Message);
            }

            htmlContent.Replace("{Order_Number}",Receipt);
            //htmlContent.Replace("{Customer_Code}", status[1]);
            htmlContent.Replace("{Customer_Name}", this.CustomerName);
            //htmlContent.Replace("{Site_Name}", status[3]);
            htmlContent.Replace("{Remarks}", this.Remarks);
            htmlContent.Replace("{Site}", this.Retailer);
            htmlContent.Replace("{Promoter}", UserFullName);
            //htmlContent.Replace("{Total_Qty}", status[5]);
            //htmlContent.Replace("{Total_Amt}", status[6]);
            htmlContent.Replace("{Table_Body}", this.getOrderBodyHtml());
            htmlContent.Replace("{Subject}", "Order Created :"+this.OrderNumber);
            return htmlContent.ToString();
        }

        private string GetEmployName()
        {
            using (SalesManageDataContext context = new SalesManageDataContext()) {
                var user = context.getUserDetail(this.UserName);
                if (user != null) { return user.full_name; }
            }
            return "";
        }

        private string getOrderBodyHtml()
        {
            const String TABLE_ROW = @"<tr>";
            const String TABLE_DATA = "<td class=\"table_data\"";
            const String TABLE_ROW_END = @"</tr>";
            const String TABLE_DATA_END = "</td>";
            const String TABLE_DATA_ALIGN = " align=\"right\"";
            const String ANGLE_BRC_RIGHT = ">";

            System.Text.StringBuilder htmlContent = new System.Text.StringBuilder();
            foreach (var row in this.DeliveryLines)
            {
                htmlContent.Append(TABLE_ROW);

                htmlContent.Append(TABLE_DATA);
                if (row.ItemCode.GetType() == typeof(System.Decimal))
                {
                    htmlContent.Append(TABLE_DATA_ALIGN);
                }
                htmlContent.Append(ANGLE_BRC_RIGHT);
                htmlContent.Append(row.ItemCode.ToString());
                htmlContent.Append(TABLE_DATA_END);

                htmlContent.Append(TABLE_DATA);
                if (row.Description.GetType() == typeof(System.Decimal))
                {
                    htmlContent.Append(TABLE_DATA_ALIGN);
                }
                htmlContent.Append(ANGLE_BRC_RIGHT);
                htmlContent.Append(row.Description.ToString());
                htmlContent.Append(TABLE_DATA_END);

                htmlContent.Append(TABLE_DATA);
                if (row.OrderQuantity.GetType() == typeof(System.Decimal))
                {
                    htmlContent.Append(TABLE_DATA_ALIGN);
                }
                htmlContent.Append(ANGLE_BRC_RIGHT);
                htmlContent.Append(row.OrderQuantity.ToString());
                htmlContent.Append(TABLE_DATA_END);

                htmlContent.Append(TABLE_DATA);
                if (row.SelleingPrice.GetType() == typeof(System.Decimal))
                {
                    htmlContent.Append(TABLE_DATA_ALIGN);
                }
                htmlContent.Append(ANGLE_BRC_RIGHT);
                htmlContent.Append(row.SelleingPrice.ToString());
                htmlContent.Append(TABLE_DATA_END);

                htmlContent.Append(TABLE_ROW_END);
            }
            return htmlContent.ToString();
        }



        /// <summary>
        /// Create Text Report of the Order
        /// </summary>
        /// <param name="Order">Order Number</param>
        /// <returns>Plain String</returns>
        private String GetOrderItemDetaisAsTabFormatedText()
        {
            //Just to cutout needy spaces.
            const String DISPLACE = @"                                   ";
            //Format Column widths
            int[] colWidth = { 15 + 1, 30 + 1, 7 + 1, 15 + 1 };

            System.Text.StringBuilder htmlContent = new System.Text.StringBuilder();
            int colCount = 0;
            string temp = "";
            foreach (var row in this.DeliveryLines)
            {
                colCount = 0;
                    temp = row.ItemCode;
                    try
                    {
                        temp = temp.Substring(0, colWidth[colCount]);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                    }
                    if (row.ItemCode.GetType() == typeof(System.Decimal))
                    {
                        //Right Align
                        if (temp.Length < colWidth[colCount])
                        {
                            temp = DISPLACE.Substring(0, (colWidth[colCount] - temp.Length)) + temp;
                        }
                    }
                    else
                    {
                        //Left Align
                        if (temp.Length < colWidth[colCount])
                        {
                            temp += DISPLACE.Substring(0, (colWidth[colCount] - temp.Length));
                        }
                    }
                    htmlContent.Append(temp);
                    colCount++;


                temp = row.Description;
                try
                {
                    temp = temp.Substring(0, colWidth[colCount]);
                }
                catch (ArgumentOutOfRangeException)
                {
                }
                if (row.ItemCode.GetType() == typeof(System.Decimal))
                {
                    //Right Align
                    if (temp.Length < colWidth[colCount])
                    {
                        temp = DISPLACE.Substring(0, (colWidth[colCount] - temp.Length)) + temp;
                    }
                }
                else
                {
                    //Left Align
                    if (temp.Length < colWidth[colCount])
                    {
                        temp += DISPLACE.Substring(0, (colWidth[colCount] - temp.Length));
                    }
                }
                htmlContent.Append(temp);
                colCount++;


                temp = row.OrderQuantity.ToString();
                try
                {
                    temp = temp.Substring(0, colWidth[colCount]);
                }
                catch (ArgumentOutOfRangeException)
                {
                }
                if (row.ItemCode.GetType() == typeof(System.Decimal))
                {
                    //Right Align
                    if (temp.Length < colWidth[colCount])
                    {
                        temp = DISPLACE.Substring(0, (colWidth[colCount] - temp.Length)) + temp;
                    }
                }
                else
                {
                    //Left Align
                    if (temp.Length < colWidth[colCount])
                    {
                        temp += DISPLACE.Substring(0, (colWidth[colCount] - temp.Length));
                    }
                }
                htmlContent.Append(temp);
                colCount++;


                temp = row.SelleingPrice.ToString();
                try
                {
                    temp = temp.Substring(0, colWidth[colCount]);
                }
                catch (ArgumentOutOfRangeException)
                {
                }
                if (row.ItemCode.GetType() == typeof(System.Decimal))
                {
                    //Right Align
                    if (temp.Length < colWidth[colCount])
                    {
                        temp = DISPLACE.Substring(0, (colWidth[colCount] - temp.Length)) + temp;
                    }
                }
                else
                {
                    //Left Align
                    if (temp.Length < colWidth[colCount])
                    {
                        temp += DISPLACE.Substring(0, (colWidth[colCount] - temp.Length));
                    }
                }
                htmlContent.Append(temp);
                colCount++;

                htmlContent.Append(Environment.NewLine);
            }
            return htmlContent.ToString();
        }
        public static String[] GetEmailReceipients() {

            var EmailReceipients = HostingEnvironment.MapPath("~/assets/EmailReceipients.txt");
            List<String> textContent = new List<string>();
            try
            {
                String line = "";
                using (System.IO.StreamReader textReader = new System.IO.StreamReader(EmailReceipients))
                {

                    while ((line = textReader.ReadLine()) != null)
                    {
                        textContent.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return textContent.ToArray();
        }
    }
}