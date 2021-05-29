using System.Collections.Generic;

namespace Dgm.Common.Authorization.MenuControl.Identity
{
    public class IdentityMenuConstant
    {
        public static KeyValuePair<string, string> Identity = new("Identity", "identity");
        public static KeyValuePair<string, string> IdentityCreate = new("Registration", "identity-reg");

        public static KeyValuePair<string, string> Role = new("Role", "role");
        public static KeyValuePair<string, string> RoleCreate = new("Role", "create");
        public static KeyValuePair<string, string> RoleUpdate = new("Role", "update");

        public static KeyValuePair<string, string> Permission = new("Permission", "permission");
        public static KeyValuePair<string, string> PermissionUpdate = new("Permission", "update");

    }
}
