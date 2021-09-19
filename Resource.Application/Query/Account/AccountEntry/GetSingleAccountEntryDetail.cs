using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Account.AccountEntry.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.Account.AccountEntry
{
    public class GetSingleAccountEntryDetail : IRequest<AccountEntryResponseViewModel>
    {
        public class GetSingleAccountEntryQuery : IRequest<AccountEntryResponseViewModel>
        {
            public string Type { get; set; }
            public string AccountNumber { get; set; }
            public DateTime TransactionDateEN { get; set; }
        }

        public class GetSingleAccountEntryQueryValidator : AbstractValidator<GetSingleAccountEntryQuery>
        {
            public GetSingleAccountEntryQueryValidator()
            {
                RuleFor(x => x.AccountNumber).NotEmpty().NotNull();
                RuleFor(x => x.Type).NotEmpty().NotNull();
                RuleFor(x => x.TransactionDateEN).GreaterThan(DateTime.MinValue);
            }

        }

        public class Handler : IRequestHandler<GetSingleAccountEntryQuery, AccountEntryResponseViewModel>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public Task<AccountEntryResponseViewModel> Handle(GetSingleAccountEntryQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
