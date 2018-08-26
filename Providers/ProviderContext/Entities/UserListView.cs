using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{

    [ComplexType()]
    public class UserListView
    {
        [Column("user_name", Order = 1)]
        public string user_name { get; set; }

        [Column("first_name", Order = 2)]
        public string  first_name { get; set; }

        [Column("second_name", Order = 3)]
        public string  second_name { get; set; }

        [Column("email", Order = 4)]
        public string email { get; set; }

        [Column("contact_number", Order = 5)]
        public string    contact_number { get; set; }

        [Column("group_name", Order = 6)]
        public string   group_name { get; set; }

        public string full_name { get { return (String.IsNullOrEmpty(first_name) ? "" : first_name) + " " + (String.IsNullOrEmpty(second_name) ? "" : second_name); } }
    }
}