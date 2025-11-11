using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Utilities.Helpers
{
    public class RoleHelper
    {
        public const string User = "user";
        public const string Admin = "admin";

        public static readonly string[] AllRoles = [User, Admin];

        public static bool IsValidRole(string role) => AllRoles.Contains(role);
    }
}
