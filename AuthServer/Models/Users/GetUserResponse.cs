using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models.Users
{
    public class GetUserResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsDefault { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string AccountNumber { get; set; }
        public bool IsEnabled { get; set; }
    }
}
