using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class AlertModal
    {
        public string TriggerDisplayName { get; set; }
        public string ModalID { get; set; }
        public string ModalLabelID { get; set; }
        public string ModalTitle { get; set; }
        public string Message { get; set; }
        public List<AlertButton> Buttons { get; set; }
    }

    public class AlertButton
    {
        public string ButtonDisplayName { get; set; }
        public string LoadingElementId { get; set; }
        public IDictionary<string, object> buttonAttributes { get; set; }
        public string Image { get; set; }
    }

}