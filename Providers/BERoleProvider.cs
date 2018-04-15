using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace BusinessExcel.Providers
{
    /*

    This provider works with the following schema for the tables of role data.

    CREATE TABLE Roles
    (
      Rolename Text (255) NOT NULL,
      ApplicationName Text (255) NOT NULL,
        CONSTRAINT PKRoles PRIMARY KEY (Rolename, ApplicationName)
    )

    CREATE TABLE UsersInRoles
    (
      Username Text (255) NOT NULL,
      Rolename Text (255) NOT NULL,
      ApplicationName Text (255) NOT NULL,
        CONSTRAINT PKUsersInRoles PRIMARY KEY (Username, Rolename, ApplicationName)
    )

    */ 
    public class BERoleProvider:SimpleRoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            return base.IsUserInRole(username, roleName);
        }
        public override string[] GetAllRoles()
        {
            return base.GetAllRoles();
        }
    }
}