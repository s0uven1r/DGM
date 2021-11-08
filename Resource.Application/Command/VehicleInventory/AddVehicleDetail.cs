using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.VehicleInventory.Request;
using Resource.Domain.Entities.VehicleInventory;
using System;
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
                RuleFor(x => x.RegistrationNumber).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
                RuleFor(x => x.EngineNumber).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
                RuleFor(x => x.ChasisNumber).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AddVehicleDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IAccountHeadCountService _accountHeadCountService;
            private readonly IUserAccessor _userAccessor;
            public Handler(IAppDbContext context, IAccountHeadCountService accountHeadCountService, IUserAccessor userAccessor)
            {
                _context = context;
                _accountHeadCountService = accountHeadCountService;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(AddVehicleDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = _userAccessor.UserId;
                    var checkExisting = _context.VehicleDetails.Where(q => q.RegistrationNumber == request.RegistrationNumber && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Vehicle Detail with same Registration Number already exists!");
                    
                    var accNumber = await _accountHeadCountService.GenerateAccountNumber("Vehicle", "V");

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
                        AccountNumber = accNumber,
                        CreatedBy = userId,
                        CreatedDate = DateTime.UtcNow
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
