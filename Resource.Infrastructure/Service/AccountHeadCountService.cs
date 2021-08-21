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
        public async Task<string> GenerateAccountHeadNumber(string type, int enumVal)
        {
            try
            {
                var entity = await _appDbContext.AccountHeadCountTables.Where(x => x.Type == type).AsTracking().SingleOrDefaultAsync();
                int count = entity == null ? 0 : entity.Count;
                ++count;
                if (entity == null)
                {
                    await _appDbContext.AccountHeadCountTables.AddAsync(new AccountHeadCountTable
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
                var alias = AccountTypeEnumConversion.GetDescriptionByValue(enumVal);
                if (string.IsNullOrEmpty(alias)) throw new AppException("Cannot get alias for Account Number");
                return $"{alias}{count:D9}";
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _appDbContext.SaveChangesAsync();

                return await GenerateAccountHeadNumber(type, enumVal);
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
