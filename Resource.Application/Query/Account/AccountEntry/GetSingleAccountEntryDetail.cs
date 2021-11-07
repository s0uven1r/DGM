using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Account.AccountEntry.Request;
using Resource.Application.Models.Account.AccountEntry.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.Account.AccountEntry
{
    public class GetSingleAccountEntryDetail
    {
        public class GetSingleAccountEntryQuery : AccountEntryGetSingleViewModel, IRequest<AccountEntryResponseViewModel>
        {
        }

        public class GetSingleAccountEntryQueryValidator : AbstractValidator<GetSingleAccountEntryQuery>
        {
            public GetSingleAccountEntryQueryValidator()
            {
                RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotEmpty().NotNull();
            }
        }

        public class Handler : IRequestHandler<GetSingleAccountEntryQuery, AccountEntryResponseViewModel>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<AccountEntryResponseViewModel> Handle(GetSingleAccountEntryQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var accountEntry = await _context.Transactions.Include(a => a.TransactionDetails)
                        .Where(x => x.Id == request.Id && !x.IsDeleted)
                        .Select(y => new
                        AccountEntryResponseViewModel
                        {
                            Id = y.Id,
                            Type = y.Type,
                            Title = y.Title,
                            AccountNumber = y.AccountNumber,
                            MarketPrice = y.TotalAmount,
                            DiscountAmount = y.Discount,
                            NetAmount = y.NetAmount,
                            DueAmount = y.DueAmount,
                            EntryDateEN = y.TransactionDate.ToString("dd/MM/yyyyy"),
                            EntryDateNP = y.TransactionDateNP,
                            Remarks = y.Remarks,
                            JournalEntries = y.TransactionDetails.Select(z => new
                            Models.Account.AccountEntry.Response.JournalEntry
                            {
                                Id = z.Id,
                                Title = z.Title,
                                Type = z.Type,
                                AccountNumber = z.AccountNumber,
                                CreditAmount = z.AmountCredit,
                                DebitAmount = z.AmountDebit,
                                Remarks = z.Remarks
                            }).ToList()
                        }).FirstOrDefaultAsync(cancellationToken);

                    return accountEntry;
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
