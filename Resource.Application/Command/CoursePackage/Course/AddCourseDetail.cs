using Dgm.Common.Enums;
using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.Course.Request;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.CoursePackage.Course
{
    public class AddCourseDetail
    {
        public class AddCourseDetailCommand : CourseCreateViewModel, IRequest
        {

        }

        public class AddCourseDetailCommandValidator : AbstractValidator<AddCourseDetailCommand>
        {
            public AddCourseDetailCommandValidator()
            {
                RuleFor(x => x.CourseName).NotEmpty();
                RuleFor(x => x.CourseTypeId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AddCourseDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(IAppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(AddCourseDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = _userAccessor.UserId;
                    var checkExisting = _context.Courses.Where(q => q.CourseName.ToLower() == request.CourseName.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Course with same name already exists!");

                    var checkAccTypeValidity = _context.CourseTypes.Where(q => q.Id == request.CourseTypeId && !q.IsDeleted).FirstOrDefault();
                    if (checkAccTypeValidity == null) throw new AppException("Invalid Course type!");
                    Domain.Entities.PackageCourse.Course courses = new()
                    {
                        CourseName = request.CourseName,
                        CourseTypeId = request.CourseTypeId,
                        CourseInfo = request.CourseInfo,
                        IsAdvanceCourse = request.IsAdvanceCourse,
                        RequiredDocuments = request.RequiredDocuments,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = userId
                    };

                    await _context.Courses.AddAsync(courses, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return Unit.Value;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
