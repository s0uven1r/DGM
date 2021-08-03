using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.VehicleInventory.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.VehicleInventory
{
    public class GetAllVehicleDetail
    {
        public class GetAllVehicleDetailQuery : IRequest<List<VehicleDetailResponseViewModel>>
        {

        }

        public class Handler : IRequestHandler<GetAllVehicleDetailQuery, List<VehicleDetailResponseViewModel>>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<VehicleDetailResponseViewModel>> Handle(GetAllVehicleDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getAllVehicle = await _context.VehicleDetails.Where(q => !q.IsDeleted)
                                        .Select(x => new VehicleDetailResponseViewModel
                                        {
                                            Id = x.Id,
                                            RegistrationNumber = x.RegistrationNumber,
                                            EngineNumber = x.EngineNumber,
                                            ChasisNumber = x.ChasisNumber,
                                            Capacity = x.Capacity,
                                            ManufacturedYear = x.ManufacturedYear,
                                            CreatedBy = x.CreatedBy,
                                            CreatedDate = x.CreatedDate,
                                            Model = x.Model,
                                            SubModel = x.SubModel,
                                            UpdatedBy = x.UpdatedBy,
                                            UpdatedDate = x.UpdatedDate
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
