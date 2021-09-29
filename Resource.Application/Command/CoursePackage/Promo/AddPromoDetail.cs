using Dgm.Common.Enums;
using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.Promo.Request;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.CoursePackage.Promo
{
    public class AddPromoDetail
    {
        public class AddPromoDetailCommand : PromoCreateViewModel, IRequest
        {

        }

        public class AddPromoDetailCommandValidator : AbstractValidator<AddPromoDetailCommand>
        {
            public AddPromoDetailCommandValidator()
            {
                RuleFor(x => x.PromoCode).NotEmpty();
                RuleFor(x => x.PackageId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AddPromoDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(IAppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(AddPromoDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    string userId = _userAccessor.UserId;
                    var checkExisting = _context.PackagePromoOffers.Where(q => q.PromoCode.ToLower() == request.PromoCode.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Promo code with same name already exists!");

                    Domain.Entities.PackageCourse.PackagePromoOffer Promos = new()
                    {
                        PromoCode = request.PromoCode,
                        PackageId = request.PackageId,
                        HasDiscountPercent = request.HasDiscountPercent,
                        StartDate = request.StartDate,
                        StartDateNp = request.StartDateNp,
                        Discount = request.Discount,
                        EndDate = request.EndDate,
                        CreatedDate = DateTime.UtcNow,
                        EndDateNp = request.EndDateNp,
                        CreatedBy = userId
                    };

                    await _context.PackagePromoOffers.AddAsync(Promos, cancellationToken);
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
