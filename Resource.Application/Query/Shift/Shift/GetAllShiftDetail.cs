using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Shift.Shift.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.Shift.Shift
{
    public class GetAllShiftDetail
    {
        public class GetAllShiftDetailQuery : IRequest<List<ShiftDetailResponseViewModel>>
        {

        }

        public class Handler : IRequestHandler<GetAllShiftDetailQuery, List<ShiftDetailResponseViewModel>>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<ShiftDetailResponseViewModel>> Handle(GetAllShiftDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _context.Shifts.Where(x => !x.IsDeleted).Select(x => new ShiftDetailResponseViewModel
                    {
                        Id = x.Id,
                        Duration = x.Duration,
                        IsActive = x.IsActive,
                        Name = x.Name,
                        StartTime = x.StartTime.ToString("HH:mm:ss"),
                        EndTime = x.EndTime.ToString("HH:mm:ss"),
                        ShiftFrequencyId = x.ShiftFrequencyId
                    }).ToListAsync();
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
