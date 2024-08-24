using NotificationService.Domain;

namespace NotificationService.Application.Repositories;

public interface INotificationSettingRepository : IBaseRepository<NotificationSetting>
{
    Task<NotificationSetting?> GetMaybeByUserIdAsync(Guid userId);
}
