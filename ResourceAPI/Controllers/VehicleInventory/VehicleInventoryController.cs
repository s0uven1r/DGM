using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.VehicleInventory;
using Resource.Application.Models.VehicleInventory.Response;
using Resource.Application.Query.VehicleInventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.VehicleInventory
{
    public class VehicleInventoryController : BaseController
    {
        //[Permission(Permission.)]
        [HttpGet("Get/GetAllVehicle")]
        public async Task<ActionResult<List<VehicleDetailResponseViewModel>>> GetAll() => await Mediator.Send(request: new GetAllVehicleDetail.GetAllVehicleDetailQuery());

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpGet("Get/GetVehicleDetailById/{id}")]
        public async Task<ActionResult<VehicleDetailResponseViewModel>> GetSingleVehicleById(string id) => await Mediator.Send(request: new GetSingleVehicleDetail.GetSingleVehicleDetailQuery { Id = id });

        /// <summary>
        /// <param name="VehicleName"></param>
        /// <param name="Model"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost("Create")]
        //[Permission(Permission.)]
        public async Task<ActionResult<Unit>> CreateVehicle(AddVehicleDetail.AddVehicleDetailCommand command) => await Mediator.Send(command);

    }
}
