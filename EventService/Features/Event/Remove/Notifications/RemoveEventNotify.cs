using MediatR;

namespace EventService.Features.Event.Remove.Notifications;

/// <inheritdoc />
public class RemoveEventNotify:INotification
{
    /// <summary>
    /// Id мероприятия 
    /// </summary>
    public Guid IdEvent { get; set; }
}