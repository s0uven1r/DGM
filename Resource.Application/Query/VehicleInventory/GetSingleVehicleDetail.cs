using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.VehicleInventory.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Query.VehicleInventory
{
    public class GetSingleVehicleDetail
    {
        public class GetSingleVehicleDetailQuery : IRequest<VehicleDetailResponseViewModel>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<GetSingleVehicleDetailQuery, VehicleDetailResponseViewModel>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<VehicleDetailResponseViewModel> Handle(GetSingleVehicleDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var getVehicle = await _context.VehicleDetails.Where(q => !q.IsDeleted && q.Id == request.Id)
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
                                        }).SingleOrDefaultAsync();
                    if (getVehicle == null) throw new AppException("Invalid! Vehicle Detail not found!");
                    return getVehicle;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
