using System.Collections.Generic;
using System.Web.Mvc;

namespace Dgm.Common.Constants.KYC
{
    public static class GenderConstant
    {
        public const string Male = "Male";
        public const string Female = "Female";

        public static List<SelectListItem> TypeOfGender = new List<SelectListItem> {
            new SelectListItem{ Text = Male, Value = Male },
            new SelectListItem{ Text = Female, Value = Female},
        };
    }
}
