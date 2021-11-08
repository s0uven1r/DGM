using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.VehicleInventory.Request;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.VehicleInventory
{
    public class UpdateVehicleMaintenanceDetail
    {
        public class UpdateVehicleMaintenanceDetailCommand : VehicleMaintenanceDetailUpdateViewModel, IRequest
        {

        }

        public class AddVehicleDetailCommandValidator : AbstractValidator<UpdateVehicleMaintenanceDetailCommand>
        {
            public AddVehicleDetailCommandValidator()
            {
                RuleFor(x => x.VehicleId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateVehicleMaintenanceDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(IAppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(UpdateVehicleMaintenanceDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var existing = _context.VehicleMaintenaceDetails.Where(q => q.Id == request.Id && !q.IsDeleted).SingleOrDefault();
                    if (existing == null) throw new AppException("Invalid! Vehicle Detail not found!");
                    var userId = _userAccessor.UserId;

                    existing.VehicleId = request.VehicleId;
                    existing.Remark = request.Remark;
                    existing.RegisterDateNP = request.RegisterDateNP;
                    existing.RegisterDateEN = request.RegisterDateEN;
                    existing.UpdatedBy = userId;
                    existing.UpdatedDate = DateTime.UtcNow;
                   

                    _context.VehicleMaintenaceDetails.Update(existing);
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
