using Dgm.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Application.Command.Customer
{
    public interface ICustomerPackageService
    {
        Task RegisterCustomerPackage(CustomerPackageViewModel model, string accNo);
    }
}
