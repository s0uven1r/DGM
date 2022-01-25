using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.Shift.ShiftFrequency
{
    public class DeleteShiftFrequency
    {
        public class DeleteShiftFrequencyCommand :  IRequest
        {
            public string Id { get; set; }
        }

        public class DeleteShiftFrequencyCommandValidator : AbstractValidator<DeleteShiftFrequencyCommand>
        {
            public DeleteShiftFrequencyCommandValidator()
            {
                RuleFor(x => x.Id).Cascade(CascadeMode.Continue).NotEmpty().NotNull();
            }
        }

        public class Handler : IRequestHandler<DeleteShiftFrequencyCommand, Unit>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteShiftFrequencyCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var frequency = await _context.ShiftFrequencies.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                    if (frequency == null) throw new AppException("Shift Frequency doesn't exists!");
                    var shiftCount = await _context.Shifts.Where(x => x.ShiftFrequencyId == frequency.Id).CountAsync(cancellationToken);
                    if (shiftCount > 0) throw new AppException("Shift Frequency has been used in Frequency!");

                    frequency.IsDeleted = true;

                    _context.ShiftFrequencies.Update(frequency);
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
