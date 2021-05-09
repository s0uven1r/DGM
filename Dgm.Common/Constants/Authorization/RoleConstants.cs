using Dgm.Common.Models.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace Dgm.Common.Constants.Authorization
{
    public static class RoleConstants
    {
        public const string SuperAdmin = "Super Admin";
        private static List<RoleSeedViewModel> _role;

        static RoleConstants()
        {
            _role = new List<RoleSeedViewModel>()
            {
                new RoleSeedViewModel{Id = "d0e2d200-79e8-4c5e-b624-bfe2f5104fcd", Title = SuperAdmin},
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
