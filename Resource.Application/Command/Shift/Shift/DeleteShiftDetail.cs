using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.Shift.Shift
{
    public class DeleteShiftDetail
    {
        public class DeleteShiftDetailCommand : IRequest
        {
            public string Id { get; set; }
        }

        public class DeleteShiftFrequencyCommandValidator : AbstractValidator<DeleteShiftDetailCommand>
        {
            public DeleteShiftFrequencyCommandValidator()
            {
                RuleFor(x => x.Id).Cascade(CascadeMode.Continue).NotEmpty().NotNull();
            }
        }

        public class Handler : IRequestHandler<DeleteShiftDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteShiftDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var shift = await _context.Shifts.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                    if (shift == null) throw new AppException("Shift doesn't exists!");

                    shift.IsDeleted = true;

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
