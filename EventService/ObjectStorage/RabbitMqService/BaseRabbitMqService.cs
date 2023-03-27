
using System.Text.Json;
using RabbitMQ.Client;
using EventService.Models.Interfaces;

namespace EventService.ObjectStorage.RabbitMqService;

/// <summary>
/// Сервис для работы с RabbitMq
/// </summary>
public class BaseRabbitMqService:IBaseRabbitMqService, IDisposable
{
    private readonly IConnection _connection;
    /// <summary>
    /// конструктор
    /// </summary>
    public BaseRabbitMqService(string hostname, string username, string password, int port, string virtualhost) {
        var factory = new ConnectionFactory
        {
            HostName = hostname,
            UserName = username,
            Password = password,
            Port = port,
            VirtualHost = virtualhost

        };
    

        _connection = factory.CreateConnection(); 
           
    }
    /// <summary>
    /// Отправка сообщения
    /// </summary>
    /// <param name="message"></param>
    /// <param name="queyename"></param>
    /// <typeparam name="T"></typeparam>
    public void SendMessage<T>(T message, string queyename)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queyename,exclusive:false);
        var json = JsonSerializer.Serialize(message);
        var body=System.Text.Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange:"", queyename, body:body);
    }


    /// <inheritdoc />
    public void Dispose()
    {
        _connection.Dispose();
    }
}