using Dgm.Common.Enum;
using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Account.AccountType.Request;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.Account.AccountType
{
    public class AddAccountTypeDetail
    {
        public class AddAccountTypeDetailCommand : AccountTypeCreateViewModel, IRequest
        {

        }

        public class AddAccountTypeDetailCommandValidator : AbstractValidator<AddAccountTypeDetailCommand>
        {
            public AddAccountTypeDetailCommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AddAccountTypeDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
            }
            public async Task<Unit> Handle(AddAccountTypeDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var checkExisting = _context.AccountTypes.Where(q => q.Title.ToLower() == request.Title.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Account Type with same name already exists!");

                    if (!Enum.IsDefined(typeof(AccountTypeEnum), request.Type)) throw new AppException("Invalid Account Type!");
                    Domain.Entities.Account.AccountType accTypes = new()
                    {
                        Title = request.Title,
                        Type = request.Type
                    };

                    await _context.AccountTypes.AddAsync(accTypes, cancellationToken);
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
