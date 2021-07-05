using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Models.VehicleInventory.Request;
using Resource.Application.Service.Abstract;
using Resource.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                RuleFor(x => x.VehicleId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateVehicleMaintenanceDetailCommand, Unit>
        {
            private readonly AppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(AppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(UpdateVehicleMaintenanceDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var existing = _context.VehicleMaintenaceDetails.Where(q => q.Id == request.Id && !q.IsDeleted).SingleOrDefault();
                    if (existing == null) throw new AppException("Invalid! Vehicle Detail not found!");

                    string userId = _userAccessor.GetCurrentUserId();

                    existing.VehicleId = request.VehicleId;
                    existing.TypeId = request.TypeId;
                    existing.Remark = request.Remark;
                   
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
