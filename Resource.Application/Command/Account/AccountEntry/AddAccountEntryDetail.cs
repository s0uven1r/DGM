using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Account.AccountEntry.Request;
using Resource.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.Account.AccountEntry
{
    public class AddAccountEntryDetail
    {
        public class AddAccountEntryDetailCommand : AccountEntryCreateViewModel, IRequest
        {

        }

        public class AddAccountEntryDetailCommandValidator : AbstractValidator<AddAccountEntryDetailCommand>
        {
            public AddAccountEntryDetailCommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Type).NotEmpty();
                RuleFor(x => x.AccountNumber).NotEmpty();
                RuleFor(x => x.EntryDateNP).NotEmpty();
                RuleFor(x => x.EntryDateEN).GreaterThan(DateTime.MinValue);
                RuleFor(x => x.Remarks).NotEmpty();
                RuleFor(x => x.JournalEntries).NotNull();
            }
        }

        public class Handler : IRequestHandler<AddAccountEntryDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddAccountEntryDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var transactionEntity = new Transaction
                    {
                        AccountNumber = request.AccountNumber,
                        TotalAmount = request.MarketPrice,
                        Discount = request.DiscountedAmount,
                        NetAmount = request.NetAmount,
                        DueAmount = request.DueAmount,
                        Remarks = request.Remarks,
                        Type = request.Type,
                        TransactionDate = request.EntryDateEN,
                        TransactionDateNP = request.EntryDateNP
                    };
                    var transactionDetails = request.JournalEntries.Select(x => new TransactionDetail
                    {
                        AccountNumber = x.AccountNumber,
                        AmountCredit = x.CreditAmount,
                        AmountDebit = x.DebitAmount,
                        TransactionDate = x.EntryDateEN,
                        TransactionDateNP = x.EntryDateNP,
                        Remarks = x.Remarks,
                        Type =x.Type,
                        TransactionId = transactionEntity.Id,
                    }).ToList();

                    await _context.Transactions.AddAsync(transactionEntity, cancellationToken);
                    await _context.TransactionDetails.AddRangeAsync(transactionDetails, cancellationToken);
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
