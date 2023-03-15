using EventService.EntityActivities.ImageActiv.Commands.Get;
using EventService.EntityActivities.SpaceActiv.Commands.Get;
using EventService.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventService.EntityActivities.ImageActiv
{
    [Route("api/[action]/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
      private IMediator _mediator;

        public ImageController(IMediator mediator) { _mediator= mediator; }
        // GET api/<ImageController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<Image>), 200)]
        /// <summary>
        /// Method returns Collection and status code
        /// </summary>
        /// TODO: Возвращать поток данных изображения
        public async Task<ObjectResult> GET(string id)
        {
            var result = await _mediator.Send(new GetImageCommand() { Id = Guid.Parse(id) });
            return new ObjectResult(result) { StatusCode = result.StatusCode };
        }

     
    }
}
