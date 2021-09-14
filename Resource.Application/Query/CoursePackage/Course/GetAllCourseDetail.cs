using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.Course.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.CoursePackage.Course
{
    public class GetAllCourseDetail
    {

        public class GetAllCourseQuery : IRequest<List<CourseResponseViewModel>>
        {

        }

        public class Handler : IRequestHandler<GetAllCourseQuery, List<CourseResponseViewModel>>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<CourseResponseViewModel>> Handle(GetAllCourseQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getAllAccHeads = await _context.Courses.Include(x => x.CourseType).Where(q => !q.IsDeleted)
                                        .Select(x => new CourseResponseViewModel
                                        {
                                            Id = x.Id,
                                            CourseTypeId = x.CourseTypeId,
                                            CourseInfo = x.CourseInfo,
                                            CourseName = x.CourseName,
                                            RequiredDocuments = x.RequiredDocuments,
                                            IsAdvanceCourse = x.IsAdvanceCourse,
                                            CourseType = x.CourseType.Title
                                        }).ToListAsync(cancellationToken: cancellationToken);

                    return getAllAccHeads;
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
