using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dgm.Common.Authorization.MenuControl.Resource
{
    public class PackageCourseMenuConstant
    {
        public static KeyValuePair<string, string> ConfigurationManagement = new("Configuration", "config");
        public static readonly KeyValuePair<string, string> CourseType = new("Course Type", "coursetype");
        public static readonly KeyValuePair<string, string> Course = new("Course", "course");
        public static readonly KeyValuePair<string, string> Package = new("Package", "package");
        public static readonly KeyValuePair<string, string> Promo = new("Promo Offer", "promo");
        public static readonly KeyValuePair<string, string> CustomerPackage = new("Customer Package", "custpack");

    }
}
