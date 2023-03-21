namespace EventService.Features.Event;

/// <summary>
/// Модель для обновления мероприятий
/// </summary>
public class UpdateEventModel
{


    /// <summary>
    /// Дата начала мероприятия
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// Дата окончания мероприятия
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Название мероприятия
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Описание мероприятия
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Id изображения мероприятия
    /// </summary>
    public string? IdImage { get; set; }

    /// <summary>
    /// Id пространства мероприятия
    /// </summary>
    public string? IdSpace { get; set; }
}