using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Models.Account.AccountHead.Response;
using Resource.Domain.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.Account.AccountHead
{
    public class GetAllAccountHeadDetail
    {

        public class GetAllAccountHeadQuery : IRequest<List<AccountHeadResponseViewModel>>
        {

        }

        public class Handler : IRequestHandler<GetAllAccountHeadQuery, List<AccountHeadResponseViewModel>>
        {
            private readonly AppDbContext _context;
            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<List<AccountHeadResponseViewModel>> Handle(GetAllAccountHeadQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getAllAccHeads = await _context.AccountHeads.Include(x => x.AccountType).Where(q => !q.IsDeleted)
                                        .Select(x => new AccountHeadResponseViewModel
                                        {
                                            Id = x.Id,
                                            AccountTypeId = x.AccountTypeId,
                                            AccountTypeTitle = x.AccountType.Title,
                                            Title = x.Title
                                        }).ToListAsync(cancellationToken: cancellationToken);

                    return getAllAccHeads;
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
