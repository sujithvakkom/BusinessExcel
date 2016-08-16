using WebMatrix.WebData;

namespace BusinessExcel.Authenticator
{

    public sealed class BEMembershipProvider : SimpleMembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            return base.ValidateUser(username, password);
        }
    }
}