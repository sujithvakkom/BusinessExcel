using WebMatrix.WebData;

namespace BusinessExcel.Authenticator
{

    public sealed class BEMembershipProvider : SimpleMembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            return base.ValidateUser(username, password);
        }

        internal static string MembershipTableName
        {
            get { return "webpages_Membership_test"; }
        }

        internal static string OAuthMembershipTableName
        {
            get { return "webpages_OAuthMembership"; }
        }

        internal static string OAuthTokenTableName
        {
            get { return "webpages_OAuthToken"; }
        }
    }
}