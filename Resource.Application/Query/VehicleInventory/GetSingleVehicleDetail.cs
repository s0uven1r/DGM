using Dgm.Common.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Models.VehicleInventory.Response;
using Resource.Domain.Persistence;
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
            private readonly AppDbContext _context;
            public Handler(AppDbContext context)
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
                                            VehicleName = x.VehicleName,
                                            CreatedBy = x.CreatedBy,
                                            CreatedDate = x.CreatedDate,
                                            Model = x.Model,
                                            Price = x.Price,
                                            SubModel = x.SubModel,
                                            UpdatedBy = x.UpdatedBy,
                                            UpdatedDate = x.UpdatedDate
                                        }).SingleOrDefaultAsync();

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
