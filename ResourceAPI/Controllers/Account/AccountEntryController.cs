﻿using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.Account.AccountEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Resource.Application.Query.Account.AccountHead.GetSingleAccountHeadDetail;

namespace ResourceAPI.Controllers.Account
{
    public class AccountEntryController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/GetVehicleDetailById/{id}")]
        public async Task<IActionResult> GetSingleVehicleById(string id)
        {
            return Ok(await Mediator.Send(request: new GetSingleAccountEntryQuery.get { Id = id }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAccountEntry([FromBody] AddAccountEntryDetail.AddAccountEntryDetailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
