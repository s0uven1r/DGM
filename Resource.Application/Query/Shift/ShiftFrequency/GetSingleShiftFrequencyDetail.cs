using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Shift.ShiftFrequency.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.Shift.ShiftFrequency
{
    public class GetSingleShiftFrequencyDetail
    {
        public class GetSingleShiftFrequencyDetailQuery : IRequest<ShiftFrequencyDetailResponseViewModel>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<GetSingleShiftFrequencyDetailQuery, ShiftFrequencyDetailResponseViewModel>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<ShiftFrequencyDetailResponseViewModel> Handle(GetSingleShiftFrequencyDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _context.ShiftFrequencies.Where(x => !x.IsDeleted && x.Id == request.Id).Select(x => new ShiftFrequencyDetailResponseViewModel
                    {
                        Id = x.Id,
                        Duration = x.Duration,
                        IsActive = x.IsActive,
                        Name = x.Name
                    }).FirstOrDefaultAsync();
                    return response;
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
