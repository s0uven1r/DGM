using Dgm.Common.Enum;
using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Models.Account.AccountType.Request;
using Resource.Application.Service.Abstract;
using Resource.Domain.Persistence;
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
            private readonly AppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(AppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(AddAccountTypeDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var checkExisting = _context.AccountTypes.Where(q => q.Title.ToLower() == request.Title.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Account Type with same name already exists!");

                    string userId = _userAccessor.GetCurrentUserId();

                    if(!Enum.IsDefined(typeof(AccountTypeEnum), request.Type)) throw new AppException("Invalid Account Type!");
                    Domain.Entities.Account.AccountType accTypes = new()
                    {
                        Title = request.Title,
                        CreatedBy = userId,
                        Type = request.Type,
                    };

                    await _context.AccountTypes.AddAsync(accTypes,cancellationToken);
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
