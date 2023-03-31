
using EventService.Features.RabbitMq;
using EventService.Features.RabbitMq.SendMessage;
using MediatR;

namespace EventService.Features.Event.Remove.Notifications;

// ReSharper disable once UnusedMember.Global
/// <inheritdoc />
public class RemoveEventNotifyHandler:INotificationHandler<RemoveEventNotify>
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mediator"></param>

    public RemoveEventNotifyHandler(IMediator mediator)
    {
        _mediator = mediator;


    }
    /// <summary>
    /// Хэндлер 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    public async Task Handle(RemoveEventNotify notification, CancellationToken cancellationToken)
    {
   
        await _mediator.Send(new SendRabbitMqMessage { Message = new EventMessage { Type = TypeEvent.EventDeleteEvent, IdEntity = notification.IdEvent }, Queyename = "event" }, cancellationToken);
    }
}