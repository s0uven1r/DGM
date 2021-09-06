using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resource.Domain.Entities.Account;

namespace Resource.Infrastructure.Persistence.Configurations
{
    public class AccountHeadCountTableConfiguration : IEntityTypeConfiguration<AccountHeadCountTable>
    {
        public void Configure(EntityTypeBuilder<AccountHeadCountTable> builder)
        {
            builder.Property(p => p.Timestamp)
                    .IsRowVersion();
        }

    }
}
