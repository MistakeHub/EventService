
using EventService.Features.Event.Remove;
using EventService.Features.RabbitMq;
using EventService.Features.RabbitMq.SendMessage;

using MediatR;

namespace EventService.Features.Event.Update.Notifications.RemoveSpace;

/// <summary>
/// Оповещение об удалении пространства
/// </summary>
// ReSharper disable once UnusedMember.Global
public class RemoveSpaceUpdateEventHandler : INotificationHandler<RemoveSpaceUpdateEvent>
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mediator"></param>

    public RemoveSpaceUpdateEventHandler(IMediator mediator)
    {
        _mediator = mediator;


    }
    /// <summary>
    /// Хэндлер 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    public async Task Handle(RemoveSpaceUpdateEvent notification, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RemoveEventCommand { Id = notification.IdEvent }, cancellationToken);
        await _mediator.Send(new SendRabbitMqMessage { Message = new EventMessage { Type = TypeEvent.SpaceDeleteEvent, IdEntity = notification.IdSpace }, Queyename = "event" }, cancellationToken);
    }
}