using Dgm.Common.Enums;
using Resource.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Infrastructure.Persistence.Seed
{
    public static class SeedAccountHeadCountTable
    {
        public static async Task SeedAccountHeadCountAsync(AppDbContext dbContext)
        {
            if (!dbContext.AccountHeadCountTables.Where(x => x.Type == nameof(AccountTypeEnum.Income)).Any())
            {
                dbContext.AccountHeadCountTables.Add(new AccountHeadCountTable
                {
                    Count = 1,
                    Type = nameof(AccountTypeEnum.Income)
                });
                await dbContext.SaveChangesForSeedAsync();
            }
            if (!dbContext.AccountHeadCountTables.Where(x => x.Type == nameof(AccountTypeEnum.Expense)).Any())
            {
                dbContext.AccountHeadCountTables.Add(new AccountHeadCountTable
                {
                    Count = 2,
                    Type = nameof(AccountTypeEnum.Expense)
                });
                await dbContext.SaveChangesForSeedAsync();
            }
        }
    }
}
