using NotificationService.Domain;

namespace NotificationService.Application.Services;

public interface INotificationService
{
    Task<Notification> GetByIdAsync(Guid id);
    Task<Notification> CreateAsync(Notification notification);
    Task DeleteAsync(Guid id);
    Task<Notification> MarkAsReadAsync(Guid id);
    Task<ICollection<Notification>> GetMyUnreadNotificationsAsync();
}
