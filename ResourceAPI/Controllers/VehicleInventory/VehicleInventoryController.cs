using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.VehicleInventory;
using Resource.Application.Query.VehicleInventory;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.VehicleInventory
{
    public class VehicleInventoryController : BaseController
    {
        //[Permission(Permission.)]
        [HttpGet("Get/GetAllVehicle")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(request: new GetAllVehicleDetail.GetAllVehicleDetailQuery()));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpGet("Get/GetVehicleDetailById/{id}")]
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
        //[Permission(Permission.)]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateVehicle(AddVehicleDetail.AddVehicleDetailCommand command)
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
        //[Permission(Permission.)]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateVehicle(string id, UpdateVehicleDetail.UpdateVehicleDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteVehicle(string id)
        {
            return Ok(await Mediator.Send(request: new DeleteVehicleDetail.DeleteVehicleDetailCommand { Id = id }));
        }
    }
}
