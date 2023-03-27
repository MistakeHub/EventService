
using EventService.Models.Entities;
using EventService.Models.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Space.Commands.Remove;

/// <summary>
/// Класс обработчик команды удаления пространства
/// </summary>
// ReSharper disable once UnusedMember.Global потому что хэндлер
public class RemoveSpaceEventRequestHandler:IRequestHandler<RemoveSpaceEvent, ScResult<string>>
{
    private readonly IBaseEventService _baseEventService;
    private readonly IBaseRabbitMqService _baseRabbitMqService;
    /// <summary>
    /// Конструктор
    /// </summary>
    public RemoveSpaceEventRequestHandler(IBaseEventService baseEventService, IBaseRabbitMqService baseRabbitMqService)
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
    public Task<ScResult<string>> Handle(RemoveSpaceEvent request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<string>();

        var resultDelete = _baseEventService.RemoveSpaceEventById(request.IdEvent);


        if (resultDelete == Guid.Empty) throw new ScException("Пространство не было удалено");
        _baseRabbitMqService.SendMessage(new EventMessage { IdEntity = resultDelete, Type = TypeEvent.SpaceDeleteEvent }, "event");
        returnResult.Result = "Изображение было удалено";

        return Task.FromResult(returnResult);
    }
}