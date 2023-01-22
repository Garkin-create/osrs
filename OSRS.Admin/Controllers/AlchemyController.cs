﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OSRS.Application.Models.Alchemy.Command;
using OSRS.Application.Models.Alchemy.Model.Input;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AlchemyController: Controller
    {
        private readonly IMediator _mediator;

        public AlchemyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("AddAlchemy")]
        public Task<Response> AddAlchemy(AddAlchemyInputModel model)
            => _mediator.Send(new AddAlchemyCommand(model));

    }
}