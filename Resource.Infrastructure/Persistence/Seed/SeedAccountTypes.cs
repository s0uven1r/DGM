using Dgm.Common.Enums;
using Resource.Domain.Entities.Account;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Infrastructure.Persistence.Seed
{
    public static class SeedAccountTypes
    {
        public static async Task SeedAccountTypesAsync(AppDbContext dbContext)
        {
            if (!dbContext.AccountTypes.Any())
            {

                List<AccountType> defaultAccountTypes = new List<AccountType>
            {
                new AccountType
                {
                    Id = "d69de0c5-6d33-4e86-9f6b-71d47d7f62ba",
                    Title = "Income",
                    Type = (int)AccountTypeEnum.Income
                },
                 new AccountType
                {
                    Id = "fce90046-c6d8-462a-8b9e-8c419c2e7fcf",
                    Title = "Expense",
                    Type = (int)AccountTypeEnum.Expense
                }
            };

                await dbContext.AccountTypes.AddRangeAsync(defaultAccountTypes);
                await dbContext.SaveChangesForSeedAsync();
            }
        }
    }
}
