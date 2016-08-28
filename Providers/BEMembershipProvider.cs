using BusinessExcel.Providers.Entity;
using WebMatrix.WebData;

namespace BusinessExcel.Providers
{

    public sealed class BEMembershipProvider : SimpleMembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            return base.ValidateUser(username, password);
        }

        public JobList GetJobList(string username)
        {
            JobList list = new Entity.JobList();
            list.Add(new JobList()
            {
                ControllerName = "Test",
                DisplayName = "Test",
                ViewName = ""
            });
            return list;
        }
    }
}