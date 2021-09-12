using Resource.Domain.Constant;
using Resource.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Infrastructure.Persistence.Seed
{
    public static class SeedAccountHeads
    {
        public static async Task SeedAccountHeadsAsync(AppDbContext dbContext)
        {
            if (!dbContext.AccountHeads.Any())
            {
                List<AccountHead> defaultAccountHeads = new List<AccountHead>
            {
                new AccountHead
                {
                    Id ="6daf74b3-1f78-4f23-aba4-86a45d4e084b",
                    AccountTypeId = "d69de0c5-6d33-4e86-9f6b-71d47d7f62ba",
                    Title = "Trainee Registration",
                    AccountNumber = AccountHeadConstant.TraineeRegistration
                },
                new AccountHead
                {
                    Id ="c64c2b36-3060-4577-be7e-21e74e3c644c",
                    AccountTypeId = "fce90046-c6d8-462a-8b9e-8c419c2e7fcf",
                    Title = "Employee Salary",
                    AccountNumber = AccountHeadConstant.EmployeeSalary
                },
                new AccountHead
                {
                    Id ="f67e3aa0-46c6-4703-ae6f-8d3a813123e9",
                    AccountTypeId = "fce90046-c6d8-462a-8b9e-8c419c2e7fcf",
                    Title = "Vehicle Maintainance",
                    AccountNumber = AccountHeadConstant.VehicleMaintainance
                }
            };
                await dbContext.AccountHeads.AddRangeAsync(defaultAccountHeads);
                await dbContext.SaveChangesForSeedAsync();
            }
        }

    }
}
