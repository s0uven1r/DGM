using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Models.Account.AccountHead.Request;
using Resource.Application.Service.Abstract;
using Resource.Domain.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.Account.AccountHead
{
    public class UpdateAccountHeadDetail
    {
        public class UpdateAccountHeadDetailCommand : AccountHeadUpdateViewModel, IRequest
        {

        }

        public class AddVehicleDetailCommandValidator : AbstractValidator<UpdateAccountHeadDetailCommand>
        {
            public AddVehicleDetailCommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.AccountTypeId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateAccountHeadDetailCommand, Unit>
        {
            private readonly AppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(AppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(UpdateAccountHeadDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var existing = _context.AccountHeads.Where(q => q.Id == request.Id && !q.IsDeleted).SingleOrDefault();
                    if (existing == null) throw new AppException("Invalid! Account Head details not found!");

                    var checkExisting = _context.AccountHeads.Where(q => q.Id != request.Id && q.Title.ToLower() == request.Title.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Account Head with same name already exists!");

                    var checkAccTypeValidity = _context.AccountTypes.Where(q => q.Id == request.AccountTypeId && !q.IsDeleted).FirstOrDefault();
                    if (checkAccTypeValidity == null) throw new AppException("Invalid account type!");

                    string userId = _userAccessor.GetCurrentUserId();

                    existing.AccountTypeId = request.AccountTypeId;
                    existing.Title = request.Title;
                    existing.UpdatedBy = userId;
                    existing.UpdatedDate = DateTime.UtcNow;

                    _context.AccountHeads.Update(existing);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Unit.Value;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
