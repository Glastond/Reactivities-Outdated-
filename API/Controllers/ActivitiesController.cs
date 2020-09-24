using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// Inorder to keep the Controllers as lean as possible the Main logic for the controllers is 
// written in the Application.Activities file and is connected to it using a Mediator (MediatR).

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // @desc    Get Activities.
        // @route   GET /api/activities
        // @access  Public
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> List()
        {
            return await _mediator.Send(new List.Query());
        }

        // @desc    Get Activity.
        // @route   GET /api/activities/:id
        // @access  Public
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Details(Guid id)
        {
            return await _mediator.Send(new Details.Query { Id = id });
        }

        // @desc    Create Activity.
        // @route   POST /api/activities
        // @access  Public
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // @desc    Edit Activity.
        // @route   PUT /api/activities/:id
        // @access  Public
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        // @desc    Delete Activity.
        // @route   DELETE /api/activities/:id
        // @access  Public
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await _mediator.Send(new Delete.Command { Id = id });
        }

    }
}