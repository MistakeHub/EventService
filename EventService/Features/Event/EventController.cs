using EventService.Features.Event.Commands.Create;
using EventService.Features.Event.Commands.GetAll;
using EventService.Features.Event.Commands.GetAllForTheWeek;
using EventService.Features.Event.Commands.HaveATicket;
using EventService.Features.Event.Commands.IssueATicket;
using EventService.Features.Event.Commands.Remove;
using EventService.Features.Event.Commands.SetTickets;
using EventService.Features.Event.Commands.Update;
using EventService.Features.Filters;
using EventService.Features.Image.Commands.IsExists;
using EventService.Features.Space.Commands.IsExists;
using EventService.Models.Entities;
using EventService.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventService.Features.Event
{
    [Route("api/[action]/events")]
    [ApiController]
    [ExceptionFilter]
    public class EventController : ControllerBase
    {


        private readonly IMediator _mediator;

        public EventController(IMediator mediator) { _mediator = mediator; }
        /// <summary>
        /// Метод возращающий коллекцию мероприятий
        /// </summary>
        [ProducesResponseType(typeof(List<Models.Entities.Event>), 200)]
        [ProducesResponseType(typeof(JsonResult), 400)]

        [HttpGet]
        public async Task<ScResult<List<EventViewModel>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllEventsCommand());
            return result;
        }

        /// <summary>
        /// Метод создающий мероприятие
        /// </summary>
        /// <response code="200">Событие создано</response>
        /// <response code="400">Событие не было создано</response>
        /// <param name="model">Тестовое значение для idImage и idSpace:7febf16f-651b-43b0-a5e3-0da8da49e90d </param>
        

        // POST api/<EventController>
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<ScResult<string>> Post([FromBody] RequestCreateEventModel model)
        {
            var checkspace = await _mediator.Send(new SpaceExistsCommand() { Id = Guid.Parse(model.IdSpace) });

            ScResult<string> result=null;

            var checkimage = await _mediator.Send(new ImageExistsCommand() { Id = Guid.Parse(model.IdImage) });
            if (checkimage.Result && checkspace.Result)
            {
                 result = await _mediator.Send(new CreateEventCommand()
                {
                    Start = model.Start, End = model.End, Title = model.Title, Description = model.Description,
                    IdImage = Guid.Parse(model.IdImage), IdSpace = Guid.Parse(model.IdSpace)
                });

            }

            return result;

 

        }
        /// <summary>
        /// Метод Обновляет информацию об определённом событие
        /// </summary>

        /// <response code="200">Событие было обновлено</response>
        /// <response code="400">Событие не было обновлено</response>
        /// <param name="idevent">id события</param>
        /// <param name="model">Тестовое значение для idImage и idSpace:7febf16f-651b-43b0-a5e3-0da8da49e90d </param>
        [HttpPut("{idevent}")]
         [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<ScResult<string>> Put(string idevent, [FromBody] UpdateEventModel model)
        {
          
            if (model.IdSpace != null)
            {
                var checkspace = await _mediator.Send(new SpaceExistsCommand() { Id = Guid.Parse(model.IdSpace) });
             
            }

            if (model.IdImage != null)
            {
                var checkimage = await _mediator.Send(new ImageExistsCommand() { Id = Guid.Parse(model.IdImage) });

            }



            var result = await _mediator.Send(new UpdateEventCommand() { Id = Guid.Parse(idevent), Start = model.Start, End = model.End, Title = model.Title, Description = model.Description, IdImage = Guid.Parse(model.IdImage), IdSpace = Guid.Parse(model.IdSpace) });

            return result;

        }
        /// <summary>
        /// Метод удаляет определённое событие
        /// </summary>
        /// <response code="200">Событие удалено</response>
        /// <response code="400">Событие не было удалено</response>
        /// <param name="ideven">id события</param>

        [HttpDelete("{idevent}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<ScResult<string>> Delete(string id)
        {
            var result = await _mediator.Send(new RemoveEventCommand() { Id = Guid.Parse(id) });
            return result;
        }

        /// <summary>
        /// Метод возвращающий все события, запланированные на неделю
        /// </summary>

        [HttpGet]
        [ProducesResponseType(typeof(List<EventViewModel>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<ScResult<List<EventViewModel>>> GetAllOfTheWeek()
        {
            var result = await _mediator.Send(new GetAllEventsForTheWeekCommand());
            return result;
        }
        /// <summary>
        /// Метод создаёт билеты на определённое мероприятие определённого количества
        /// </summary>
        /// <response code="200">Билеты были успешно созданы</response>
        /// <response code="400">Билеты не были созданы</response>
        /// <param name="idevent">Id мероприятия</param>
        /// <param name="count">количество билетов</param>
        [HttpPut("tickets/settickets/{idevent}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<ScResult<string>> Put(string idevent, int count)
        {
            var result = await _mediator.Send(new SetTicketsCommand(){Count = count, IdEvent = Guid.Parse(idevent) });
            return result;
        }
        /// <summary>
        /// Проверяет у определённого пользователя наличие билета на определённое мероприятие
        /// </summary>
        /// <param name="idevent">Id мероприятия</param>
        /// /// <param name="idowner">Id предпологаемого владельца</param>
        [HttpGet("tickets/{idevent}/haveaticket")]
        [ProducesResponseType(typeof(ScResult<bool>), 200)]
   
        public async Task<ScResult<bool>> HaveATicket(string idevent, string idowner)
        {
            var result = await _mediator.Send(new HaveATicketCommand(){IdEvent = Guid.Parse(idevent), IdOwner = Guid.Parse(idowner) });
            return result ;
        }
        /// <summary>
        /// Выдаёт билеты на определённое мероприятие определённому пользователю
        /// </summary>
        /// <response code="200">Билеты были успешно созданы</response>
        /// <response code="400">Билеты не были созданы</response>
        /// <param name="idevent">Id мероприятия</param>
        /// <param name="idowner">Id мероприятия</param>
        /// <param name="place">место</param>
        [HttpPut("tickets/{idevent}")]
        [ProducesResponseType(typeof(ScResult<Ticket>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<ScResult<Ticket>> Get(string idevent, string idowner, int place)
        {
            var result = await _mediator.Send(new IssueATicketCommand(){IdEvent = Guid.Parse(idevent), IdOwner = Guid.Parse(idowner), Place = place});
            return result;
        }

    }
}
