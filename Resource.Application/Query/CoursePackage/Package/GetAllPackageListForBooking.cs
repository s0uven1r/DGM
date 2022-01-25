using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.Package;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.CoursePackage.Package
{
    public class GetAllPackageListForBooking
    {
        public class GetAllPackageListForBookingQuery : IRequest<List<PackageListForBooking>>
        {

        }

        public class Handler : IRequestHandler<GetAllPackageListForBookingQuery, List<PackageListForBooking>>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<PackageListForBooking>> Handle(GetAllPackageListForBookingQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getAll = await (from package in _context.Packages
                                        join course in _context.Courses
                                        on package.CourseId equals course.Id
                                        join frequency in _context.ShiftFrequencies
                                        on package.ShiftFrequencyId equals frequency.Id
                                        orderby package.CreatedDate descending
                                        where !package.IsDeleted && !course.IsDeleted && !frequency.IsDeleted
                                        select new PackageListForBooking
                                        {
                                            PackageId = package.Id,
                                            PackageName = package.PackageName,
                                            CourseId = course.Id,
                                            CourseName = course.CourseName,
                                            Description = package.Description,
                                            Duration = $"{package.TotalDay} days",
                                            Price = package.Price,
                                            Time = $"{frequency.Duration} mins per day"
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
