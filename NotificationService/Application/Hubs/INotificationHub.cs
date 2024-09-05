using NotificationService.Contracts.Data;

namespace NotificationService.Application.Hubs;

public interface INotificationHub
{
    Task SendNotification(NotificationOutput notificationOutput);
}
