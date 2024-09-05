using Microsoft.EntityFrameworkCore;
using NotificationService.Application.Repositories;
using NotificationService.Domain;

namespace NotificationService.Infrastructure.Repositories;

public class NotificationSettingRepository : BaseRepository<NotificationSetting>, INotificationSettingRepository
{
    public NotificationSettingRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<NotificationSetting?> GetMaybeByUserIdAsync(Guid userId)
    {
        return await dbContext.NotificationSettings.SingleOrDefaultAsync(ns => ns.UserId == userId);
    }
}
