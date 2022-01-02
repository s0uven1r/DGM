using Dgm.Common.Enums;
using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.Promo.Response;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.CoursePackage.Promo
{
    public class GetSinglePromoDetail
    {
        public class GetSinglePromoQuery : IRequest<PromoResponseViewModel>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<GetSinglePromoQuery, PromoResponseViewModel>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<PromoResponseViewModel> Handle(GetSinglePromoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getSingleAccTypes = await _context.PackagePromoOffers.Where(q => !q.IsDeleted && q.Id == request.Id)
                                        .Select(x => new PromoResponseViewModel
                                        {
                                            Id = x.Id,
                                            Discount = x.Discount,
                                            EndDate = x.EndDate.ToString("dd/MM/yyyy"),
                                            EndDateNp = x.EndDateNp,
                                            HasDiscountPercent = x.HasDiscountPercent,
                                            PromoCode = x.PromoCode,
                                            StartDate = x.StartDate.ToString("dd/MM/yyyy"),
                                            StartDateNp = x.StartDateNp
                                        }).SingleOrDefaultAsync(cancellationToken: cancellationToken);
                    return getSingleAccTypes;
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }

    public class GetSinglePromoDetailByPromoCode
    {
        public class GetSinglePromoQuery : IRequest<PromoResponseViewModel>
        {
            public string PromoCode { get; set; }
        }

        public class Handler : IRequestHandler<GetSinglePromoQuery, PromoResponseViewModel>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<PromoResponseViewModel> Handle(GetSinglePromoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getSingleAccTypes = await _context.PackagePromoOffers.Where(q => !q.IsDeleted && q.PromoCode == request.PromoCode && q.StartDate <= DateTime.Now && q.EndDate >= DateTime.Now)
                                        .Select(x => new PromoResponseViewModel
                                        {
                                            Id = x.Id,
                                            Discount = x.Discount,
                                            EndDate = x.EndDate.ToString("dd/MM/yyyy"),
                                            EndDateNp = x.EndDateNp,
                                            HasDiscountPercent = x.HasDiscountPercent,
                                            PromoCode = x.PromoCode,
                                            StartDate = x.StartDate.ToString("dd/MM/yyyy"),
                                            StartDateNp = x.StartDateNp
                                        }).SingleOrDefaultAsync(cancellationToken: cancellationToken);
                    if (getSingleAccTypes == null) throw new Exception("Not valid promocode");
                    return getSingleAccTypes;
                }
                catch(Exception ex)
                {
                    throw new AppException(ex.Message);
                }
            }
        }
    }
}
