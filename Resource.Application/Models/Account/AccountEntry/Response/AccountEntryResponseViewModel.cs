using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Application.Models.Account.AccountEntry.Response
{
    public class AccountEntryResponseViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string AccountNumber { get; set; }
        public string Type { get; set; }
        public string EntryDateNP { get; set; }
        public string EntryDateEN { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string Remarks { get; set; }

        public List<JournalEntry> JournalEntries { get; set; }
    }

    public class JournalEntry
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string AccountNumber { get; set; }
        public string Type { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string Remarks { get; set; }
    }

}
