using EventService.EntityActivities.EventActiv.Commands.Create;
using EventService.EntityActivities.EventActiv.Commands.GetAll;
using EventService.EntityActivities.EventActiv.Commands.GetAllForTheWeek;
using EventService.EntityActivities.EventActiv.Commands.Remove;
using EventService.EntityActivities.EventActiv.Commands.Update;
using EventService.EntityActivities.ImageActiv.Commands.IsExists;
using EventService.EntityActivities.SpaceActiv.Commands.IsExists;
using EventService.Helpers;
using EventService.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EventService.Features.Event;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventService.EntityActivities.EventActiv
{
    [Route("api/[action]/events")]
    [ApiController]
    public class EventController : ControllerBase
    {


        private IMediator _mediator;

        public EventController(IMediator mediator) { _mediator = mediator; }
        /// <summary>
        /// Method returns Collection and status code
        /// </summary>
        [ProducesResponseType(typeof(List<Event>), 200)]

        [HttpGet]
        public async Task<ObjectResult> GETALL()
        {
            var result = await _mediator.Send(new GetAllEventsCommand());
            return new ObjectResult(result.Data) { StatusCode = result.StatusCode };
        }

        /// <summary>
        /// Method Creates an Event and returns bool value and status code
        /// </summary>
        /// <response code="200">Event has been created</response>
        /// <response code="400">Invalid values</response>
        /// <param name="model">Test value for idImage and idSpace:7febf16f-651b-43b0-a5e3-0da8da49e90d </param>
        

        // POST api/<EventController>
        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<ObjectResult> POST([FromBody] RequestCreateEventModel model)
        {


            var checkspace = await _mediator.Send(new SpaceExistsCommand() { Id = Guid.Parse(model.IdSpace) });

            if (!(bool)checkspace.Data) return new ObjectResult(checkspace.Data) { StatusCode = checkspace.StatusCode };

            var checkimage = await _mediator.Send(new ImageExistsCommand() { Id = Guid.Parse(model.IdImage) });

            if (!(bool)checkimage.Data) return new ObjectResult(checkimage.Data) { StatusCode = checkimage.StatusCode };

            var result = await _mediator.Send(new CreateEventCommand() { Start = model.Start, End = model.End, Title = model.Title, Description = model.Description, IdImage = Guid.Parse(model.IdImage), IdSpace = Guid.Parse(model.IdSpace) });

            return new ObjectResult(result.Data) { StatusCode = result.StatusCode };

 

        }
        /// <summary>
        /// Medthod Updates specific Event by id and returns bool value and status code
        /// </summary>

        /// <response code="200">Event has been updated</response>
        /// <response code="400">Invalid values</response>
        /// <param name="id">string id of Event</param>
        /// <param name="model">Test value for idImage and idSpace:7febf16f-651b-43b0-a5e3-0da8da49e90d </param>
        [HttpPut("{id}")]
         [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<ObjectResult> PUT(string id,[FromBody] UpdateEventModel model)
        {
          
            if (model.IdSpace != null)
            {
                var checkspace = await _mediator.Send(new SpaceExistsCommand() { Id = Guid.Parse(model.IdSpace) });
                if (!(bool)checkspace.Data) return new ObjectResult(checkspace.Data) { StatusCode = checkspace.StatusCode };
            }

            if (model.IdImage != null)
            {
                var checkimage = await _mediator.Send(new ImageExistsCommand() { Id = Guid.Parse(model.IdImage) });

                if (!(bool)checkimage.Data) return new ObjectResult(checkimage.Data) { StatusCode = checkimage.StatusCode };
            }


            var result = await _mediator.Send(new UpdateEventCommand() { Id = Guid.Parse(id), Start = model.Start, End = model.End, Title = model.Title, Description = model.Description, IdImage = Guid.Parse(model.IdImage), IdSpace = Guid.Parse(model.IdSpace) });

            return new ObjectResult(result.Data) { StatusCode = result.StatusCode };

        }
        /// <summary>
        /// Method Removes particular Event by id and returns bool value and status code
        /// </summary>
        /// <response code="200">Event has been removed by specific id</response>
        /// <response code="400">Invalid values</response>
        /// <param name="id">string id of Event</param>

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<ObjectResult> DELETE(string id)
        {
            var result = await _mediator.Send(new RemoveEventCommand() { Id = Guid.Parse(id) });
            return new ObjectResult(result.Data) { StatusCode = result.StatusCode };
        }

        /// <summary>
        /// Returns All Event for the current week and status code
        /// </summary>

        [HttpGet]
        [ProducesResponseType(typeof(List<Event>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<ObjectResult> GETALLFORTHEWEEK()
        {
            var result = await _mediator.Send(new GetAllEventsForTheWeekCommand());
            return new ObjectResult(result.Data) { StatusCode = result.StatusCode };
        }

    }
}
