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
            public Handler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(AddVehicleMaintenanceDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {

                    VehicleMaintenanceDetail vehicle = new()
                    {
                        TypeId = request.TypeId,
                        VehicleId = request.VehicleId,
                        Remark = request.Remark,
                        RegisterDateEN = request.RegisterDateEN,
                        RegisterDateNP = request.RegisterDateNP,
                        CreatedBy = request.UserId
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
