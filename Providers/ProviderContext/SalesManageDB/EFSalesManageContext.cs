using DBSalesManage;

namespace BusinessExcel.Providers.ProviderContext.SalesManageDB
{
    public class EFSalesManageContext : DBSalesmanageEntities
    {
        public EFSalesManageContext()
            : base("SalesManageData") { }
    }
}