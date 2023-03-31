using EventService.Features.Event;

namespace EventService.Infrastructure.Interfaces;

/// <summary>
/// Интерфейс для сервисов мероприятий
/// </summary>
public interface IBaseEventService
{
    /// <summary>
    /// Метод создания мероприятия
    /// </summary>
    /// <param name="eventDefault">Мероприятие</param>
    /// <returns>Результат добавления</returns>
    public Task<bool> CreateEvent(Event eventDefault);

    /// <summary>
    /// Метод обновления мероприятия
    /// </summary>
    /// <param name="updateEvent">Мероприятие</param>

    public Task<bool> UpdateEvent(Event updateEvent);

    /// <summary>
    /// удаление мероприятия
    /// </summary>
    /// <param name="idevent">мероприятие</param>
    /// <returns>результат удаления</returns>
    public Task<bool> DeleteEvent(Guid idevent);

    /// <summary>
    /// Получение всех мероприятий
    /// </summary>
    /// <returns>Список мероприятий</returns>
    public Task<List<Event>> GetAllEvents();

    /// <summary>
    /// Получения мероприятий на неделю
    /// </summary>
    /// <returns>Список мероприятий</returns>
    public Task<List<Event>> GetAllEventsForTheWeek();

    /// <summary>
    /// Заполение мероприятия бесплатными билетами
    /// </summary>
    /// <param name="eventDefault">мероприятие</param>
    /// <returns>Результат заполнения</returns>
    public Task<bool> SetFreeTickets(Event eventDefault);

    /// <summary>
    /// Выдача билета на мероприятие
    /// </summary>
    /// <param name="eventDefault">мероприятия</param>
    /// <returns>Результат</returns>
    public Task<bool> IssueTicket(Event eventDefault);

    /// <summary>
    /// Проверка наличия билета на мероприятие
    /// </summary>
    /// <param name="eventDefault">мероприятие</param>
    /// <param name="idowner">пользователь</param>
    /// <returns>наличие билета</returns>
    public Task<bool> HaveATicket(Event eventDefault, Guid idowner);

    /// <summary>
    ///  Проверка наличия места у билета
    /// </summary>
    /// <param name="eventDefault">мероприятие</param>
    /// <param name="idticket">билет</param>
    /// <returns>место либо null</returns>
    public Task<int?> CheckSeat(Event eventDefault, Guid idticket);

    /// <summary>
    /// Получения мероприятия по Id
    /// </summary>
    /// <param name="idevent"></param>

    public Task<Event?> GetEventById(Guid idevent);
}