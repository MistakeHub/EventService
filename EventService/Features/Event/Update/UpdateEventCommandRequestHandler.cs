
using AutoMapper;
using EventService.Features.Event.Update.Notifications.RemoveImage;
using EventService.Features.Event.Update.Notifications.RemoveSpace;
using EventService.Infrastructure.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Update;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды обновления мероприятия
/// </summary>
public class UpdateEventCommandRequestHandler : IRequestHandler<UpdateEventCommand, ScResult<string>>
{
    private readonly IBaseEventService _baseEventService;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper; 
    /// <summary>
    /// Конструктор
    /// </summary>
    public UpdateEventCommandRequestHandler(IBaseEventService baseEventService, IMediator mediator, IMapper mapper)
    {
       _mediator=mediator;
        _baseEventService = baseEventService;
        _mapper=mapper;
    }
    /// <summary>
    /// Обработчик 
    /// </summary>

    public async Task<ScResult<string>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<string>();


        var updateEvent = await _baseEventService.GetEventById(request.Id);

        if (updateEvent == null) throw new ScException("Данное мероприятие не существует");

        if (request.IdSpace == Guid.Empty)
        {
            await _mediator.Publish(new RemoveSpaceUpdateEvent { IdEvent = updateEvent.Id, IdSpace = updateEvent.IdSpace }, cancellationToken);
            returnResult.Result ="Пространство удалено";
            return returnResult;
        }

        var eventDefault = _mapper.Map<Event>(request);



        var resultUpdate = await _baseEventService.UpdateEvent(eventDefault);
        if (!resultUpdate) throw new ScException("Мероприятие не было обновлено");
        
        if (request.IdImage == Guid.Empty) await _mediator.Publish(new RemoveImageUpdateEvent { IdImage = eventDefault.IdImage }, cancellationToken);
        returnResult.Result = "Мероприятие обновлено";
       

        return returnResult;
    }
}