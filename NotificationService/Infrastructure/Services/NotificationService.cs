using AutoMapper;
using NotificationService.Application.Repositories;
using NotificationService.Application.Services;
using NotificationService.Domain;

namespace NotificationService.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly IMapper mapper;
    private readonly INotificationRepository notificationRepository;

    public NotificationService(IMapper mapper, INotificationRepository notificationRepository)
    {
        this.mapper = mapper;
        this.notificationRepository = notificationRepository;
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
        await notificationRepository.AddAsync(notification);

        // publish event to SignalR
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
