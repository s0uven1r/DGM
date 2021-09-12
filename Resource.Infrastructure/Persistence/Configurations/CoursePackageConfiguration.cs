using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resource.Domain.Entities.PackageCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Infrastructure.Persistence.Configurations
{
    public class CourseConfiguration
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasOne(x => x.CourseType)
            .WithMany(y => y.Courses)
            .HasForeignKey(a => a.CourseTypeId);
        }
    }
    public class PackageConfiguration
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.HasOne(x => x.Course)
            .WithMany(y => y.Packages)
            .HasForeignKey(a => a.CourseId);
        }
    }
    public class PromoConfiguration
    {
        public void Configure(EntityTypeBuilder<PackagePromoOffer> builder)
        {
            builder.HasOne(x => x.Package)
            .WithMany(y => y.PackagePromoOffers)
            .HasForeignKey(a => a.PackageId);
        }
    }
}
