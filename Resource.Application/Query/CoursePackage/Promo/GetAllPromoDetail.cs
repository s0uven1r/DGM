using Dgm.Common.Enums;
using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.Promo.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.CoursePackage.Promo
{
    public class GetAllPromoDetail
    {
        public class GetAllPromoQuery : IRequest<List<PromoResponseViewModel>>
        {

        }

        public class Handler : IRequestHandler<GetAllPromoQuery, List<PromoResponseViewModel>>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<PromoResponseViewModel>> Handle(GetAllPromoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getAllAccTypes = await _context.PackagePromoOffers.Where(q => !q.IsDeleted)
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
                                        }).ToListAsync(cancellationToken: cancellationToken);

                    return getAllAccTypes.ToList();
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
