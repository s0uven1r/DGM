namespace Resource.Domain.Entities.Account
{
    public class AccountCountTable : BaseEntity
    {
        public string Type { get; set; }
        public int Count { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
