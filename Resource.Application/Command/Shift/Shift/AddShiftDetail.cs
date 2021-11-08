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
    public class AddShiftDetail
    {
        public class AddShiftDetailCommand : ShiftCreateViewModel, IRequest
        {

        }

        public class AddShiftDetailCommandValidator : AbstractValidator<AddShiftDetailCommand>
        {
            public AddShiftDetailCommandValidator()
            {
                RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Name is required!");
                RuleFor(x => x.ShiftFrequencyId).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Duration is invalid!");
                RuleFor(x => x.StartTime).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Start Time is required!");
            }
        }

        public class Handler : IRequestHandler<AddShiftDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(AddShiftDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var shiftFrequency = await _context.ShiftFrequencies.Where(x => x.Id == request.ShiftFrequencyId).FirstOrDefaultAsync();
                    if (shiftFrequency == null) throw new AppException("Invalid Shift Frequency");

                    var startTime = DateTime.Parse(request.StartTime, System.Globalization.CultureInfo.CurrentCulture);
                    var endTime = startTime.AddMinutes(shiftFrequency.Duration);
                    Domain.Entities.Shift.Shift newShift = new()
                    {
                        ShiftFrequencyId = request.ShiftFrequencyId,
                        IsActive = request.IsActive,
                        Duration = shiftFrequency.Duration,
                        StartTime = startTime,
                        EndTime = endTime,
                        Name = request.Name,
                    };

                    await _context.Shifts.AddAsync(newShift);
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
