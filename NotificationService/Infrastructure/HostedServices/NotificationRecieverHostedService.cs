using NotificationService.Application.Services;

namespace NotificationService.Infrastructure.HostedServices;

public class NotificationReceiverListenerHostedService : IHostedService
{
    private readonly INotificationReceiverService notificationReceiver;

    public NotificationReceiverListenerHostedService(INotificationReceiverService notificationReceiver)
    {
        this.notificationReceiver = notificationReceiver;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        notificationReceiver.NotificationRecieveHandler();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
