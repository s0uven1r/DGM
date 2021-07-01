using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Models.VehicleInventory.Request;
using Resource.Application.Service.Abstract;
using Resource.Domain.Entities.VehicleInventory;
using Resource.Domain.Persistence;
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
                RuleFor(x => x.VehicleName).NotEmpty();
                RuleFor(x => x.Model).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AddVehicleDetailCommand, Unit>
        {
            private readonly AppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(AppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(AddVehicleDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var checkExisting = _context.VehicleDetails.Where(q => q.VehicleName.ToLower() == request.VehicleName.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Vehicle Detail already exists!");

                    string userId = _userAccessor.GetCurrentUserId();

                    VehicleDetail vehicle = new()
                    {
                        VehicleName = request.VehicleName,
                        Model = request.Model,
                        SubModel = request.SubModel,
                        CreatedBy = userId,
                        Price = request.Price,
                    };

                    await _context.VehicleDetails.AddAsync(vehicle);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Unit.Value;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
