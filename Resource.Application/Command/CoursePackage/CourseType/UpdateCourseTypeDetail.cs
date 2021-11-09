using Dgm.Common.Enums;
using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.CourseType.Request;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.CoursePackage.CourseType
{
    public class UpdateCourseTypeDetail
    {
        public class UpdateCourseTypeDetailCommand : CourseTypeUpdateViewModel, IRequest
        {

        }

        public class AddVehicleDetailCommandValidator : AbstractValidator<UpdateCourseTypeDetailCommand>
        {
            public AddVehicleDetailCommandValidator()
            {
                RuleFor(x => x.Title).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateCourseTypeDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(IAppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(UpdateCourseTypeDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = _userAccessor.UserId;
                    var existing = _context.CourseTypes.Where(q => q.Id == request.Id && !q.IsDeleted).SingleOrDefault();
                    if (existing == null) throw new AppException("Invalid! CoursePackage Type details not found!");

                    var checkExisting = _context.CourseTypes.Where(q => q.Id != request.Id && q.Title.ToLower() == request.Title.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("CoursePackage Type with same name already exists!");

                    existing.Title = request.Title;
                    existing.UpdatedBy = userId;
                    existing.UpdatedDate = DateTime.UtcNow;

                    _context.CourseTypes.Update(existing);
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
