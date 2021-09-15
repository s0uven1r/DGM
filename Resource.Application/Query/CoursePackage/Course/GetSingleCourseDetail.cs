using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.Course.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.CoursePackage.Course
{
    public class GetSingleCourseDetail
    {
        public class GetSingleCourseQuery : IRequest<CourseResponseViewModel>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<GetSingleCourseQuery, CourseResponseViewModel>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<CourseResponseViewModel> Handle(GetSingleCourseQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getSingleAccHeads = await _context.Courses.Include(x => x.CourseType).Where(q => !q.IsDeleted && q.Id == request.Id)
                                        .Select(x => new CourseResponseViewModel
                                        {
                                            Id = x.Id,
                                            CourseTypeId = x.CourseTypeId,
                                            CourseInfo = x.CourseInfo,
                                            CourseName = x.CourseName,
                                            RequiredDocuments = x.RequiredDocuments,
                                            IsAdvanceCourse = x.IsAdvanceCourse,
                                            CourseType = x.CourseType.Title
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
