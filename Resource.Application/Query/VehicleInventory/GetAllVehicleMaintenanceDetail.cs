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
    public class GetAllVehicleMaintenanceDetail
    {


        public class GetAllVehicleDetailQuery : IRequest<List<VehicleMaintenanceDetailResponseViewModel>>
        {

        }

        public class Handler : IRequestHandler<GetAllVehicleDetailQuery, List<VehicleMaintenanceDetailResponseViewModel>>
        {
            private readonly AppDbContext _context;
            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<List<VehicleMaintenanceDetailResponseViewModel>> Handle(GetAllVehicleDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getAllVehicle = await _context.VehicleMaintenaceDetails.Include(x => x.Vehicle).Where(q => !q.IsDeleted)
                                        .Select(x => new VehicleMaintenanceDetailResponseViewModel
                                        {
                                            
                                            RegistrationNumber = x.Vehicle.RegistrationNumber,
                                            Type = x.TypeId,
                                            Remark = x.Remark,
                                            Id = x.Id
                                            
                                        }).ToListAsync();

                    return getAllVehicle;
                }
                catch
                {
                    throw new AppException("Something went wrong!");
                }
            }
        }
    }
}
