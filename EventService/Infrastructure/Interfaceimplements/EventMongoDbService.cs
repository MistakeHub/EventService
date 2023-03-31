using EventService.Features.Event;
using EventService.Infrastructure.Interfaces;
using MongoDB.Driver;


namespace EventService.Infrastructure.InterfaceImplements;

/// <summary>
/// Сервис работы с Мероприятиями посредством MongoDb
/// </summary>
public class EventMongoDbService : IBaseEventService
{
    private readonly IMongoCollection<Event> _events;
    private const string CollectionName = "events";

    /// <summary>
    /// Конструктор с начальной инициализацией Базы данных
    /// </summary>

    public EventMongoDbService(MongoClient mongo)
    {
        _events = DataBaseInitialize(mongo).Result;

    }

    /// <summary>
    /// Метод создания мероприятия 
    /// </summary>

    public async Task<bool> CreateEvent(Event eventDefault)
    {
        try
        {
            await _events.InsertOneAsync(eventDefault);
            return _events.Find(v => v.Title == eventDefault.Title) != null;
        }
        catch (Exception)
        {
            return false;
        }

    }

    /// <summary>
    /// Метод Удаления мероприятия 
    /// </summary>
    public async Task<bool> DeleteEvent(Guid idevent)
    {
        try
        {
            return (await _events.DeleteOneAsync(v => v.Id == idevent)).DeletedCount > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Метод получения всех мероприятий
    /// </summary>

    public async Task<List<Event>> GetAllEvents()
    {
        try
        {
            return await _events.Find(v => true).ToListAsync();
        }
        catch (Exception)
        {
            return null!;
        }
    }

    /// <summary>
    /// Метод получения всех мероприятий на неделю
    /// </summary>

    public async Task<List<Event>> GetAllEventsForTheWeek()
    {
        try
        {
            var start = DateTime.UtcNow;

            var end = start.AddDays(7);

            return await _events.Find(v => v.Start > start || v.End <= end).ToListAsync();
        }
        catch (Exception)
        {
            return null!;
        }
    }

    /// <summary>
    ///Метод заполения бесплатных билетов на определённое мероприятие
    /// </summary>

    public async Task<bool> SetFreeTickets(Event eventDefault)
    {
        try
        {
            return (await _events.ReplaceOneAsync(v => v.Id == eventDefault.Id,eventDefault)).ModifiedCount > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Метод выдачи билета на мероприятие
    /// </summary>

    public async Task<bool> IssueTicket(Event eventDefault)
    {
        try
        {
            return (await _events.ReplaceOneAsync(v => eventDefault.Id == v.Id, eventDefault)).IsModifiedCountAvailable;
        }
        catch (Exception)
        {
            return false;
        }

    }

    /// <summary>
    /// Метод проверки наличия билета у пользователя на мероприятие
    /// </summary>

    public Task<bool> HaveATicket(Event eventDefault, Guid idowner)
    {
        try
        {
            return Task.FromResult(eventDefault.Tickets.Any(v => v.IdOwner == idowner));
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }
    }

    /// <summary>
    /// Метод обновления мероприятия
    /// </summary>

    public async Task<bool> UpdateEvent(Event updateEvent)
    {
        try
        {
            return (await _events.ReplaceOneAsync(v => v.Id == updateEvent.Id, updateEvent)).IsModifiedCountAvailable;
        }
        catch (Exception)
        {
            return false;
        }

    }

    // ReSharper disable once MemberCanBeMadeStatic.Local решарпер предлагает сделать метод статичным, но в этом нет никакого смысла
    // ReSharper disable once SuggestBaseTypeForParameter
    private async Task<IMongoCollection<Event>> DataBaseInitialize(MongoClient client)
    {

        // ReSharper disable once StringLiteralTypo
        var database = client.GetDatabase("eventdb");

        var collection = database.GetCollection<Event>(CollectionName);

        if ((await database.ListCollectionsAsync()).FirstOrDefault() != null) return collection;
        await database.CreateCollectionAsync(CollectionName);
        await collection.InsertOneAsync(new Event { Id = Guid.Parse("00000000-0000-0000-0000-000000000000"), Description = "TestDescription", Start = DateTime.UtcNow, End = DateTime.UtcNow.AddDays(1), Title = "TestTitle", IdImage = Guid.Parse("7febf16f-651b-43b0-a5e3-0da8da49e90d"), IdSpace = Guid.Parse("7febf16f-651b-43b0-a5e3-0da8da49e90d") });

        return collection;
    }

    /// <summary>
    /// Метод проверки наличия места у билета
    /// </summary>

    public Task<int?> CheckSeat(Event eventDefault, Guid idticket)
    {
        try
        {
            return Task.FromResult(eventDefault.Tickets.FirstOrDefault(v => v.Id == idticket)?.Seat);
        }
        catch (Exception)
        {
            return null!;
        }
    }

    /// <summary>
    /// Получить мероприятие
    /// </summary>
    /// <param name="idevent">id мероприятия</param>
    /// <returns></returns>
    public async Task<Event?> GetEventById(Guid idevent)
    {
        return await _events.Find(v => v.Id == idevent).FirstOrDefaultAsync();
    }
}