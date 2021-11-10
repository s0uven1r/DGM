using Dgm.Common.Attributes;
using Dgm.Common.Authorization.Claim.Resource;
using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.VehicleInventory;
using Resource.Application.Query.VehicleInventory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.VehicleInventory
{
    public class VehicleInventoryController : BaseController
    {
        [HttpGet("Get/GetAllVehicle")]
        [Permission(VehicleClaimConstant.ViewVehicle)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(request: new GetAllVehicleDetail.GetAllVehicleDetailQuery()));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get/GetVehicleDetailById/{id}")]
        [Permission(VehicleClaimConstant.ViewVehicle)]
        public async Task<IActionResult> GetSingleVehicleById(string id)
        {
            return Ok(await Mediator.Send(request: new GetSingleVehicleDetail.GetSingleVehicleDetailQuery { Id = id }));
        }

        /// <summary>
        /// <param name="RegistrationNumber"></param>
        /// <param name="ChasisNumber"></param>
        /// <param name="EngineNumber"></param>
        /// <param name="Model"></param>
        /// <param name="SubModel"></param>
        /// <param name="Capacity"></param>
        /// <param name="ManufacturedYear"></param>
        /// <param name="Price"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost("Create")]
        [Permission(VehicleClaimConstant.ViewVehicle)]
        public async Task<IActionResult> CreateVehicle([FromBody] AddVehicleDetail.AddVehicleDetailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>s
        /// <param name="Id"></param>
        /// <param name="RegistrationNumber"></param>
        /// <param name="ChasisNumber"></param>
        /// <param name="EngineNumber"></param>
        /// <param name="Model"></param>
        /// <param name="SubModel"></param>
        /// <param name="Capacity"></param>
        /// <param name="ManufacturedYear"></param>
        /// <param name="Price"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        [Permission(VehicleClaimConstant.ViewVehicle)]
        public async Task<IActionResult> UpdateVehicle(string id, UpdateVehicleDetail.UpdateVehicleDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        [Permission(VehicleClaimConstant.ViewVehicle)]
        public async Task<IActionResult> DeleteVehicle(string id)
        {
            return Ok(await Mediator.Send(request: new DeleteVehicleDetail.DeleteVehicleDetailCommand { Id = id }));
        }

        [HttpGet]
        [Route("GetAccountDetails")]
        public async Task<IActionResult> GetAccountDetails(string value)
        {
            var data = await Mediator.Send(request: new GetAllVehicleDetail.GetAllVehicleDetailQuery());
            var vehicleAccountDetails = data.ToList().Where(x => x.RegistrationNumber.Contains(value)).Select(x => new KeyValuePair<string, string>(string.Join(" ", x.RegistrationNumber, "-", x.AccountNumber), x.AccountNumber)).ToList();
            return Ok(vehicleAccountDetails);
        }
    }
}
