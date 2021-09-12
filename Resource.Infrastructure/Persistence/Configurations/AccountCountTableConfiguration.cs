using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resource.Domain.Entities.Account;

namespace Resource.Infrastructure.Persistence.Configurations
{
    public class AccountCountTableConfiguration : IEntityTypeConfiguration<AccountCountTable>
    {
        public void Configure(EntityTypeBuilder<AccountCountTable> builder)
        {
            builder.Property(p => p.Timestamp)
                    .IsRowVersion();
        }

    }
}
