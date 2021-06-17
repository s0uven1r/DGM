using System.Collections.Generic;

namespace AuthServer.Models.Permission
{
    public class RolePermissionViewModel : RolePermissionDetail
    {

    }
    public class RolePermissionDetail
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RolePermissionGroup> RolePermissionGroup { get; set; }
        public RolePermissionDetail()
        {
            RolePermissionGroup = new List<RolePermissionGroup>();
        }
    }
    public class RolePermissionGroup
    {
        public string Module { get; set; }
        public List<PermissionList> PermissionList { get; set; }
        public RolePermissionGroup()
        {
            PermissionList = new List<PermissionList>();
        }
    }

    public class PermissionList
    {
        public string ClaimId { get; set; }
        public string ClaimValue { get; set; }
        public string ClaimTitle { get; set; }
        public bool HasClaim { get; set; }
    }
    public class PermissionManagementViewModel{
        public string RoleId { get; set; }
        public List<ClaimListViewModel> ClaimList { get; set; }
        public PermissionManagementViewModel()
        {
            ClaimList = new List<ClaimListViewModel>();
        }
    }
    public class ClaimListViewModel
    {
        public string Id { get; set; }
    }
}
