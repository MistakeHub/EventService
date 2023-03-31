namespace EventService.Infrastructure.Interfaces;

/// <summary>
/// Интерфейс для работы RabbitMq
/// </summary>
public interface IBaseRabbitMqService
{
    /// <summary>
    /// отправление сообщение в очередь
    /// </summary>
    /// <param name="message"></param>
    /// <param name="queyename"></param>
    /// <typeparam name="T"></typeparam>
    public Task SendMessage<T>(T message, string queyename);
}