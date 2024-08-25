using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Application.Repositories;
using NotificationService.Application.Services;
using NotificationService.Contracts.Constants;
using NotificationService.Controllers.Notification.Responses;
using NotificationService.Domain;
using NotificationService.Infrastructure.Hubs;

namespace NotificationService.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly IMapper mapper;
    private readonly IHubContext<NotificationHub> notificationHubContext;
    private readonly INotificationRepository notificationRepository;
    private readonly INotificationSettingService notificationSettingService;

    public NotificationService(
        IMapper mapper,
        IHubContext<NotificationHub> hubContext,
        INotificationRepository notificationRepository,
        INotificationSettingService notificationSettingService)
    {
        this.mapper = mapper;
        this.notificationHubContext = hubContext;
        this.notificationRepository = notificationRepository;
        this.notificationSettingService = notificationSettingService;
    }

    public async Task<Notification> CreateAsync(Notification notification)
    {
        return await notificationRepository.AddAsync(notification);
    }

    public async Task DeleteAsync(Guid id)
    {
        var notification = await notificationRepository.GetAsync(id);

        notificationRepository.Delete(notification);
    }

    public async Task<Notification> GetByIdAsync(Guid id)
    {
        return await notificationRepository.GetAsync(id);
    }

    public async Task<ICollection<Notification>> GetMyNotificationsAsync()
    {
        return await notificationRepository.GetMyNotificationsAsync();
    }

    public async Task<ICollection<Notification>> GetMyUnreadNotificationsAsync()
    {
        return await notificationRepository.GetMyUnreadNotificationsAsync();
    }

    public async Task HandleNotificationAsync(Notification notification)
    {
        if (await notificationSettingService.IsNotificationTypeEnabledAsync(notification.ReceiverId, notification.Type))
        {
            var addedNotification = await notificationRepository.AddAsync(notification);

            await notificationHubContext.Clients.Group(notification.ReceiverId.ToString()).SendAsync(General.SendNotificationMethodName, mapper.Map<NotificationResponse>(addedNotification));
        }
    }

    public async Task MarkAllAsReadAsync()
    {
        await notificationRepository.MarkAllAsReadAsync();
    }

    public async Task<Notification> MarkAsReadAsync(Guid id)
    {
        var notification = await notificationRepository.GetAsync(id);

        notification.IsRead = true;

        return notificationRepository.Update(notification);
    }
}
