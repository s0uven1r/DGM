using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Account.AccountEntry.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.Account.AccountEntry
{
    public class GetAllAccountEntryDetail
    {
        public class GetAllAccountEntryQuery : IRequest<List<AccountEntryListResponseViewModel>>
        {

        }
        public class Handler : IRequestHandler<GetAllAccountEntryQuery, List<AccountEntryListResponseViewModel>>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<AccountEntryListResponseViewModel>> Handle(GetAllAccountEntryQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getAllAccEntry = await _context.Transactions.Where(q => !q.IsDeleted)
                                        .Select(x => new AccountEntryListResponseViewModel
                                        {
                                            Id = x.Id,
                                            AccountNumber = x.AccountNumber,
                                            EntryDateEN = x.TransactionDate.ToString("dd/MM/yyyyy"),
                                            EntryDateNP = x.TransactionDateNP,
                                            Remarks = x.Remarks
                                        }).ToListAsync(cancellationToken: cancellationToken);

                    return getAllAccEntry;
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }

    }
}
