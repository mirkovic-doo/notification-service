using NotificationService.Contracts.Data;
using NotificationService.Domain;

namespace NotificationService.Application.Services;

public interface INotificationSettingService
{
    Task<NotificationSetting> GetOrCreateUserNotificationSettingAsync(Guid userId);
    Task<NotificationSetting> UpdateAsync(NotificationSettingInput notificationSetting);
    Task<bool> IsNotificationTypeEnabledAsync(Guid userId, NotificationType type);
}
