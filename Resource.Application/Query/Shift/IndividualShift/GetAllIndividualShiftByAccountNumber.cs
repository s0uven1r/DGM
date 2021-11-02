using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Shift.IndividualShift.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.Shift.IndividualShift
{
    public class GetAllIndividualShiftByAccountNumber
    {
        public class GetAllIndividualShiftByAccountNumberQuery : IRequest<List<IndividualShiftDetailResponseViewModel>>
        {
            public string UserAccountNumber { get; set; }
        }

        public class Handler : IRequestHandler<GetAllIndividualShiftByAccountNumberQuery, List<IndividualShiftDetailResponseViewModel>>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<IndividualShiftDetailResponseViewModel>> Handle(GetAllIndividualShiftByAccountNumberQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _context.IndividualShifts.Where(x => !x.IsDeleted && x.UserAccountNumber == request.UserAccountNumber).Select(x => new IndividualShiftDetailResponseViewModel
                    {
                        Id = x.Id,
                        ShiftId = x.ShiftId,
                        VehicleId = x.VehicleId,
                        TrainerId = x.TrainerId,
                        TrainingDate = x.TrainingDate.HasValue ? x.TrainingDate.Value.ToString("dd/MM/yyyy") : null,
                        TrainingDateNp = x.TrainingDateNp,
                        PackageId = x.PackageId,
                        UserAccountNumber = x.UserAccountNumber
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
