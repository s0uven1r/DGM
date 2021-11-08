using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resource.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Infrastructure.Persistence.Configurations
{
    public class TransactionDetailConfiguration : IEntityTypeConfiguration<TransactionDetail>
    {
        public void Configure(EntityTypeBuilder<TransactionDetail> builder)
        {
            builder.HasOne(x => x.Transaction)
        .WithMany(y => y.TransactionDetails)
        .HasForeignKey(a => a.TransactionId);
        }
    }
    public class CustomerPaymentDetailConfiguration : IEntityTypeConfiguration<CustomerPayment>
    {
        public void Configure(EntityTypeBuilder<CustomerPayment> builder)
        {
            builder.HasOne(x => x.CustomerPackage)
        .WithMany(y => y.CustomerPayments)
        .HasForeignKey(a => a.CustomerPackageId);
        }
    }
}
