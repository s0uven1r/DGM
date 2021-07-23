using Dgm.Common.Enum;
using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.Account.AccountType;
using Resource.Application.Query.Account.AccountType;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.Account
{
    public class AccountTypeController : BaseController
    {
        //[Permission(Permission.)]
        [HttpGet("Get/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(request: new GetAllAccountTypeDetail.GetAllAccountTypeQuery()));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpGet("Get/GetSingleById/{id}")]
        public async Task<IActionResult> GetSingleById(string id)
        {
            return Ok(await Mediator.Send(request: new GetSingleAccountTypeDetail.GetSingleAccountTypeQuery { Id = id }));
        }

        /// <summary> 
        /// <param name="title"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(AddAccountTypeDetail.AddAccountTypeDetailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>s
        /// <param name="Id"></param>
        /// <param name="title"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id, UpdateAccountTypeDetail.UpdateAccountTypeDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("Get/GetAccountTypeEnumDDL")]
        public IActionResult GetAccountTypeEnumDDL()
        {
            return Ok(AccountTypeEnumConversion.GetEnumList());
        }
    }
}
