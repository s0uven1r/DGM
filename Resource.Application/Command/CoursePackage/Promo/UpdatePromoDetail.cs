using Dgm.Common.Enums;
using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.Promo.Request;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.CoursePackage.Promo
{
    public class UpdatePromoDetail
    {
        public class UpdatePromoDetailCommand : PromoUpdateViewModel, IRequest
        {

        }

        public class AddVehicleDetailCommandValidator : AbstractValidator<UpdatePromoDetailCommand>
        {
            public AddVehicleDetailCommandValidator()
            {
                RuleFor(x => x.PromoCode).NotEmpty();
                RuleFor(x => x.PackageId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdatePromoDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(IAppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(UpdatePromoDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = _userAccessor.UserId;
                    var existing = _context.PackagePromoOffers.Where(q => q.Id == request.Id && !q.IsDeleted).SingleOrDefault();
                    if (existing == null) throw new AppException("Invalid! promo details not found!");

                    var checkExisting = _context.PackagePromoOffers.Where(q => q.Id != request.Id && q.PromoCode.ToLower() == request.PromoCode.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("Promo code with same name already exists!");

                    existing.PromoCode = request.PromoCode;
                    existing.StartDate = DateTime.ParseExact(request.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
                    existing.StartDateNp = request.StartDateNp;
                    existing.EndDate = DateTime.ParseExact(request.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
                    existing.EndDateNp = request.EndDateNp;
                    existing.Discount = request.Discount;
                    existing.PackageId = request.PackageId;
                    existing.HasDiscountPercent = request.HasDiscountPercent;
                    existing.UpdatedBy = userId;
                    existing.UpdatedDate = DateTime.UtcNow;

                    _context.PackagePromoOffers.Update(existing);
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
