using Dgm.Common.Attributes;
using Dgm.Common.Authorization.Claim.Resource;
using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.Shift.ShiftFrequency;
using Resource.Application.Query.Shift.ShiftFrequency;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.Shift
{
    public class ShiftFrequencyController : BaseController
    {
        [HttpGet("Get/GetAll")]
        [Permission(ShiftClaimConstant.ViewShiftFrequency)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(request: new GetAllShiftFrequencyDetail.GetAllShiftFrequencyDetailQuery()));
        }

        [HttpGet("Get/GetById/{id}")]
        [Permission(ShiftClaimConstant.ViewShiftFrequency)]
        public async Task<IActionResult> GetSingleById(string id)
        {
            return Ok(await Mediator.Send(request: new GetSingleShiftFrequencyDetail.GetSingleShiftFrequencyDetailQuery { Id = id }));
        }

        [HttpPost("Create")]
        [Permission(ShiftClaimConstant.WriteShiftFrequency)]
        public async Task<IActionResult> Create(AddShiftFrequency.AddShiftFrequencyCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("Update/{id}")]
        [Permission(ShiftClaimConstant.WriteShiftFrequency)]
        public async Task<IActionResult> Update(string id, UpdateShiftFrequency.UpdateShiftFrequencyCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("Delete/{id}")]
        [Permission(ShiftClaimConstant.WriteShiftFrequency)]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(request: new DeleteShiftFrequency.DeleteShiftFrequencyCommand { Id = id }));
        }
    }
}
