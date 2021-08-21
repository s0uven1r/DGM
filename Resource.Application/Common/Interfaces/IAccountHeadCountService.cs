using System.Threading.Tasks;

namespace Resource.Application.Common.Interfaces
{
    public interface IAccountHeadCountService
    {
        Task<string> GenerateAccountHeadNumber(string type, int enumVal);
    }
}
