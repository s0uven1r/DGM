using System.Text.Json.Serialization;

namespace Resource.Application.Models.Account.AccountHead.Request
{
    public class AccountHeadUpdateViewModel
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string Title { get; set; }
        public string AccountTypeId { get; set; }
    }
}
