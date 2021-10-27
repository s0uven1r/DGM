using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Shift.Shift.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.Shift.Shift
{
    public class GetSingleShiftDetail
    {
        public class GetSingleShiftDetailQuery : IRequest<ShiftDetailResponseViewModel>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<GetSingleShiftDetailQuery, ShiftDetailResponseViewModel>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<ShiftDetailResponseViewModel> Handle(GetSingleShiftDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _context.Shifts.Where(x => !x.IsDeleted && x.Id == request.Id).Select(x => new ShiftDetailResponseViewModel
                    {
                        Id = x.Id,
                        Duration = x.Duration,
                        IsActive = x.IsActive,
                        Name = x.Name,
                        StartTime = x.StartTime,
                        EndTime = x.EndTime,
                        ShiftFrequencyId = x.ShiftFrequencyId
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
