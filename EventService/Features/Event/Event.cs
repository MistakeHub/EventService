using MongoDB.Bson.Serialization.Attributes;

namespace EventService.Features.Event;

/// <summary>
/// Класс Мероприятия
/// </summary>
public class Event
{

    /// <summary>
    /// Id мероприятия
    /// </summary>
    [BsonId]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Дата начала мероприятия
    /// </summary>
    public DateTime? Start { get; set; }

    /// <summary>
    /// Дата окончания мероприятия
    /// </summary>
    public DateTime? End { get; set; }

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
    public Guid IdSpace { get; set; }

    /// <summary>
    /// Билеты на мероприятие
    /// </summary>
    public List<Ticket.Ticket> Tickets { get; set; } = new();

    // ReSharper disable once UnusedMember.Global  
    /// <summary>
    /// Флаг наличия мест у билетов
    /// </summary>
    public bool HaveTicketsSeats
    {
        get { return Tickets.Any(v => v.Seat != null); }
    }

    /// <summary>
    /// Проверка наличия свободных билетов
    /// </summary>
    public bool IsTicketsAvailable
    {
        get { return Tickets.Any(v => v.IdOwner != null); }
    }

    /// <summary>
    /// Цена за билет
    /// </summary>
    public decimal Price { get; set; } = 0;
}