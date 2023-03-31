

using EventService.Infrastructure.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Remove;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды удаления мероприятия
/// </summary>
public class RemoveEventCommandRequestHandler : IRequestHandler<RemoveEventCommand, ScResult<string>>
{
    private readonly IBaseEventService _baseEventService;
    private readonly IMediator _mediator;
    /// <summary>
    /// Конструктор
    /// </summary>
    public RemoveEventCommandRequestHandler(IBaseEventService baseEventService, IMediator mediator) {

        _baseEventService = baseEventService;
        _mediator  = mediator;
    }
    /// <summary>
    /// Обработчик 
    /// </summary>
  
    public async Task<ScResult<string>> Handle(RemoveEventCommand request, CancellationToken cancellationToken)
    {
        
        var returnResult = new ScResult<string>();

        var checkEvent = _baseEventService.GetEventById(request.Id);
        if (checkEvent == null) throw new ScException("Данное мероприятие не существует");
        var resultDelete = await _baseEventService.DeleteEvent(request.Id);

        if (!resultDelete) throw new ScException("Мероприятие не было удалено");

       await _mediator.Publish(new RemoveEventCommand{Id = request.Id}, cancellationToken);

        returnResult.Result= "Мероприятие было удалено";
       
        return returnResult;
    }
}