using EventService.Infrastructure.Interfaces;
using MediatR;

namespace EventService.Features.RabbitMq.SendMessage;

/// <inheritdoc />
// ReSharper disable once UnusedMember.Global
public class SendRabbitMqMessageRequestHandler:IRequestHandler<SendRabbitMqMessage>
{
    private readonly IBaseRabbitMqService _baseRabbitMqService;
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="baseRabbitMqService"></param>
    public SendRabbitMqMessageRequestHandler(IBaseRabbitMqService baseRabbitMqService) { _baseRabbitMqService=baseRabbitMqService; }

    /// <inheritdoc />
    public async Task Handle(SendRabbitMqMessage request, CancellationToken cancellationToken)
    {
        await _baseRabbitMqService.SendMessage(request.Message, request.Queyename);
    }
}