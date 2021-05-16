using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models.Roles.Request
{
    public class CreateRoleRequest
    {
        //[Required]
        //[RegularExpression(@"^(?!.*<[^>]+>).*", ErrorMessage = "HTML tags not allowed.")]
        public string Name { get; set; }
    }
}
