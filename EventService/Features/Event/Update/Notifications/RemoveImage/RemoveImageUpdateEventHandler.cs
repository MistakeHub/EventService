using EventService.Features.RabbitMq;
using EventService.Features.RabbitMq.SendMessage;

using MediatR;

namespace EventService.Features.Event.Update.Notifications.RemoveImage;

/// <summary>
/// Оповещение об удалении изображения
/// </summary>
// ReSharper disable once UnusedMember.Global
public class RemoveImageUpdateEventHandler:INotificationHandler<RemoveImageUpdateEvent>
{
    private readonly IMediator _mediator;
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mediator"></param>

    public RemoveImageUpdateEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// Хэндлер 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    public async Task Handle(RemoveImageUpdateEvent notification, CancellationToken cancellationToken)
    {
      await  _mediator.Send(new SendRabbitMqMessage{Message = new EventMessage {Type = TypeEvent.ImageDeleteEvent, IdEntity = notification.IdImage }, Queyename = "event" }, cancellationToken)  ;
    }
}