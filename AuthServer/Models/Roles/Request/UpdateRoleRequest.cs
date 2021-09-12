namespace AuthServer.Models.Roles.Request
{
    public class UpdateRoleRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Rank { get; set; }
    }
}
