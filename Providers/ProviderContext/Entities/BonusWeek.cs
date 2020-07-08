using BusinessExcel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public class BonusWeek
    {
        [Column("ID", Order = 1)]
        public int Id { get; set; }

        [Column("YEARNUMBER", Order = 2)]
        public int YearNumber { get; set; }

        [Column("WEEKUMBER", Order = 3)]
        public int WeekNumner { get; set; }

        [Column("DATEFROM", Order = 4)]
        public DateTime DateFrom { get; set; }

        [Column("DATETO", Order = 5)]
        public DateTime DateTo { get; set; }

        [Column("DESCRIPTION", Order = 6)]
        public string Description { get; set; }
        public List<BonusSettings> WeekSettings { get; internal set; }

        public bool IsActive() {
            return DateTime.Now.Date <= DateTo.Date && DateTime.Now.Date >= DateFrom.Date;
        }
    }
}

/*
 
ID int IDENTITY(1,1),
YEARNUMBER INT not NULL,
WEEKUMBER	INT not NULL,
DATEFROM	DATE not NULL,
DATETO DATE not NULL,
DESCRIPTION NVARCHAR(50) not NULL
     */
