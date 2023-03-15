using EventService.EntityActivities.SpaceActiv.Commands.Get;
using EventService.Helpers;
using EventService.Models.Entities;
using EventService.Models.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventService.EntityActivities.SpaceActiv
{
    [Route("api/[action]/spaces")]
    [ApiController]
    public class SpaceController : ControllerBase
    {
        private IMediator _mediator;

        public SpaceController(IMediator mediator) { _mediator = mediator; }

        /// <summary>
        /// Method returns Collection and status code
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<Space>), 200)]
        public async Task<ObjectResult> GET(string id)
        {
            var result = await _mediator.Send(new GetSpaceCommand() { Id=Guid.Parse(id)});
            return new ObjectResult(result) { StatusCode=result.StatusCode};
        }

   
    }
}
