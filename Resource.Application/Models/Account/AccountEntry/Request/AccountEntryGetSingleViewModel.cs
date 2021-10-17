using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Application.Models.Account.AccountEntry.Request
{
    public class AccountEntryGetSingleViewModel
    {
        public string Type { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionDateEN { get; set; }
    }
}
