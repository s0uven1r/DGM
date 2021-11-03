using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.Shift.IndividualShift;
using Resource.Application.Query.Shift.IndividualShift;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.Shift
{
    public class IndividualShiftController : BaseController
    {

        //[HttpGet("Get/GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(await Mediator.Send(request: new GetAllShiftDetail.GetAllShiftDetailQuery()));
        //}

        [HttpGet("Get/GetAllByUserAccountNumber/{accountNumber}")]
        public async Task<IActionResult> GetAllByAccountNumber(string accountNumber)
        {
            return Ok(await Mediator.Send(request: new GetAllIndividualShiftByAccountNumber.GetAllIndividualShiftByAccountNumberQuery { UserAccountNumber = accountNumber }));
        }
        [HttpGet("Get/GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await Mediator.Send(request: new GetAllIndividualShiftByAccountNumber.GetAllIndividualShiftByAccountNumberQuery { UserAccountNumber = id }));
        }
        //[HttpPost("Create")]
        //public async Task<IActionResult> Create(AddIndividualShiftDetail.AddIndividualShiftDetailCommand command)
        //{
        //    return Ok(await Mediator.Send(command));
        //}

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id, UpdateIndividualShiftDetail.UpdateIndividualShiftDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("UpdateShiftTrainer/{id}")]
        public async Task<IActionResult> Update(string id, UpdateIndividualShiftTrainerDetail.UpdateIndividualShiftTrainerDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        //[HttpDelete("Delete/{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    return Ok(await Mediator.Send(request: new DeleteShiftDetail.DeleteShiftDetailCommand { Id = id }));
        //}
    }
}
