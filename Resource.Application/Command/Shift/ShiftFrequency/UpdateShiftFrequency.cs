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
    public class UpdateShiftFrequency
    {
        public class UpdateShiftFrequencyCommand : ShiftFrequencyUpdateViewModel, IRequest
        {

        }

        public class UpdateShiftFrequencyValidator : AbstractValidator<UpdateShiftFrequencyCommand>
        {
            public UpdateShiftFrequencyValidator()
            {
                RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotEmpty().NotNull();
                RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotEmpty().NotNull();
                RuleFor(x => x.Duration).Cascade(CascadeMode.Stop).Must(x => x > 0).WithMessage("Duration is invalid!");
            }
        }

        public class Handler : IRequestHandler<UpdateShiftFrequencyCommand, Unit>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(UpdateShiftFrequencyCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var frequency = await _context.ShiftFrequencies.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                    if (frequency == null) throw new AppException("Shift Frequency doesn't exists!");

                    var validateFrequency = await _context.ShiftFrequencies.Where(x => x.Name == request.Name).FirstOrDefaultAsync(cancellationToken);
                    if (validateFrequency != null) throw new AppException("Frequency with same name already exists!");

                    frequency.Name = request.Name;
                    frequency.IsActive = request.IsActive;
                    frequency.Duration = request.Duration;

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
