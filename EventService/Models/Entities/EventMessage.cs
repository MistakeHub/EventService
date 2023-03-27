namespace EventService.Models.Entities;

/// <summary>
/// Модель Событийного сообщения
/// </summary>
public class EventMessage
{
    /// <summary>
    /// Тип события
    /// </summary>
    public TypeEvent Type { get; set; }
    /// <summary>
    /// Id элемента
    /// </summary>
    public Guid? IdEntity { get; set; }
}

/// <summary>
/// Типы событий
/// </summary>
public enum TypeEvent
{
    /// <summary>
    /// Событие удаления пространства
    /// </summary>
    SpaceDeleteEvent =1,
    /// <summary>
    /// Событие удаление изображения
    /// </summary>
    ImageDeleteEvent=2,
    /// <summary>
    /// Событие удаление мероприятия
    /// </summary>
    EventDeleteEvent=3
}