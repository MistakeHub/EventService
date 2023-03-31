using MediatR;

namespace EventService.Features.Event.Update.Notifications.RemoveSpace;

/// <summary>
/// Команда оповещения об удалении пространства 
/// </summary>
public class RemoveSpaceUpdateEvent : INotification
{
    /// <summary>
    /// Id пространства
    /// </summary>
    public Guid IdSpace { get; set; }

    /// <summary>
    /// Id мероприятия 
    /// </summary>
    public Guid IdEvent { get; set; }
}