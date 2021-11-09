using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Shift.IndividualShift.Request;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.Shift.IndividualShift
{
    public class AddIndividualShiftDetail
    {
        public class AddIndividualShiftDetailCommand : IndividualShiftCreateViewModel, IRequest
        {

        }

        public class AddIndividualShiftDetailCommandValidator : AbstractValidator<AddIndividualShiftDetailCommand>
        {
            public AddIndividualShiftDetailCommandValidator()
            {
            }
        }

        public class Handler : IRequestHandler<AddIndividualShiftDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(AddIndividualShiftDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {

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
