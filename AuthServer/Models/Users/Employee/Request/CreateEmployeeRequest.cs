using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models.Users.Employee.Request
{
    public class CreateEmployeeRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
