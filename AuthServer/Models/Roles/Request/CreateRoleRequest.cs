namespace AuthServer.Models.Roles.Request
{
    public class CreateRoleRequest
    {
        public string Name { get; set; }
        public int Rank { get; set; }
        public bool IsPublic { get; set; }
        //public bool CanBeDeleted { get; set; }
    }
}
