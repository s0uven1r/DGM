using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dgm.Common.Authorization.Claim.Resource
{
    public class PackageCourseClaimConstant
    {
        public const string ViewConfiguration = "Claim.Configuration.Read";

       
        // course type
        public const string ViewCourseType = "Claim.CourseType.Read";
        public const string WriteCourseType = "Claim.CourseType.Write";

        // course
        public const string ViewCourse = "Claim.Course.Read";
        public const string WriteCourse = "Claim.Course.Write";

        // package
        public const string ViewPackage = "Claim.Package.Read";
        public const string WritePackage = "Claim.Package.Write";
        
        // promo
        public const string ViewPromo = "Claim.Promo.Read";
        public const string Writepromo = "Claim.Promo.Write";

        // customer package
        public const string ViewCustomerPackage = "Claim.Customer.Package.Read";
        public const string WriteCustomerPackage = "Claim.Customer.Package.Write";
    }
}
