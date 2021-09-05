using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.VehicleInventory.Request;
using Resource.Domain.Entities.VehicleInventory;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.VehicleInventory
{
    public class AddVehicleMaintenanceDetail
    {
        public class AddVehicleMaintenanceDetailCommand : VehicleMaintenanceDetailCreateViewModel, IRequest
        {

        }

        public class AddVehicleMaintenanceDetailCommandValidator : AbstractValidator<AddVehicleMaintenanceDetailCommand>
        {
            public AddVehicleMaintenanceDetailCommandValidator()
            {
                RuleFor(x => x.VehicleId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AddVehicleMaintenanceDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(IAppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(AddVehicleMaintenanceDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = _userAccessor.UserId;
                    VehicleMaintenanceDetail vehicle = new()
                    {
                        VehicleId = request.VehicleId,
                        Remark = request.Remark,
                        RegisterDateEN = request.RegisterDateEN,
                        RegisterDateNP = request.RegisterDateNP,
                        CreatedBy = userId
                    };

                    await _context.VehicleMaintenaceDetails.AddAsync(vehicle, cancellationToken);
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
