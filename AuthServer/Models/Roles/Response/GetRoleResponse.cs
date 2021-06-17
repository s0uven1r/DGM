using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models.Roles.Response
{
    public class GetRoleResponse
    {
        public string Id { get; set; }
        public string  Name { get; set; }
        public bool IsPublic { get; set; }
        public bool IsDefault { get; set; }
    }
}
