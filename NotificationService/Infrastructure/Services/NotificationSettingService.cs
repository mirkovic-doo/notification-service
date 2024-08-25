using AutoMapper;
using NotificationService.Application.Repositories;
using NotificationService.Application.Services;
using NotificationService.Contracts.Data;
using NotificationService.Domain;

namespace NotificationService.Infrastructure.Services;

public class NotificationSettingService : INotificationSettingService
{
    private readonly IMapper mapper;
    private readonly INotificationSettingRepository notificationSettingRepository;

    public NotificationSettingService(IMapper mapper, INotificationSettingRepository notificationSettingRepository)
    {
        this.mapper = mapper;
        this.notificationSettingRepository = notificationSettingRepository;
    }

    public async Task<NotificationSetting> GetOrCreateUserNotificationSettingAsync(Guid userId)
    {
        var maybeNotificationSetting = await notificationSettingRepository.GetMaybeByUserIdAsync(userId);

        if (maybeNotificationSetting == null)
        {
            maybeNotificationSetting = new NotificationSetting()
            {
                UserId = userId,
            };

            maybeNotificationSetting = await notificationSettingRepository.AddAsync(maybeNotificationSetting);
        }

        return maybeNotificationSetting;
    }

    public async Task<bool> IsNotificationTypeEnabledAsync(Guid userId, NotificationType type)
    {
        var userNotificationSetting = await GetOrCreateUserNotificationSettingAsync(userId);

        return type switch
        {
            NotificationType.ReservationCancellation => userNotificationSetting.IsReservationCancellationNotificationEnabled,
            NotificationType.ReservationResponse => userNotificationSetting.IsReservationResponseNotificationEnabled,
            NotificationType.ReservationRequest => userNotificationSetting.IsReservationRequestNotificationEnabled,
            NotificationType.ReviewRecieved => userNotificationSetting.IsReviewRecievedNotificationEnabled,
            _ => true,
        };
    }

    public async Task<NotificationSetting> UpdateAsync(NotificationSettingInput input)
    {
        var notificationSetting = await notificationSettingRepository.GetAsync(input.Id);

        mapper.Map(input, notificationSetting);

        return notificationSettingRepository.Update(notificationSetting);
    }
}
