using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotificationService.Application.Services;
using NotificationService.Configuration;
using NotificationService.Contracts.Data;
using NotificationService.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using IModel = RabbitMQ.Client.IModel;

namespace NotificationService.Infrastructure.Services;

public class NotificationReceiverService : INotificationReceiverService, IDisposable
{
    private readonly IMapper mapper;
    private readonly IModel channel;
    private readonly IConnection connection;
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly RabbitMQConfig config;

    public NotificationReceiverService(
        IMapper mapper,
        IServiceScopeFactory serviceScopeFactory,
        IOptions<RabbitMQConfig> options)
    {
        config = options.Value;
        this.mapper = mapper;
        this.serviceScopeFactory = serviceScopeFactory;

        var factory = new ConnectionFactory
        {
            HostName = config.HostName,
            UserName = config.UserName,
            Password = config.Password
        };

        connection = factory.CreateConnection();
        channel = connection.CreateModel();

        channel.QueueDeclare(queue: config.QueueName,
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
    }

    public void NotificationRecieveHandler()
    {
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var payload = JsonConvert.DeserializeObject<NotificationPayload>(message);

            if (payload != null)
            {
                await HandleNotificationAsync(payload);
            }
        };

        channel.BasicConsume(queue: config.QueueName,
                              autoAck: true,
                              consumer: consumer);
    }

    private async Task HandleNotificationAsync(NotificationPayload payload)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
        await notificationService.HandleNotificationAsync(mapper.Map<Notification>(payload));
    }

    public void Dispose()
    {
        channel?.Dispose();
        connection?.Dispose();
    }
}
