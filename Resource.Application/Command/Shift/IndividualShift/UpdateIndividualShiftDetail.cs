using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Shift.IndividualShift.Request;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.Shift.IndividualShift
{
    public class UpdateIndividualShiftDetail
    {
        public class UpdateIndividualShiftDetailCommand : UpdateIndividualShiftViewModel, IRequest
        {

        }

        public class UpdateIndividualShiftDetailCommandValidator : AbstractValidator<UpdateIndividualShiftDetailCommand>
        {
            public UpdateIndividualShiftDetailCommandValidator()
            {
                RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Id is required!");
                RuleFor(x => x.ShiftId).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("ShiftId is required!");
                RuleFor(x => x.VehicleId).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("VehicleId is required!");
            }
        }

        public class Handler : IRequestHandler<UpdateIndividualShiftDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(UpdateIndividualShiftDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var individualShift = await _context.IndividualShifts.Where(x => !x.IsDeleted && x.Id == request.Id).FirstOrDefaultAsync();
                    if (individualShift == null) throw new AppException("Individual Shift Record not found!");

                    var shift = await _context.Shifts.Where(x => !x.IsDeleted && x.Id == request.ShiftId).FirstOrDefaultAsync();
                    if (shift == null) throw new AppException("Invalid ShiftId!");

                    individualShift.ShiftId = request.ShiftId;
                    individualShift.TrainerId = request.TrainerId;
                    individualShift.TrainerDetail = request.TrainerDetail;
                    individualShift.TrainingDate = string.IsNullOrEmpty(request.TrainingDate) ? null
                                                   : DateTime.ParseExact(request.TrainingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
                    individualShift.TrainingDateNp = string.IsNullOrEmpty(request.TrainingDate) ? null : request.TrainingDateNp;
                    individualShift.VehicleId = request.VehicleId;

                    _context.IndividualShifts.Update(individualShift);
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
