using System.Collections.Generic;
using System.Web.Mvc;

namespace Dgm.Common.Constants.KYC
{
    public static class MaritalStatus
    {
        public const string Married = "Married";
        public const string UnMarried = "UnMarried";

        public static List<SelectListItem> TypeOfMaritalStatus = new List<SelectListItem> {
            new SelectListItem{ Text = Married, Value = Married},
            new SelectListItem{ Text = UnMarried, Value = UnMarried},
        };
    }
}
