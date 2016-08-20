namespace BusinessExcel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BusinessExcelDataModel : DbContext
    {
        public BusinessExcelDataModel()
            : base("name=BusinessExcelData")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
