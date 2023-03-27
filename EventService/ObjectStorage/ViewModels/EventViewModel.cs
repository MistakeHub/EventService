namespace EventService.ObjectStorage.ViewModels;

/// <summary>
/// View model для мероприятия
/// </summary>
public class EventViewModel
{
    /// <summary>
    /// Id мероприятия
    /// </summary>
    public Guid? Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Дата начала мероприятия
    /// </summary>
    public DateTime? Start { get; set; }

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
    public Guid? IdImage { get; set; }

    /// <summary>
    /// Id пространства мероприятия
    /// </summary>
    public Guid? IdSpace { get; set; }


    // ReSharper disable once UnusedMember.Global  
    /// <summary>
    /// Флаг наличия мест у билетов
    /// </summary>
    public bool HaveTicketsSeats { get; set; }


    /// <summary>
    /// Проверка наличия свободных билетов
    /// </summary>
    public bool IsTicketsAvailable { get; set; }

    /// <summary>
    /// цена
    /// </summary>
    public decimal Price { get; set; }





}