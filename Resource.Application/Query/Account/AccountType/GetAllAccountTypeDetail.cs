using Dgm.Common.Enum;
using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Account.AccountType.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.Account.AccountType
{
    public class GetAllAccountTypeDetail
    {
        public class GetAllAccountTypeQuery : IRequest<List<AccountTypeResponseViewModel>>
        {

        }

        public class Handler : IRequestHandler<GetAllAccountTypeQuery, List<AccountTypeResponseViewModel>>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<AccountTypeResponseViewModel>> Handle(GetAllAccountTypeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getAllAccTypes = await _context.AccountTypes.Where(q => !q.IsDeleted)
                                        .Select(x => new AccountTypeResponseViewModel
                                        {
                                            Id = x.Id,
                                            Title = x.Title,
                                            Type = x.Type
                                        }).ToListAsync(cancellationToken: cancellationToken);

                    return getAllAccTypes.Select(x => { x.TypeName = Enum.GetName(typeof(AccountTypeEnum), x.Type); return x; }).ToList();
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
