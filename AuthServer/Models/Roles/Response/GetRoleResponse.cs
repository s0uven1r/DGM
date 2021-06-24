namespace AuthServer.Models.Roles.Response
{
    public class GetRoleResponse
    {
        public string Id { get; set; }
        public string  Name { get; set; }
        public bool IsPublic { get; set; }
        public bool IsDefault { get; set; }
        public int Rank { get; set; }
    }
}
