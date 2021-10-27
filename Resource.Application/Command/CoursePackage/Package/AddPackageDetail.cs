using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.Package.Request;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.CoursePackage.Package
{
    public class AddPackageDetail
    {
        public class AddPackageDetailCommand : PackageCreateViewModel, IRequest
        {

        }

        public class AddPackageDetailCommandValidator : AbstractValidator<AddPackageDetailCommand>
        {
            public AddPackageDetailCommandValidator()
            {
                RuleFor(x => x.PackageName).NotEmpty();
                RuleFor(x => x.CourseId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AddPackageDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(IAppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(AddPackageDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = _userAccessor.UserId;
                    var checkExisting = _context.Packages.Where(q => q.PackageName.ToLower() == request.PackageName.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Package with same name already exists!");

                    var checkCourseTypeValidity = _context.Courses.Where(q => q.Id == request.CourseId && !q.IsDeleted).FirstOrDefault();
                    if (checkCourseTypeValidity == null) throw new AppException("Invalid course!");

                    var shiftFrequencyValidity = _context.ShiftFrequencies.Where(q => q.Id == request.ShiftFrequencyId && !q.IsDeleted).FirstOrDefault();
                    if (shiftFrequencyValidity == null) throw new AppException("Invalid Shift Frequency!");

                    Domain.Entities.PackageCourse.Package Packages = new()
                    {
                        PackageName = request.PackageName,
                        CourseId = request.CourseId,
                        ShiftFrequencyId = request.ShiftFrequencyId,
                        Duration = shiftFrequencyValidity.Duration,
                        Price = request.Price,
                        TotalDay = request.TotalDay,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = userId
                    };

                    await _context.Packages.AddAsync(Packages, cancellationToken);
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
