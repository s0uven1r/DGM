using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.Account.AccountHead;
using Resource.Application.Common.Interfaces;
using Resource.Application.Query.Account.AccountHead;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.Account
{
    [Authorize]
    public class AccountHeadController : BaseController
    {
        private readonly IAccountHeadCountService _accountHeadCountService;
        public AccountHeadController(IAccountHeadCountService accountHeadCountService)
        {
            _accountHeadCountService = accountHeadCountService;
        }
        AccountHeadController()
        {

        }
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
        [HttpGet("Get/GetById/{id}")]
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

        [HttpGet]
        [Route("GetAccountNumber")]
        [AllowAnonymous]

        public async Task<IActionResult> GetAccountDetails(string type, string alias)
        {
            var data = await _accountHeadCountService.GenerateAccountNumber(type,alias);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetAccountDetails")]
        public async Task<IActionResult> GetAccountDetails(string value)
        {
            var data = await Mediator.Send(request: new GetAllAccountHeadDetail.GetAllAccountHeadQuery());
            var vehicleAccountDetails = data.ToList().Where(x => x.Title.Contains(value)).Select(x => new KeyValuePair<string, string>(string.Join(" ", x.Title, "-", x.AccountNumber), x.AccountNumber)).ToList();
            return Ok(vehicleAccountDetails);
        }
    }
}
