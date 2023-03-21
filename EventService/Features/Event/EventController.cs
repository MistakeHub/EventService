using EventService.Features.Event.Commands.Create;
using EventService.Features.Event.Commands.GetAll;
using EventService.Features.Event.Commands.GetAllForTheWeek;
using EventService.Features.Event.Commands.Remove;
using EventService.Features.Ticket.Commands.SetFreeTickets;
using EventService.Features.Event.Commands.Update;
using EventService.Features.Filters;
using EventService.Features.Image.Commands.IsExists;
using EventService.Features.Space.Commands.IsExists;
using EventService.Features.Ticket.Commands.CheckSeat;

using EventService.Features.Ticket.Commands.IssueATicket;
using EventService.Features.User.Commands.IsExists;
using EventService.ObjectStorage.ViewModels;
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
[Route("api/events")]
[ApiController]
[ExceptionFilter]
public class EventController : ControllerBase
{


    private readonly IMediator _mediator;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mediator"></param>
    public EventController(IMediator mediator) { _mediator = mediator; }
    /// <summary>
    /// Метод возращающий коллекцию мероприятий
    /// </summary>
    [ProducesResponseType(typeof(List<Models.Entities.Event>), 200)]
    [ProducesResponseType(typeof(JsonResult), 400)]

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
    // POST api/<EventController>
    [HttpPost]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 400)]
    public async Task<ScResult<string>> Post([FromBody] RequestCreateEventModel model)
    {
        await _mediator.Send(new SpaceExistsCommand { Id = Guid.Parse(model.IdSpace) });

        await _mediator.Send(new ImageExistsCommand { Id = Guid.Parse(model.IdImage) });

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
    /// <param name="idevent">id мероприятия</param>
    /// <param name="model">Тестовое значение для idImage и idSpace:7febf16f-651b-43b0-a5e3-0da8da49e90d </param>
    // ReSharper disable once StringLiteralTypo Решарпер хочет idEvent
    [HttpPut("{idevent}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 400)]
    public async Task<ScResult<string>> Put(string idevent, [FromBody] UpdateEventModel model)
    {
          
        if (Guid.TryParse(model.IdSpace, out var idSpace))
        {
            await _mediator.Send(new SpaceExistsCommand { Id = idSpace});
             
        }
            

        if (Guid.TryParse(model.IdImage, out var idImage))
        {
            await _mediator.Send(new ImageExistsCommand { Id = idImage });

        }

        var result = await _mediator.Send(new UpdateEventCommand { Id = Guid.Parse(idevent), Start = model.Start, End = model.End, Title = model.Title, Description = model.Description, IdImage = idImage, IdSpace = idSpace});

        return result;

    }
    /// <summary>
    /// Метод удаляет определённое событие
    /// </summary>
    /// <response code="200">Событие удалено</response>
    /// <response code="400">Событие не было удалено</response>
    /// <param name="idevent">id события</param>
    // ReSharper disable once StringLiteralTypo Решарпер хочет idEvent
    [HttpDelete("{idevent}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 400)]
    public async Task<ScResult<string>> Delete(string idevent)
    {
        var result = await _mediator.Send(new RemoveEventCommand { Id = Guid.Parse(idevent) });
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
    /// <param name="isautogeneratedplaces">Флаг автогенерации места для билета</param>
    // ReSharper disable once StringLiteralTypo Решарпер хочет idEvent setFreeTickets
    [HttpPut("tickets/setfreetickets/{idevent}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 400)]
    // ReSharper disable once IdentifierTypo Решарпер хочет idEvent
    // ReSharper disable once IdentifierTypo 
    public async Task<ScResult<string>> Put(string idevent, int count, bool isautogeneratedplaces)
    {
        var result = await _mediator.Send(new SetFreeTicketsCommand{Count = count, IdEvent = Guid.Parse(idevent), IsAutoGeneratePlaces = isautogeneratedplaces});
        return result;
    }
    /// <summary>
    /// Проверяет у определённого пользователя наличие билета на определённое мероприятие
    /// </summary>
    /// <param name="idevent">Id мероприятия</param>
    /// /// <param name="idowner">Id предпологаемого владельца</param>
    // ReSharper disable once StringLiteralTypo Решарпер хочет idEvent и haveATicket
    [HttpGet("tickets/{idevent}/haveaticket")]
    [ProducesResponseType(typeof(ScResult<bool>), 200)]
    // ReSharper disable once IdentifierTypo
    public async Task<ScResult<bool>> HaveATicket(string idevent, string idowner)
    {
        var result = await _mediator.Send(new HaveATicketCommand{IdEvent = Guid.Parse(idevent), IdOwner = Guid.Parse(idowner) });
        return result ;
    }
    /// <summary>
    /// Выдаёт билет на определённое мероприятие определённому пользователю
    /// </summary>
    /// <response code="200">Билет был успешно выдан</response>
    /// <response code="400">Билет не был выдан</response>
    /// <param name="idevent">Id мероприятия</param>
    /// <param name="idowner">Id пользователя</param>
    [HttpPut("tickets/{idevent}")]
    [ProducesResponseType(typeof(ScResult<Models.Entities.Ticket>), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 400)]
    public async Task<ScResult<Models.Entities.Ticket>> IssueTicket(string idevent, string idowner)
    {
        if (Guid.TryParse(idowner, out var userid))await _mediator.Send(new UserExistsCommand { Id = userid });
        var result = await _mediator.Send(new IssueATicketCommand{IdEvent = Guid.Parse(idevent), IdOwner = userid });
        return result;
    }

    /// <summary>
    /// Метод Проверяет, зарезервированно ли место на определённый билет
    /// </summary>
    /// <response code="200">У билета есть место</response>
    /// <response code="400">У билета не назначенно место</response>
    /// <param name="idevent">Id мероприятия</param>
    /// <param name="idticket">Id билета</param>
    [HttpPut("tickets/{idevent}/seat")]
    [ProducesResponseType(typeof(int?), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 400)]
    public async Task<ScResult<int?>> CheckTicket(string idevent, string idticket)
    {
        var result = await _mediator.Send(new CheckSeatCommand { IdEvent = Guid.Parse(idevent), IdTicket = Guid.Parse(idticket) });
        return result;
    }

}