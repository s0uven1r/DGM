using System.Collections.Generic;
using System.Web.Mvc;

namespace Dgm.Common.Constants.KYC
{
    public static class BloodGroupConstant
    {
        public const string Apositive = "A+";
        public const string Anegative = "A-";
        public const string Bpositive = "B+";
        public const string Bnegative = "B-";
        public const string Opositive = "O+";
        public const string Onegative = "O-";
        public const string ABpositive = "AB+";
        public const string ABnegative = "AB-";

        public static List<SelectListItem> TypeOfBloodGroup = new List<SelectListItem> {
            new SelectListItem{ Text = Apositive, Value = Apositive },
            new SelectListItem{ Text = Anegative, Value = Anegative},
            new SelectListItem{ Text = Bpositive, Value = Bpositive},
            new SelectListItem{ Text = Bnegative, Value = Bnegative},
            new SelectListItem{ Text = Opositive, Value = Opositive},
            new SelectListItem{ Text = Onegative, Value = Onegative},
            new SelectListItem{ Text = ABpositive, Value = ABpositive},
            new SelectListItem{ Text = ABnegative, Value = ABnegative},
        };
    }
}
