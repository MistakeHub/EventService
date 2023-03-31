using MediatR;

namespace EventService.Features.Event.Update.Notifications.RemoveImage;

/// <summary>
/// Команда оповещения об удалении изображения 
/// </summary>
public class RemoveImageUpdateEvent:INotification
{
    /// <summary>
    /// Id изображения
    /// </summary>
    public Guid? IdImage { get; set; }
}