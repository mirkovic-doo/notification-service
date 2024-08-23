using Microsoft.EntityFrameworkCore;
using NotificationService.Application.Repositories;
using NotificationService.Domain;

namespace NotificationService.Infrastructure.Repositories;

public class NotificationRepository : BaseRepository<Notification>, IBaseRepository<Notification>, INotificationRepository
{
    public NotificationRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Notification> GetAsync(Guid id)
    {
        var notification = await base.GetAsync(id);

        if (notification.ReceiverId != dbContext.CurrentUserId)
        {
            throw new Exception($"Entity with {id} not found");
        }

        return notification;
    }

    public async Task<ICollection<Notification>> GetMyNotificationsAsync()
    {
        return await dbContext.Notifications.Where(n => n.ReceiverId == dbContext.CurrentUserId).ToListAsync();
    }

    public async Task<ICollection<Notification>> GetMyUnreadNotificationsAsync()
    {
        return await dbContext.Notifications.Where(n => !n.IsRead && n.ReceiverId == dbContext.CurrentUserId).ToListAsync();
    }

    public async Task MarkAllAsReadAsync()
    {
        await dbContext.Notifications.Where(n => n.ReceiverId == dbContext.CurrentUserId).ExecuteUpdateAsync((setters) => setters.SetProperty(n => n.IsRead, true));
    }
}
