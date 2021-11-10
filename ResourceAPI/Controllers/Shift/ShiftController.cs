using Dgm.Common.Attributes;
using Dgm.Common.Authorization.Claim.Resource;
using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.Shift.Shift;
using Resource.Application.Query.Shift.Shift;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.Shift
{
    public class ShiftController : BaseController
    {
        [HttpGet("Get/GetAll")]
        [Permission(ShiftClaimConstant.ViewShift)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(request: new GetAllShiftDetail.GetAllShiftDetailQuery()));
        }

        [HttpGet("Get/GetById/{id}")]
        [Permission(ShiftClaimConstant.ViewShift)]
        public async Task<IActionResult> GetSingleById(string id)
        {
            return Ok(await Mediator.Send(request: new GetSingleShiftDetail.GetSingleShiftDetailQuery { Id = id }));
        }

        [HttpPost("Create")]
        [Permission(ShiftClaimConstant.WriteShift)]
        public async Task<IActionResult> Create(AddShiftDetail.AddShiftDetailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("Update/{id}")]
        [Permission(ShiftClaimConstant.WriteShift)]
        public async Task<IActionResult> Update(string id, UpdateShiftDetail.UpdateShiftDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("Delete/{id}")]
        [Permission(ShiftClaimConstant.WriteShift)]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(request: new DeleteShiftDetail.DeleteShiftDetailCommand { Id = id }));
        }
    }
}
