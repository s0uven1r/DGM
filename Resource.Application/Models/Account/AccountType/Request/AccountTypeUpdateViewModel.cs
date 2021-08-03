using System.Text.Json.Serialization;

namespace Resource.Application.Models.Account.AccountType.Request
{
    public class AccountTypeUpdateViewModel
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
    }
}
