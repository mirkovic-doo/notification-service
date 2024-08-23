using NotificationService.Domain;

namespace NotificationService.Application.Repositories;

public interface INotificationRepository : IBaseRepository<Notification>
{
    Task<ICollection<Notification>> GetMyUnreadNotificationsAsync();
}
