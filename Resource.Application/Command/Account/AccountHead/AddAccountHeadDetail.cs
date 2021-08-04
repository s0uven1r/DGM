using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Account.AccountHead.Request;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.Account.AccountHead
{
    public class AddAccountHeadDetail
    {
        public class AddAccountHeadDetailCommand : AccountHeadCreateViewModel, IRequest
        {

        }

        public class AddAccountHeadDetailCommandValidator : AbstractValidator<AddAccountHeadDetailCommand>
        {
            public AddAccountHeadDetailCommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.AccountTypeId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AddAccountHeadDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(AddAccountHeadDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var checkExisting = _context.AccountHeads.Where(q => q.Title.ToLower() == request.Title.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Account Head with same name already exists!");

                    var checkAccTypeValidity = _context.AccountTypes.Where(q => q.Id == request.AccountTypeId && !q.IsDeleted).FirstOrDefault();
                    if (checkAccTypeValidity == null) throw new AppException("Invalid account type!");


                    Domain.Entities.Account.AccountHead accHeads = new()
                    {
                        Title = request.Title,
                        AccountTypeId = request.AccountTypeId
                    };

                    await _context.AccountHeads.AddAsync(accHeads, cancellationToken);
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
