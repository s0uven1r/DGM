using Dgm.Common.Enums;
using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.CourseType.Response;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.CoursePackage.CourseType
{
    public class GetSingleCourseTypeDetail
    {
        public class GetSingleCourseTypeQuery : IRequest<CourseTypeResponseViewModel>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<GetSingleCourseTypeQuery, CourseTypeResponseViewModel>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<CourseTypeResponseViewModel> Handle(GetSingleCourseTypeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getSingleAccTypes = await _context.CourseTypes.Where(q => !q.IsDeleted && q.Id == request.Id)
                                        .Select(x => new CourseTypeResponseViewModel
                                        {
                                            Id = x.Id,
                                            Title = x.Title
                                        }).SingleOrDefaultAsync(cancellationToken: cancellationToken);
                    return getSingleAccTypes;
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
