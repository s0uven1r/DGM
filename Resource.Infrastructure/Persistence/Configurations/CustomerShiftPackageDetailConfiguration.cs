using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resource.Domain.Entities.PackageCourse;

namespace Resource.Infrastructure.Persistence.Configurations
{
    public class CustomerPackageDetailConfiguration : IEntityTypeConfiguration<CustomerPackage>
    {
        public void Configure(EntityTypeBuilder<CustomerPackage> builder)
        {
            builder.HasOne(x => x.Package)
            .WithMany(y => y.CustomerPackages)
            .HasForeignKey(a => a.PackageId);
        }
    }
}
