using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Shift.ShiftFrequency.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.Shift.ShiftFrequency
{
    public class GetAllShiftFrequencyDetail
    {
        public class GetAllShiftFrequencyDetailQuery : IRequest<List<ShiftFrequencyDetailResponseViewModel>>
        {

        }

        public class Handler : IRequestHandler<GetAllShiftFrequencyDetailQuery, List<ShiftFrequencyDetailResponseViewModel>>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<ShiftFrequencyDetailResponseViewModel>> Handle(GetAllShiftFrequencyDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _context.ShiftFrequencies.Where(x => !x.IsDeleted).Select(x => new ShiftFrequencyDetailResponseViewModel
                    {
                        Id = x.Id,
                        Duration = x.Duration,
                        IsActive = x.IsActive,
                        Name = x.Name
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
