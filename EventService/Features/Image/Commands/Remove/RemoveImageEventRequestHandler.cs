
using EventService.Models.Entities;
using EventService.Models.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Image.Commands.Remove;

/// <summary>
/// Класс обработчик команды удаления изображения из мероприятия 
/// </summary>
// ReSharper disable once UnusedMember.Global потому что хэндлер
public class RemoveImageEventRequestHandler:IRequestHandler<RemoveImageEvent, ScResult<string>>
{
    private readonly IBaseEventService _baseEventService;
    private readonly IBaseRabbitMqService _baseRabbitMqService;
    /// <summary>
    /// Конструктор
    /// </summary>
    public RemoveImageEventRequestHandler(IBaseEventService baseEventService, IBaseRabbitMqService baseRabbitMqService)
    {
        _baseRabbitMqService = baseRabbitMqService;
        _baseEventService = baseEventService;
    }
    /// <summary>
    /// Обработчик
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ScException"></exception>
    public Task<ScResult<string>> Handle(RemoveImageEvent request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<string>();
          
        var resultDelete = _baseEventService.RemoveEventImageById(request.IdEvent);


        if (resultDelete ==Guid.Empty) throw new ScException("Изображение не было удалено");
        _baseRabbitMqService.SendMessage(new EventMessage { IdEntity = resultDelete, Type = TypeEvent.ImageDeleteEvent}, "event");
        returnResult.Result = "Мероприятие было удалено";

        return Task.FromResult(returnResult);
    }
}