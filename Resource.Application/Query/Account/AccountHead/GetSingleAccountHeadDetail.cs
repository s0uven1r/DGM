using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Models.Account.AccountHead.Response;
using Resource.Domain.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.Account.AccountHead
{
    public class GetSingleAccountHeadDetail
    {
        public class GetSingleAccountHeadQuery : IRequest<AccountHeadResponseViewModel>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<GetSingleAccountHeadQuery, AccountHeadResponseViewModel>
        {
            private readonly AppDbContext _context;
            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<AccountHeadResponseViewModel> Handle(GetSingleAccountHeadQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getSingleAccHeads = await _context.AccountHeads.Include(x => x.AccountType).Where(q => !q.IsDeleted && q.Id == request.Id)
                                        .Select(x => new AccountHeadResponseViewModel
                                        {
                                            Id = x.Id,
                                            AccountTypeId = x.AccountTypeId,
                                            AccountTypeTitle = x.AccountType.Title,
                                            Title = x.Title
                                        }).SingleOrDefaultAsync(cancellationToken: cancellationToken);

                    return getSingleAccHeads;
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
