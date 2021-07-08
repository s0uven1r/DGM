using Auth.Infrastructure.Constants;
using Dgm.Common.Models.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace Dgm.Common.Constants.Authorization
{
    public static class RoleConstants
    {
        private static readonly List<RoleSeedViewModel> _role;

        static RoleConstants()
        {
            _role = new List<RoleSeedViewModel>()
            {
                new RoleSeedViewModel{Id = "d0e2d200-79e8-4c5e-b624-bfe2f5104fcd", Title = SystemRoles.SuperAdmin, Rank = 0, IsDefault = true},
                new RoleSeedViewModel{Id = "f0bb0bf0-b85a-4bfd-a527-e02d8198bb29", Title = SystemRoles.Admin, Rank = 1, IsDefault = false},
                new RoleSeedViewModel{Id = "750c390c-62f7-4af9-a5d3-cad0e5cdcda7", Title = SystemRoles.Consumer, Rank = 2, IsDefault = false}
            };

            if (_role.GroupBy(dr => dr.Id).Any(drg => drg.Count() > 1))
            {
                throw new System.Exception("Duplicate primary keys (IDs) for designation and role.");
            }
        }

        public static List<RoleSeedViewModel> GetAll()
        {
            return _role;
        }

        public static RoleSeedViewModel GetByTitle(string title)
        {
            return _role.FirstOrDefault(f => f.Title.ToLower() == title.ToLower());
        }
        public static RoleSeedViewModel GetById(string id)
        {
            return _role.FirstOrDefault(f => f.Id == id);
        }
    }
}
