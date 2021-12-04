using Dgm.Common.Attributes;
using Dgm.Common.Authorization.Claim.Resource;
using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.Account.AccountEntry;
using Resource.Application.Query.Account.AccountEntry;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.Account
{
    public class AccountEntryController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/GetAll")]
        [Permission(AccountingClaimConstant.ViewAccountingTransactionEntry)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(request: new GetAllAccountEntryDetail.GetAllAccountEntryQuery()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/GetSingleAccountEntry")]
        [Permission(AccountingClaimConstant.ViewAccountingTransactionEntry)]
        public async Task<IActionResult> GetSingleAccountEntry([FromQuery] GetSingleAccountEntryDetail.GetSingleAccountEntryQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [Permission(AccountingClaimConstant.WriteAccountingTransactionEntry)]
        public async Task<IActionResult> CreateAccountEntry([FromBody] AddAccountEntryDetail.AddAccountEntryDetailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
