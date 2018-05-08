﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    /*
user_id	int
user_name	nvarchar(50)
first_name	nvarchar(50)
second_name	nvarchar(50)
password	nvarchar(50)
display_name	nvarchar(50)
create_date	datetime
is_active	int
end_date	datetime
email	nvarchar(50)
role_id	int
contact_number	nvarchar(15)
emp_code_lookup	varchar(6)
	
     */
    [Table(name: "user_m",Schema = "sc_salesmanage_user")]
    public class UserM
    {
        [Key]
        [Column("user_id")]
        public int UserID { get; set; }
        [Column("user_name")]
        public int UserName { get; set; }
        [Column("first_name")]
        public int FirstName { get; set; }
        [Column("second_name")]
        public int SecondName { get; set; }
        [Column("display_name")]
        public int DisplayName { get; set; }
        [Column("create_date")]
        public int CreateDate { get; set; }
        [Column("is_active")]
        public int IsActive { get; set; }
        [Column("end_date")]
        public int EndDate { get; set; }
        [Column("email")]
        public int Email { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("contact_number")]
        public int ContactNumber { get; set; }
        [Column("emp_code_lookup")]
        public int EmpCodeLookup { get; set; }
    }
}