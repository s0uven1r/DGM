using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.Package.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.CoursePackage.Package
{
    public class GetAllPackageDetail
    {

        public class GetAllPackageQuery : IRequest<List<PackageResponseViewModel>>
        {

        }

        public class Handler : IRequestHandler<GetAllPackageQuery, List<PackageResponseViewModel>>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<PackageResponseViewModel>> Handle(GetAllPackageQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getAll = await _context.Packages.Include(x => x.Course).Where(q => !q.IsDeleted)
                                        .Select(x => new PackageResponseViewModel
                                        {
                                            Id = x.Id,
                                            PackageName = x.PackageName,
                                            CourseId = x.CourseId,
                                            Duration = x.Duration,
                                            Price = x.Price,
                                            TotalDay = x.TotalDay,
                                            CourseTitle = x.Course.CourseName ?? "",
                                            ShiftFrequencyId = x.ShiftFrequencyId
                                        }).ToListAsync(cancellationToken: cancellationToken);

                    return getAll;
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
