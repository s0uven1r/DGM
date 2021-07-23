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
    public class UpdateAccountTypeDetail
    {
        public class UpdateAccountTypeDetailCommand : AccountTypeUpdateViewModel, IRequest
        {

        }

        public class AddVehicleDetailCommandValidator : AbstractValidator<UpdateAccountTypeDetailCommand>
        {
            public AddVehicleDetailCommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateAccountTypeDetailCommand, Unit>
        {
            private readonly AppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(AppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(UpdateAccountTypeDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var existing = _context.AccountTypes.Where(q => q.Id == request.Id && !q.IsDeleted).SingleOrDefault();
                    if (existing == null) throw new AppException("Invalid! Account Type details not found!");

                    var checkExisting = _context.AccountTypes.Where(q => q.Id != request.Id && q.Title.ToLower() == request.Title.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Account Type with same name already exists!");
                    if (!Enum.IsDefined(typeof(AccountTypeEnum), request.Type)) throw new AppException("Invalid Account Type!");
                    string userId = _userAccessor.GetCurrentUserId();

                    existing.Title = request.Title;
                    existing.Type = request.Type;
                    existing.UpdatedBy = userId;
                    existing.UpdatedDate = DateTime.UtcNow;

                    _context.AccountTypes.Update(existing);
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
