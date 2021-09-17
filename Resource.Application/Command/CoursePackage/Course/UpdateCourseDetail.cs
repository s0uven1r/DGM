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
    public class UpdatePackageDetail
    {
        public class UpdateCourseDetailCommand : CourseUpdateViewModel, IRequest
        {

        }

        public class AddVehicleDetailCommandValidator : AbstractValidator<UpdateCourseDetailCommand>
        {
            public AddVehicleDetailCommandValidator()
            {
                RuleFor(x => x.CourseName).NotEmpty();
                RuleFor(x => x.CourseTypeId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateCourseDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(IAppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(UpdateCourseDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = _userAccessor.UserId;
                    var existing = _context.Courses.Where(q => q.Id == request.Id && !q.IsDeleted).SingleOrDefault();
                    if (existing == null) throw new AppException("Invalid! Course details not found!");

                    var checkExisting = _context.Courses.Where(q => q.Id != request.Id && q.CourseName.ToLower() == request.CourseName.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Course with same name already exists!");

                    existing.CourseName = request.CourseName;
                    existing.CourseInfo = request.CourseInfo;
                    existing.CourseTypeId = request.CourseTypeId;
                    existing.IsAdvanceCourse = request.IsAdvanceCourse;
                    existing.RequiredDocuments = request.RequiredDocuments;
                    existing.CourseInfo = request.CourseInfo;
                    existing.UpdatedBy = userId;
                    existing.UpdatedDate = DateTime.UtcNow;

                    _context.Courses.Update(existing);
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
