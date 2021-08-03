using FluentValidation;
using MediatR;
using Resource.Application.Models.VehicleInventory.Request;
using Resource.Application.Service.Abstract;
using Resource.Domain.Entities.VehicleInventory;
using Resource.Domain.Persistence;
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
            private readonly AppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(AppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(AddVehicleMaintenanceDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                   
                    string userId = _userAccessor.GetCurrentUserId();

                    VehicleMaintenanceDetail vehicle = new()
                    {
                        TypeId = request.TypeId,
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
