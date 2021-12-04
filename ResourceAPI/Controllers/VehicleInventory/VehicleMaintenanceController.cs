using Dgm.Common.Attributes;
using Dgm.Common.Authorization.Claim.Resource;
using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.VehicleInventory;
using Resource.Application.Query.VehicleInventory;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.VehicleInventory
{
    public class VehicleMaintenanceController : BaseController
    {
        [HttpGet]
        [Permission(VehicleClaimConstant.ViewMaintenance)]
        public async Task<IActionResult> GetDetail()
        {
            return Ok(await Mediator.Send(request: new GetAllVehicleMaintenanceDetail.GetAllVehicleDetailQuery()));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Permission(VehicleClaimConstant.ViewMaintenance)]
        public async Task<IActionResult> GetDetail(string id)
        {
            return Ok(await Mediator.Send(request: new GetSingleVehicleMaintenanceDetail.GetSingleVehicleDetailQuery { Id = id }));
        }

        /// <summary>
        /// <param name="VehicleId"></param>
        /// <param name="TypeId"></param>
        /// <param name="Remarks"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost("Create")]
        [Permission(VehicleClaimConstant.WriteMaintenance)]
        public async Task<IActionResult> Create(AddVehicleMaintenanceDetail.AddVehicleMaintenanceDetailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>s
        /// <param name="VehicleId"></param>
        /// <param name="TypeId"></param>
        /// <param name="Remarks"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Permission(VehicleClaimConstant.WriteMaintenance)]
        public async Task<IActionResult> Update(string id, UpdateVehicleMaintenanceDetail.UpdateVehicleMaintenanceDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Permission(VehicleClaimConstant.WriteMaintenance)]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(request: new DeleteVehicleMaintenanceDetail.DeleteVehicleMaintenanceDetailCommand { Id = id }));
        }
    }
}
