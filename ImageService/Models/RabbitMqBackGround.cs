
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using EventService.Features.RabbitMq;

namespace ImageService.Models;

public class RabbitMqBackGround : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queyename;

    public RabbitMqBackGround(string queyename, string hostname, string username, string password, int port, string virtualhost)
    {
        _queyename = queyename;
        var factory = new ConnectionFactory
        {
            HostName = hostname,
            UserName = username,
            Password = password,
            Port = port,
            VirtualHost = virtualhost

        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queyename, exclusive: false);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (stoppingToken.IsCancellationRequested) Dispose();



        var consumer = new EventingBasicConsumer(_channel);
        await  Task.Run(() =>
        {
            consumer.Received += (_, d) =>
            {

                var json = System.Text.Encoding.UTF8.GetString(d.Body.ToArray());
                var content = JsonSerializer.Deserialize<EventMessage>(json, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (content != null) Console.WriteLine($"{content.Type}, {content.IdEntity}");
                _channel.BasicAck(d.DeliveryTag, false);
            };

        }, stoppingToken);  
        _channel.BasicConsume(_queyename, true, consumer);
          
    }

    public override void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
        base.Dispose();
    }
}