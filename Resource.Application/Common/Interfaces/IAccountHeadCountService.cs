using System.Threading.Tasks;

namespace Resource.Application.Common.Interfaces
{
    public interface IAccountHeadCountService
    {
        Task<string> GenerateAccountNumber(string type, string alias);
    }
}
