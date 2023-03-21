namespace EventService.Features.Event;

/// <summary>
/// Модель данных для создания мероприятий
/// </summary>
public class RequestCreateEventModel
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
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание мероприятия
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Id изображения мероприятия
    /// </summary>
    public string IdImage { get; set; } = null!;

    /// <summary>
    /// Id пространства мероприятия
    /// </summary>
    public string IdSpace { get; set; } = null!;
}