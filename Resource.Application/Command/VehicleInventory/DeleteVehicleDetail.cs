using Dgm.Common.Error;
using MediatR;
using Resource.Application.Service.Abstract;
using Resource.Domain.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.VehicleInventory
{
    public class DeleteVehicleDetail
    {
        public class DeleteVehicleDetailCommand : IRequest
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<DeleteVehicleDetailCommand, Unit>
        {
            private readonly AppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(AppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(DeleteVehicleDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var existing = _context.VehicleDetails.Where(q => q.Id == request.Id && !q.IsDeleted).SingleOrDefault();
                    if (existing == null) throw new AppException("Invalid! Vehicle Detail not found!");

                    string userId = _userAccessor.GetCurrentUserId();

                    existing.IsDeleted = true;
                    existing.UpdatedBy = userId;
                    existing.UpdatedDate = DateTime.UtcNow;

                    _context.VehicleDetails.Update(existing);
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
