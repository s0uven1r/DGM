using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resource.Domain.Entities.Account;

namespace Resource.Infrastructure.Persistence.Configurations
{
    public class AccountHeadConfiguration : IEntityTypeConfiguration<AccountHead>
    {
        public void Configure(EntityTypeBuilder<AccountHead> builder)
        {
            builder.HasOne(x => x.AccountType)
                     .WithMany(y => y.AccountHeads)
                     .HasForeignKey(a => a.AccountTypeId);

        }
    }
}
