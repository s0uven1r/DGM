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
                    var response = await (from individualShift in _context.IndividualShifts
                                          join shift in _context.Shifts
                                          on individualShift.ShiftId equals shift.Id
                                          join package in _context.Packages
                                          on individualShift.PackageId equals package.Id
                                          join vehicleDetails in _context.VehicleDetails
                                          on individualShift.VehicleId equals vehicleDetails.Id into vehicleTemp
                                          from vehicle in vehicleTemp.DefaultIfEmpty()
                                          where !individualShift.IsDeleted && individualShift.UserAccountNumber == request.UserAccountNumber
                                          orderby individualShift.TrainingDate
                                          select new IndividualShiftDetailResponseViewModel
                                          {
                                              Id = individualShift.Id,
                                              ShiftId = individualShift.ShiftId,
                                              PackageId = individualShift.PackageId,
                                              VehicleId = individualShift.VehicleId,
                                              TrainerId = individualShift.TrainerId,
                                              TrainingDate = individualShift.TrainingDate.HasValue ? individualShift.TrainingDate.Value.ToString("dd/MM/yyyy") : null,
                                              TrainingDateNp = individualShift.TrainingDateNp,
                                              UserAccountNumber = individualShift.UserAccountNumber,
                                              PackageName = package.PackageName,
                                              ShiftName = string.Join(", ", shift.Name, " (" + shift.StartTime.ToString("hh:mm tt") + "-" + shift.EndTime.ToString("hh:mm tt") + ") "),
                                              TrainerDetail = individualShift.TrainerId,
                                              VehicleNumber = vehicle != null ? vehicle.RegistrationNumber : "-",
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
