using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.Package.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.CoursePackage.Package
{
    public class GetSinglePackageDetail
    {
        public class GetSinglePackageQuery : IRequest<PackageResponseViewModel>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<GetSinglePackageQuery, PackageResponseViewModel>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<PackageResponseViewModel> Handle(GetSinglePackageQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getSingleAccHeads = await _context.Packages.Include(x => x.Course).Where(q => !q.IsDeleted && q.Id == request.Id)
                                        .Select(x => new PackageResponseViewModel
                                        {
                                            Id = x.Id,
                                            PackageName = x.PackageName,
                                            CourseId = x.CourseId,
                                            Duration = x.Duration,
                                            Price = x.Price,
                                            TotalDay = x.TotalDay,
                                            CourseTitle = x.Course.CourseName ?? ""
                                        }).SingleOrDefaultAsync(cancellationToken: cancellationToken);

                    return getSingleAccHeads;
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
