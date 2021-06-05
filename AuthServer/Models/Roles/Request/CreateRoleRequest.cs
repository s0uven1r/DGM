using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models.Roles.Request
{
    public class CreateRoleRequest
    {
        public string Name { get; set; }
        public bool IsPublic { get; set; }
    }
}
