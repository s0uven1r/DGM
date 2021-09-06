using Dgm.Common.Enums;
using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Account.AccountType.Response;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.Account.AccountType
{
    public class GetSingleAccountTypeDetail
    {
        public class GetSingleAccountTypeQuery : IRequest<AccountTypeResponseViewModel>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<GetSingleAccountTypeQuery, AccountTypeResponseViewModel>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<AccountTypeResponseViewModel> Handle(GetSingleAccountTypeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getSingleAccTypes = await _context.AccountTypes.Where(q => !q.IsDeleted && q.Id == request.Id)
                                        .Select(x => new AccountTypeResponseViewModel
                                        {
                                            Id = x.Id,
                                            Title = x.Title,
                                            Type = x.Type
                                        }).SingleOrDefaultAsync(cancellationToken: cancellationToken);
                    if(getSingleAccTypes != null) getSingleAccTypes.TypeName = Enum.GetName(typeof(AccountTypeEnum), getSingleAccTypes.Type);
                    return getSingleAccTypes;
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
