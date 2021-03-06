﻿using System;
using System.Threading.Tasks;
using EricBach.LambdaLogger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pecuniary.Account.Data.Models;
using Pecuniary.Account.Query.Queries;

namespace Pecuniary.Account.Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountReadModel>> GetAsync(Guid id)
        {
            Logger.Log($"Received {nameof(GetAccountQuery)}");

            return await _mediator.Send(new GetAccountQuery {Id = id});
        }
    }
}
