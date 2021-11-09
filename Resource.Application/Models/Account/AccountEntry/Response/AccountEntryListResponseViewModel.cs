using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Application.Models.Account.AccountEntry.Response
{
    public class AccountEntryListResponseViewModel
    {
        public string Id { get; set; }
        public string AccountNumber { get; set; }
        public string EntryDateNP { get; set; }
        public string EntryDateEN { get; set; }
        public string Remarks { get; set; }
    }
}
