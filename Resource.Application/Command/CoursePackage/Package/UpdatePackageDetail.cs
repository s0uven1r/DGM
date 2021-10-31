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
    public class UpdatePackageDetail
    {
        public class UpdatePackageDetailCommand : PackageUpdateViewModel, IRequest
        {

        }

        public class AddVehicleDetailCommandValidator : AbstractValidator<UpdatePackageDetailCommand>
        {
            public AddVehicleDetailCommandValidator()
            {
                RuleFor(x => x.PackageName).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
                RuleFor(x => x.CourseId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
                RuleFor(x => x.ShiftFrequencyId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdatePackageDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(IAppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(UpdatePackageDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = _userAccessor.UserId;
                    var existing = _context.Packages.Where(q => q.Id == request.Id && !q.IsDeleted).SingleOrDefault();
                    if (existing == null) throw new AppException("Invalid! Package details not found!");

                    var checkExisting = _context.Packages.Where(q => q.Id != request.Id && q.PackageName.ToLower() == request.PackageName.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Package with same name already exists!");


                    var shiftFrequencyValidity = _context.ShiftFrequencies.Where(q => q.Id == request.ShiftFrequencyId && !q.IsDeleted).FirstOrDefault();
                    if (shiftFrequencyValidity == null) throw new AppException("Invalid Shift Frequency!");

                    existing.PackageName = request.PackageName;
                    existing.Price = request.Price;
                    existing.TotalDay = request.TotalDay;
                    existing.ShiftFrequencyId = request.ShiftFrequencyId;
                    existing.Duration = shiftFrequencyValidity.Duration * request.TotalDay;
                    existing.CourseId = request.CourseId;
                    existing.UpdatedBy = userId;
                    existing.UpdatedDate = DateTime.UtcNow;

                    _context.Packages.Update(existing);
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
