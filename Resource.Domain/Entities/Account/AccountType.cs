using System.Collections.Generic;

namespace Resource.Domain.Entities.Account
{
    public class AccountType : BaseEntity
    {
        public string Title { get; set; }
        public int Type { get; set; }
        public virtual ICollection<AccountHead> AccountHeads { get; set; }
    }
}
