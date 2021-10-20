namespace Dgm.Common.Authorization.Claim.Identity
{
    public class IdentityClaimConstant
    {
        public const string ViewIdentity = "Claim.Identity.Read";
        // user
        public const string ViewUser= "Claim.Identity.User.Read";
        public const string WriteUser = "Claim.Identity.User.Write";

        //role
        public const string ViewRole = "Claim.Role.Read";
        public const string WriteRole = "Claim.Role.Write";

        //permission
        public const string ViewPermission = "Claim.Permission.Read";
        public const string WritePermission = "Claim.Permission.Write";

        //settings
        public const string ViewSettings = "Claim.Settings.Read";
        public const string ViewLogo = "Claim.Logo.Read";

    }
}
