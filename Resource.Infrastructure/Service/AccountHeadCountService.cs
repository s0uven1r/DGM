using Dgm.Common.Enums;
using Dgm.Common.Error;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Domain.Entities.Account;
using Resource.Infrastructure.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Infrastructure.Service
{
    public class AccountHeadCountService : IAccountHeadCountService
    {
        public readonly AppDbContext _appDbContext;
        public AccountHeadCountService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<string> GenerateAccountNumber(string type, string alias)
        {
            try
            {
                var entity = await _appDbContext.AccountCountTables.Where(x => x.Type == type).AsTracking().SingleOrDefaultAsync();
                int count = entity == null ? 0 : entity.Count;
                ++count;
                if (entity == null)
                {
                    await _appDbContext.AccountCountTables.AddAsync(new AccountCountTable
                    {
                        Count = count,
                        Type = type,
                    });
                }
                else
                {
                    entity.Count = count;
                    await _appDbContext.SaveChangesAsync();
                }
                return $"{alias}_{count:D9}";
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _appDbContext.SaveChangesAsync();

                return await GenerateAccountNumber(type, alias);
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new AppException("Something went wrong in generating Account Number");
            }
        }
    }
}
