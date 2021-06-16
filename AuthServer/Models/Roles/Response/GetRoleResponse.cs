using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models.Roles.Response
{
    public class GetRoleResponse
    {
        public object Id { get; set; }
        public object Name { get; set; }
        public object IsPublic { get; set; }
    }
}
