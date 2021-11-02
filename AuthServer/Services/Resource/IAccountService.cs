using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Services.Resource
{
    public interface IAccountService
    {
        Task<string> GetAccountNumber(string type, string alias);
        Task<string> RegisterCustomerPackage(string type, string alias, string accNo,
            string startDate, string startDateNP, string endDate, string endDateNP,
            string packageId, string shiftId, int paymentGateway, decimal paidAmount, string promoCode);
    }
}
