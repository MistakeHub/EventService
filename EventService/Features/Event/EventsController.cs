

using EventService.Features.Event.Create;
using EventService.Features.Event.GetAll;
using EventService.Features.Event.GetAllForTheWeek;
using EventService.Features.Event.Remove;
using EventService.Features.Event.Update;
using EventService.Features.Filters;
using EventService.Features.Image.IsExists;
using EventService.Features.Space.IsExists;
using EventService.Features.Ticket.CheckSeat;
using EventService.Features.Ticket.IssueATicket;
using EventService.Features.Ticket.SetFreeTickets;
using EventService.Features.User.IsExists;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using Microsoft.AspNetCore.Authentication.JwtBearer;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventService.Features.Event;

/// <summary>
/// Контроллер мероприятия
/// </summary>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
[ExceptionFilter]
public class EventsController : ControllerBase
{


    private readonly IMediator _mediator;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mediator"></param>
    public EventsController(IMediator mediator) { _mediator = mediator; }
    /// <summary>
    /// Метод возращающий коллекцию мероприятий
    /// </summary>
    [ProducesResponseType(typeof(List<Event>), 200)]
    [ProducesResponseType(typeof(ScResult), 400)]
    [ProducesResponseType(typeof(ScResult), 500)]
    [HttpGet("GetAll")]
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
    // POST api/<EventsController>
    [HttpPost]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ScResult), 400)]
    [ProducesResponseType(typeof(ScResult), 500)]
    public async Task<ScResult<string>> Post([FromBody] RequestCreateEventModel model)
    {
		await _mediator.Send(new SpaceExistsCommand { Id = Guid.Parse(model.IdSpace), Authorization = HttpContext.Request.Headers.Authorization });
		
		await _mediator.Send(new ImageExistsCommand { Id = Guid.Parse(model.IdImage), Authorization = HttpContext.Request.Headers.Authorization });
		

        var result = await _mediator.Send(new CreateEventCommand
        {
            Start = model.Start, End = model.End, Title = model.Title, Description = model.Description,
            IdImage = Guid.Parse(model.IdImage), IdSpace = Guid.Parse(model.IdSpace)
        });

        return result;

    }
    /// <summary>
    /// Метод Обновляет информацию об определённом событие
    /// </summary>
    /// <response code="200">Событие было обновлено</response>
    /// <response code="400">Событие не было обновлено</response>
    /// <param name="idEvent">id мероприятия</param>
    /// <param name="model">Тестовое значение для idImage и idSpace:7febf16f-651b-43b0-a5e3-0da8da49e90d </param>
    [HttpPut("{idEvent}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ScResult), 400)]
    [ProducesResponseType(typeof(ScResult), 500)]
    public async Task<ScResult<string>> Put(string idEvent, [FromBody] UpdateEventModel model)
    {

        if (Guid.TryParse(model.IdSpace, out var idSpace))
        {
            await _mediator.Send(new SpaceExistsCommand { Id = idSpace, Authorization = HttpContext.Request.Headers.Authorization });
           
        }

        if (Guid.TryParse(model.IdImage, out var idImage))
        {
            await _mediator.Send(new ImageExistsCommand { Id = idImage, Authorization = HttpContext.Request.Headers.Authorization } );

        }
    

        var result = await _mediator.Send(new UpdateEventCommand { Id = Guid.Parse(idEvent), Start = model.Start, End = model.End, Title = model.Title, Description = model.Description, IdImage = idImage, IdSpace = idSpace});

        return result;

    }
    /// <summary>
    /// Метод удаляет определённое событие
    /// </summary>
    /// <response code="200">Событие удалено</response>
    /// <response code="400">Событие не было удалено</response>
    /// <param name="idEvent">id события</param>
    [HttpDelete("{idEvent}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ScResult), 400)]
    [ProducesResponseType(typeof(ScResult), 500)]
    public async Task<ScResult<string>> Delete(string idEvent)
    {
        var result = await _mediator.Send(new RemoveEventCommand { Id = Guid.Parse(idEvent) });
        return result;
    }

    /// <summary>
    /// Метод возвращающий все события, запланированные на неделю
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<EventViewModel>), 200)]
    [ProducesResponseType(typeof(ScResult), 400)]
    [ProducesResponseType(typeof(ScResult), 500)]
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
    /// <param name="idEvent">Id мероприятия</param>
    /// <param name="count">количество билетов</param>
    /// <param name="isAutoGeneratedPlaces">Флаг автогенерации места для билета</param>
    // ReSharper disable once StringLiteralTypo Решарпер хочет idEvent setFreeTickets
    [HttpPut("tickets/setfreetickets/{idEvent}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ScResult), 400)]
    [ProducesResponseType(typeof(ScResult), 500)]

    public async Task<ScResult<string>> Put(string idEvent, int count, bool isAutoGeneratedPlaces)
    {
        var result = await _mediator.Send(new SetFreeTicketsCommand{Count = count, IdEvent = Guid.Parse(idEvent), IsAutoGeneratePlaces = isAutoGeneratedPlaces});
        return result;
    }
    /// <summary>
    /// Проверяет у определённого пользователя наличие билета на определённое мероприятие
    /// </summary>
    /// <param name="idEvent">Id мероприятия</param>
    /// /// <param name="idOwner">Id предпологаемого владельца</param>

    [HttpGet("tickets/{idEvent}/haveaticket")]
    [ProducesResponseType(typeof(ScResult<bool>), 200)]
    // ReSharper disable once IdentifierTypo
    public async Task<ScResult<bool>> HaveATicket(string idEvent, string idOwner)
    {
        var result = await _mediator.Send(new HaveATicketCommand{IdEvent = Guid.Parse(idEvent), IdOwner = Guid.Parse(idOwner) });
        return result ;
    }

    /// <summary>
    /// Выдаёт билет на определённое мероприятие определённому пользователю
    /// </summary>
    /// <response code="200">Билет был успешно выдан</response>
    /// <response code="400">Билет не был выдан</response>
    /// <param name="idEvent">Id мероприятия</param>
    /// <param name="idOwner">Id пользователя</param>
    /// <param name="price">цена за билет</param>
    [HttpPut("tickets/{idEvent}")]
    [ProducesResponseType(typeof(ScResult<Ticket.Ticket>), 200)]
    [ProducesResponseType(typeof(ScResult), 400)]
    [ProducesResponseType(typeof(ScResult), 500)]
    public async Task<ScResult<Ticket.Ticket>> IssueTicket(string idEvent, string idOwner, decimal price)
    {
        if (Guid.TryParse(idOwner, out var userid))
            await _mediator.Send(new UserExistsCommand { Id = userid });
        var result = await _mediator.Send(new IssueATicketCommand{IdEvent = Guid.Parse(idEvent), IdOwner = userid,Price = price, Authorization = HttpContext.Request.Headers.Authorization});
        return result;
    }

    /// <summary>
    /// Метод Проверяет, зарезервированно ли место на определённый билет
    /// </summary>
    /// <response code="200">У билета есть место</response>
    /// <response code="400">У билета не назначенно место</response>
    /// <param name="idEvent">Id мероприятия</param>
    /// <param name="idTicket">Id билета</param>
    [HttpPut("tickets/{idEvent}/seat")]
    [ProducesResponseType(typeof(int?), 200)]
    [ProducesResponseType(typeof(ScResult), 400)]
    [ProducesResponseType(typeof(ScResult), 500)]
    public async Task<ScResult<int?>> CheckTicket(string idEvent, string idTicket)
    {
        var result = await _mediator.Send(new CheckSeatCommand { IdEvent = Guid.Parse(idEvent), IdTicket = Guid.Parse(idTicket) });
        return result;
    }

}