using MediatR;

namespace EventService.Features.RabbitMq.SendMessage;

/// <summary>
/// Команда отправки сообщения в очередь сообщений
/// </summary>
public class SendRabbitMqMessage:IRequest
{
    /// <summary>
    /// Сообщение
    /// </summary>
#pragma warning disable CS8618
    public EventMessage Message { get; set; }
#pragma warning restore CS8618

    /// <summary>
    /// Название очереди
    /// </summary>
#pragma warning disable CS8618
    public string Queyename { get; set; }
#pragma warning restore CS8618
}