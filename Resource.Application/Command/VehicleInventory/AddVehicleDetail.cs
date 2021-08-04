using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.VehicleInventory.Request;
using Resource.Domain.Entities.VehicleInventory;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.VehicleInventory
{
    public class AddVehicleDetail
    {
        public class AddVehicleDetailCommand : VehicleDetailCreateViewModel, IRequest
        {

        }

        public class AddVehicleDetailCommandValidator : AbstractValidator<AddVehicleDetailCommand>
        {
            public AddVehicleDetailCommandValidator()
            {
                RuleFor(x => x.RegistrationNumber).NotEmpty();
                RuleFor(x => x.EngineNumber).NotEmpty();
                RuleFor(x => x.ChasisNumber).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AddVehicleDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddVehicleDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var checkExisting = _context.VehicleDetails.Where(q => q.RegistrationNumber == request.RegistrationNumber && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Vehicle Detail with same Registration Number already exists!");


                    VehicleDetail vehicle = new()
                    {
                        RegistrationNumber = request.RegistrationNumber,
                        EngineNumber = request.EngineNumber,
                        ChasisNumber = request.ChasisNumber,
                        Model = request.Model,
                        SubModel = request.SubModel,
                        Capacity = request.Capacity,
                        ManufacturedYear = request.ManufacturedYear,
                        Manufacturer = request.Manufacturer,
                        RegisterDateEN = request.RegisterDateEN,
                        RegisterDateNP = request.RegisterDateNP,
                    };

                    await _context.VehicleDetails.AddAsync(vehicle, cancellationToken);
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
