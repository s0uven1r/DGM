using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Domain.Entities.Account
{
    public class ClosingBalance : BaseEntity
    {
        public decimal AccountNumber { get; set; } //salary (employee salary) // vahicle ()
        public decimal Balance { get; set; } //60000 // 70000
    }
}
