using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Services.Resource
{
    public interface IAccountService
    {
        Task<string> GetAccountNumber(string type, string alias);
    }
}
