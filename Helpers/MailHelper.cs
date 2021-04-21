using DataProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace BusinessExcel.Helpers
{
    public class MailHelper
    {
        /*
        #region
        const string MAIL_FROM = "vansalesgs@gmail.com";
        const string MAIL_FROM_DISPLAY = "Vansales Gs";
        const string DEFAULT_MAIL_TO = "srkrishnan@grandstores.ae";
        const string VANSALE_ORDER_REQUEST = "Vansale Order Request- ";
        const string THREAD_NAME = @"Mailer";
        #endregion
        private SmtpClient client;

        public MailHelper()
        {
            client = new SmtpClient() { EnableSsl = true };
        }

        private MailMessage Message { get; set; }

        /// <summary>
        /// Mail send call back
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">E contains User state that is the order number</param>
        /// 
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            string order = e.UserState.ToString();
            //Update Notifier

        }

        /// <summary>
        /// Sent Vansale Order Request Mail Async
        /// </summary>
        /// <param name="order">Order Number</param>
        /// <param name="filePathHTML">HTML Place Holded Server Mapped File Path</param>
        /// <param name="filePathText">Text Place Holded Server Mapped File Path</param>
        /// <returns></returns>
        internal String SendAsynMail(string order, string filePathHTML, string filePathText, string attachmentPath)
        {
                string output = "";

            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                db.UpdateLog("Trying sending Mail", "Start Mailing Process");
                string[] addresses = VansaleDataBaseHelper.GetMailAddressesFor(order);
                string addressListCSV = string.Join(",", addresses);
                Message = new MailMessage();
                Message.From = new MailAddress(MAIL_FROM, MAIL_FROM_DISPLAY);
                Message.Sender = new MailAddress(MAIL_FROM, MAIL_FROM_DISPLAY);
                //Remove this line for publishing
                //addressListCSV = DEFAULT_MAIL_TO;
                Message.To.Add(addressListCSV);
                Message.Subject = VANSALE_ORDER_REQUEST + order;
                System.Text.StringBuilder htmlContent = new System.Text.StringBuilder();
                try
                {
                    String line = "";
                    using (System.IO.StreamReader htmlReader = new System.IO.StreamReader(filePathHTML))
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
                System.Text.StringBuilder textContent = new System.Text.StringBuilder();
                try
                {
                    String line = "";
                    using (System.IO.StreamReader textReader = new System.IO.StreamReader(filePathText))
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
                string[] status = VansaleDataBaseHelper.GetOrderSummary(order);
                if (status.Length > 0)
                {
                    htmlContent.Replace("{Order_Number}", status[0]);
                    htmlContent.Replace("{Customer_Code}", status[1]);
                    htmlContent.Replace("{Customer_Name}", status[2]);
                    htmlContent.Replace("{Site_Name}", status[3]);
                    htmlContent.Replace("{Remarks}", status[4]);
                    htmlContent.Replace("{Total_Qty}", status[5]);
                    htmlContent.Replace("{Total_Amt}", status[6]);
                    htmlContent.Replace("{Table_Body}", GetOrderItemDetaisAsHTML(order));
                    htmlContent.Replace("{Subject}", Message.Subject);

                    textContent.Replace("{Order_Number}", status[0]);
                    textContent.Replace("{Customer_Code}", status[1]);
                    textContent.Replace("{Customer_Name}", status[2]);
                    textContent.Replace("{Site_Name}", status[3]);
                    textContent.Replace("{Remarks}", status[4]);
                    textContent.Replace("{Total_Qty}", status[5]);
                    textContent.Replace("{Total_Amt}", status[6]);
                    textContent.Replace("{Table_Body}", GetOrderItemDetaisAsTabFormatedText(order));

                    LinkedResource logo = null;
                    try
                    {
                        logo = new LinkedResource(attachmentPath, MediaTypeNames.Image.Gif);
                        logo.ContentId = "companylogo";
                    }
                    catch (Exception) { }
                    ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                    // Add the alternate body to the message.
                    mimeType.MediaType = MediaTypeNames.Text.Html;
                    AlternateView alternateHTML = AlternateView.CreateAlternateViewFromString(htmlContent.ToString(), mimeType);
                    if (logo != null)
                        alternateHTML.LinkedResources.Add(logo);
                    mimeType.MediaType = MediaTypeNames.Text.Plain;
                    AlternateView alternateText = AlternateView.CreateAlternateViewFromString(textContent.ToString(), mimeType);

                    Message.AlternateViews.Add(alternateHTML);
                    Message.AlternateViews.Add(alternateText);

                    Message.Body = textContent.ToString();
                    Message.IsBodyHtml = true;

                    Message.From = new MailAddress(MAIL_FROM, MAIL_FROM_DISPLAY);
                    Message.ReplyTo = new MailAddress(MAIL_FROM, MAIL_FROM_DISPLAY);
                    Object state = Message;
                    try
                    {
                        client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                        client.SendAsync(Message, order);
                        output = "Mail Sent Please Check.";
                    }
                    catch (Exception ex)
                    {
                        output = ex.Message;
                        DatabaseHelper.VansaleDataBaseHelper.InsertErrorLog(THREAD_NAME, ex.Message);
                    }
                }
                else
                {
                    output = "Mail Not sent. The Order does not exists.";
                }
            }
            return output;
        }

        /// <summary>
        /// Creates HTML Table Rows of Order
        /// </summary>
        /// <param name="Order">Order number</param>
        /// <returns>Plain Text in HTML</returns>
        private String GetOrderItemDetaisAsHTML(String Order)
        {
            const String TABLE_ROW = @"<tr>";
            const String TABLE_DATA = "<td class=\"table_data\"";
            const String TABLE_ROW_END = @"</tr>";
            const String TABLE_DATA_END = "</td>";
            const String TABLE_DATA_ALIGN = " align=\"right\"";
            const String ANGLE_BRC_RIGHT = ">";
            DataTable table = VansaleDataBaseHelper.GetOrderLines(Order);
            System.Text.StringBuilder htmlContent = new System.Text.StringBuilder();
            foreach (DataRow row in table.Rows)
            {
                htmlContent.Append(TABLE_ROW);
                foreach (Object item in row.ItemArray)
                {
                    htmlContent.Append(TABLE_DATA);
                    if (item.GetType() == typeof(System.Decimal))
                    {
                        htmlContent.Append(TABLE_DATA_ALIGN);
                    }
                    htmlContent.Append(ANGLE_BRC_RIGHT);
                    htmlContent.Append(item.ToString());
                    htmlContent.Append(TABLE_DATA_END);
                }
                htmlContent.Append(TABLE_ROW_END);
            }
            return htmlContent.ToString();
        }

        /// <summary>
        /// Create Text Report of the Order
        /// </summary>
        /// <param name="Order">Order Number</param>
        /// <returns>Plain String</returns>
        private String GetOrderItemDetaisAsTabFormatedText(String Order)
        {
            //Just to cutout needy spaces.
            const String DISPLACE = @"                                   ";
            //Format Column widths
            int[] colWidth = { 15 + 1, 30 + 1, 7 + 1, 15 + 1 };
            DataTable table = VansaleDataBaseHelper.GetOrderLines(Order);
            System.Text.StringBuilder htmlContent = new System.Text.StringBuilder();
            int colCount = 0;
            string temp = "";
            foreach (DataRow row in table.Rows)
            {
                colCount = 0;
                foreach (Object item in row.ItemArray)
                {
                    temp = item.ToString();
                    try
                    {
                        temp = temp.Substring(0, colWidth[colCount]);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                    }
                    if (item.GetType() == typeof(System.Decimal))
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
                }
                htmlContent.Append(Environment.NewLine);
            }
            return htmlContent.ToString();
        }
        */
    }
}