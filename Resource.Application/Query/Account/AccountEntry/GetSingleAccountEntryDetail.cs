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
                RuleFor(x => x.AccountNumber).NotEmpty().NotNull();
                RuleFor(x => x.Type).NotEmpty().NotNull();
                RuleFor(x => x.TransactionDateEN).NotEmpty().NotNull();
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
                    var dateEN = DateTime.ParseExact(request.TransactionDateEN, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
                    var accountEntry = await _context.Transactions.Include(a => a.TransactionDetails)
                        .Where(x => x.Type == request.Type && x.AccountNumber == request.AccountNumber && x.TransactionDate == dateEN)
                        .Select(y => new
                        AccountEntryResponseViewModel
                        {
                            AccountNumber = y.AccountNumber,
                            MarketPrice = y.TotalAmount,
                            DiscountedAmount = y.Discount,
                            NetAmount = y.NetAmount,
                            DueAmount = y.DueAmount,
                            EntryDateEN = y.TransactionDate,
                            EntryDateNP = y.TransactionDateNP,
                            Remarks = y.Remarks,
                            Type = y.Type,
                            JournalEntries = y.TransactionDetails.Select(z => new
                            Models.Account.AccountEntry.Response.JournalEntry
                            {
                                Type = z.Type,
                                AccountNumber = z.AccountNumber,
                                CreditAmount = z.AmountCredit,
                                DebitAmount = z.AmountDebit,
                                EntryDateEN = z.TransactionDate,
                                EntryDateNP = z.TransactionDateNP,
                                Remarks = z.Remarks,
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
