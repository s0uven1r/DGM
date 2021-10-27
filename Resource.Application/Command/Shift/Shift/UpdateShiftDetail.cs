using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Shift.Shift.Request;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.Shift.Shift
{
    public class UpdateShiftDetail
    {
        public class UpdateShiftDetailCommand : ShiftUpdateViewModel, IRequest
        {

        }

        public class UpdateShiftDetailCommandValidator : AbstractValidator<UpdateShiftDetailCommand>
        {
            public UpdateShiftDetailCommandValidator()
            {
                RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotEmpty().NotNull();
                RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Name is required!");
                RuleFor(x => x.ShiftFrequencyId).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Duration is invalid!");
                RuleFor(x => x.StartTime).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Start Time is required!");
            }
        }

        public class Handler : IRequestHandler<UpdateShiftDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(UpdateShiftDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {

                    var shift = await _context.Shifts.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                    if (shift == null) throw new AppException("Invalid! Shift not found!");

                    var shiftFrequency = await _context.ShiftFrequencies.Where(x => x.Id == request.ShiftFrequencyId).FirstOrDefaultAsync();
                    if (shiftFrequency == null) throw new AppException("Invalid Shift Frequency");
                    
                    var startTime = DateTime.Parse(request.StartTime, System.Globalization.CultureInfo.CurrentCulture);
                    var endTime = startTime.AddMinutes(shiftFrequency.Duration);
                    
                    shift.ShiftFrequencyId = request.ShiftFrequencyId;
                    shift.IsActive = request.IsActive;
                    shift.Duration = shiftFrequency.Duration;
                    shift.StartTime = startTime;
                    shift.EndTime = endTime;
                    shift.Name = request.Name;

                    _context.Shifts.Update(shift);
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
