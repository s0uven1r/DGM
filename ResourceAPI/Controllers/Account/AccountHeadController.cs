using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.Account.AccountHead;
using Resource.Application.Query.Account.AccountHead;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.Account
{
    public class AccountHeadController : BaseController
    {
        //[Permission(Permission.)]
        [HttpGet("Get/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(request: new GetAllAccountHeadDetail.GetAllAccountHeadQuery()));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpGet("Get/GetSingleById/{id}")]
        public async Task<IActionResult> GetSingleById(string id)
        {
            return Ok(await Mediator.Send(request: new GetSingleAccountHeadDetail.GetSingleAccountHeadQuery { Id = id }));
        }

        /// <summary> 
        /// <param name="title"></param>
        /// <param name="accountTypeId"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(AddAccountHeadDetail.AddAccountHeadDetailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>s
        /// <param name="Id"></param>
        /// <param name="title"></param>
        /// <param name="accountTypeId"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id, UpdateAccountHeadDetail.UpdateAccountHeadDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }
    }
}
