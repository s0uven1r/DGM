using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Shift.ShiftFrequency.Request;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.Shift.ShiftFrequency
{
    public class AddShiftFrequency
    {
        public class AddShiftFrequencyCommand : ShiftFrequencyCreateViewModel, IRequest
        {

        }

        public class AddShiftFrequencyCommandValidator : AbstractValidator<AddShiftFrequencyCommand>
        {
            public AddShiftFrequencyCommandValidator()
            {
                RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Name is required!");
                RuleFor(x => x.Duration).Cascade(CascadeMode.Stop).Must(x => x > 0).WithMessage("Duration is invalid!");
            }
        }

        public class Handler : IRequestHandler<AddShiftFrequencyCommand, Unit>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(AddShiftFrequencyCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var frequency = await _context.ShiftFrequencies.Where(x => x.Name == request.Name).FirstOrDefaultAsync();
                    if (frequency != null) throw new AppException("Frequency with same name already exists!");

                    Domain.Entities.Shift.ShiftFrequency shiftFrequency = new()
                    {
                        Name = request.Name,
                        IsActive = request.IsActive,
                        Duration = request.Duration
                    };
                    await _context.ShiftFrequencies.AddAsync(shiftFrequency);
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
