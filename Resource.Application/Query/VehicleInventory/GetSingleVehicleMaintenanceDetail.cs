using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Models.VehicleInventory.Response;
using Resource.Domain.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.VehicleInventory
{
    public class GetSingleVehicleMaintenanceDetail
    {
        public class GetSingleVehicleDetailQuery : IRequest<VehicleMaintenanceDetailResponseViewModel>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<GetSingleVehicleDetailQuery, VehicleMaintenanceDetailResponseViewModel>
        {
            private readonly AppDbContext _context;
            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<VehicleMaintenanceDetailResponseViewModel> Handle(GetSingleVehicleDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getVehicle = await _context.VehicleMaintenaceDetails.Include(x => x.Vehicle).Where(q => !q.IsDeleted && q.Id == request.Id)
                                        .Select(x => new VehicleMaintenanceDetailResponseViewModel
                                        {

                                            RegistrationNumber = x.Vehicle.RegistrationNumber,
                                            Type = x.TypeId,
                                            Remark = x.Remark,
                                            Id = x.Id

                                        }).SingleOrDefaultAsync();
                    if (getVehicle == null) throw new AppException("Invalid! Vehicle Detail not found!");
                    return getVehicle;
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
