using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Models.VehicleInventory.Request;
using Resource.Application.Service.Abstract;
using Resource.Domain.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.VehicleInventory
{
    public class UpdateVehicleDetail
    {
        public class UpdateVehicleDetailCommand : VehicleDetailUpdateViewModel, IRequest
        {

        }

        public class AddVehicleDetailCommandValidator : AbstractValidator<UpdateVehicleDetailCommand>
        {
            public AddVehicleDetailCommandValidator()
            {
                RuleFor(x => x.RegistrationNumber).NotEmpty();
                RuleFor(x => x.EngineNumber).NotEmpty();
                RuleFor(x => x.ChasisNumber).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateVehicleDetailCommand, Unit>
        {
            private readonly AppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(AppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(UpdateVehicleDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var existing = _context.VehicleDetails.Where(q => q.Id == request.Id && !q.IsDeleted).SingleOrDefault();
                    if (existing == null) throw new AppException("Invalid! Vehicle Detail not found!");

                    var checkExisting = _context.VehicleDetails.Where(q => q.Id != request.Id && q.RegistrationNumber == request.RegistrationNumber && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Vehicle Detail with same Registration Number already exists!");

                    string userId = _userAccessor.GetCurrentUserId();

                    existing.RegistrationNumber = request.RegistrationNumber;
                    existing.EngineNumber = request.EngineNumber;
                    existing.ChasisNumber = request.ChasisNumber;
                    existing.Model = request.Model;
                    existing.SubModel = request.SubModel;
                    existing.UpdatedBy = userId;
                    existing.UpdatedDate = DateTime.UtcNow;
                    existing.Capacity = request.Capacity;
                    existing.ManufacturedYear = request.ManufacturedYear;
                    existing.Manufacturer = request.Manufacturer;
                    existing.RegisterDateNP = request.RegisterDateNP;
                    existing.RegisterDateEN = request.RegisterDateEN;
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
