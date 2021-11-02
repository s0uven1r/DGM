using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.Shift.ShiftFrequency;
using Resource.Application.Query.Shift.ShiftFrequency;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.Shift
{
    public class ShiftFrequencyController : BaseController
    {
        [HttpGet("Get/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(request: new GetAllShiftFrequencyDetail.GetAllShiftFrequencyDetailQuery()));
        }

        [HttpGet("Get/GetById/{id}")]
        public async Task<IActionResult> GetSingleById(string id)
        {
            return Ok(await Mediator.Send(request: new GetSingleShiftFrequencyDetail.GetSingleShiftFrequencyDetailQuery { Id = id }));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(AddShiftFrequency.AddShiftFrequencyCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id, UpdateShiftFrequency.UpdateShiftFrequencyCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(request: new DeleteShiftFrequency.DeleteShiftFrequencyCommand { Id = id }));
        }
    }
}
