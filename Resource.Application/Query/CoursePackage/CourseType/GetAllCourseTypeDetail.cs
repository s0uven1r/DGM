using Dgm.Common.Enums;
using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.CourseType.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.CoursePackage.CourseType
{
    public class GetAllCourseTypeDetail
    {
        public class GetAllCourseTypeQuery : IRequest<List<CourseTypeResponseViewModel>>
        {

        }

        public class Handler : IRequestHandler<GetAllCourseTypeQuery, List<CourseTypeResponseViewModel>>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<CourseTypeResponseViewModel>> Handle(GetAllCourseTypeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getAllAccTypes = await _context.CourseTypes.Where(q => !q.IsDeleted)
                                        .Select(x => new CourseTypeResponseViewModel
                                        {
                                            Id = x.Id,
                                            Title = x.Title
                                        }).ToListAsync(cancellationToken: cancellationToken);

                    return getAllAccTypes.ToList();
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
