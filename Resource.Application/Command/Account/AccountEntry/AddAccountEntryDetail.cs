using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Account.AccountEntry.Request;
using Resource.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                RuleFor(x => x.Title).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
                RuleFor(x => x.Type).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
                RuleFor(x => x.AccountNumber).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
                RuleFor(x => x.EntryDateNP).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
                RuleFor(x => x.EntryDateEN).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
                RuleFor(x => x.Remarks).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
                RuleFor(x => x.JournalEntries).NotNull();
                RuleFor(x => x.MarketPrice).Cascade(CascadeMode.Stop).GreaterThan(0);
                RuleFor(x => x.NetAmount).Cascade(CascadeMode.Stop).GreaterThan(0);
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
                    var dateEN = DateTime.ParseExact(request.EntryDateEN, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;

                    var accountEntry = await _context.Transactions.Where(x => x.Type == request.Type && x.AccountNumber == request.AccountNumber && x.TransactionDate == dateEN).FirstOrDefaultAsync(cancellationToken);

                    if (accountEntry == null)
                        await AddAccountEntry(request, cancellationToken);
                    else
                        await UpdateAccountEntry(accountEntry, request, cancellationToken);

                    await transaction.CommitAsync(cancellationToken);
                    return Unit.Value;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }

            private async Task AddAccountEntry(AddAccountEntryDetailCommand request, CancellationToken cancellationToken)
            {
                var transactionEntity = new Transaction
                {
                    AccountNumber = request.AccountNumber,
                    TotalAmount = request.MarketPrice,
                    Discount = request.DiscountAmount,
                    NetAmount = request.NetAmount,
                    DueAmount = request.DueAmount,
                    Remarks = request.Remarks,
                    Type = request.Type,
                    TransactionDate = DateTime.ParseExact(request.EntryDateEN, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date,
                    TransactionDateNP = request.EntryDateNP
                };
                var transactionDetails = request.JournalEntries.Select(x => new TransactionDetail
                {
                    AccountNumber = x.AccountNumber,
                    AmountCredit = x.CreditAmount,
                    AmountDebit = x.DebitAmount,
                    TransactionDate = DateTime.ParseExact(x.EntryDateEN, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    TransactionDateNP = x.EntryDateNP,
                    Remarks = x.Remarks,
                    Type = x.Type,
                    TransactionId = transactionEntity.Id,
                }).ToList();

                await _context.Transactions.AddAsync(transactionEntity, cancellationToken);
                await _context.TransactionDetails.AddRangeAsync(transactionDetails, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

            }

            private async Task UpdateAccountEntry(Transaction accountEntry, AddAccountEntryDetailCommand request, CancellationToken cancellationToken)
            {
                accountEntry.AccountNumber = request.AccountNumber;
                accountEntry.TotalAmount = request.MarketPrice;
                accountEntry.Discount = request.DiscountAmount;
                accountEntry.NetAmount = request.NetAmount;
                accountEntry.DueAmount = request.DueAmount;
                accountEntry.Remarks = request.Remarks;
                accountEntry.Type = request.Type;
                accountEntry.TransactionDate = DateTime.ParseExact(request.EntryDateEN, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
                accountEntry.TransactionDateNP = request.EntryDateNP;

                _context.Transactions.Update(accountEntry);

                _context.TransactionDetails.RemoveRange(_context.TransactionDetails.Where(x => x.TransactionId == accountEntry.Id));
                await _context.SaveChangesAsync(cancellationToken);

                var transactionDetails = request.JournalEntries.Select(x => new TransactionDetail
                {
                    AccountNumber = x.AccountNumber,
                    AmountCredit = x.CreditAmount,
                    AmountDebit = x.DebitAmount,
                    TransactionDate = DateTime.ParseExact(x.EntryDateEN, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    TransactionDateNP = x.EntryDateNP,
                    Remarks = x.Remarks,
                    Type = x.Type,
                    TransactionId = accountEntry.Id,
                }).ToList();

                await _context.TransactionDetails.AddRangeAsync(transactionDetails, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
