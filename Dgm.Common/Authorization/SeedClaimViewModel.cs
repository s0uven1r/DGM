using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dgm.Common.Authorization
{
    public class SeedClaimViewModel
    {
        public string Id { get; set; }
        public string ClaimValue { get; set; }
        public string MenuId { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Class { get; set; }
        public string FaClass { get; set; }
        public string RouteUrl { get; set; }
        public string ParentId { get; set; }
        public int Rank { get; set; }
    }
}
