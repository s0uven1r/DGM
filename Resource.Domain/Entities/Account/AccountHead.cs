namespace Resource.Domain.Entities.Account
{
    public class AccountHead : BaseEntity
    {
        public string Title { get; set; }
        public string AccountTypeId { get; set; }
        public string AccountNumber { get; set; }
        public virtual AccountType AccountType { get; set; }
    }
}
