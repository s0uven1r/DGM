using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Models.Account.AccountHead.Request;
using Resource.Application.Service.Abstract;
using Resource.Domain.Persistence;
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
            private readonly AppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(AppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(AddAccountHeadDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var checkExisting = _context.AccountHeads.Where(q => q.Title.ToLower() == request.Title.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Account Head with same name already exists!");

                    var checkAccTypeValidity = _context.AccountTypes.Where(q => q.Id == request.AccountTypeId && !q.IsDeleted).FirstOrDefault();
                    if (checkAccTypeValidity == null) throw new AppException("Invalid account type!");

                    string userId = _userAccessor.GetCurrentUserId();

                    Domain.Entities.Account.AccountHead accHeads = new()
                    {
                        Title = request.Title,
                        CreatedBy = userId,
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
