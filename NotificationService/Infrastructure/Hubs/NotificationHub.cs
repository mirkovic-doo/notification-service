using Microsoft.AspNetCore.SignalR;
using NotificationService.Application.Hubs;

namespace NotificationService.Infrastructure.Hubs;

public class NotificationHub : Hub<INotificationHub>
{
    private readonly NotificationDbContext dbContext;

    public NotificationHub(NotificationDbContext notificationDbContext)
    {
        dbContext = notificationDbContext;
    }

    public override async Task OnConnectedAsync()
    {
        var currentUserId = dbContext.CurrentUserId;

        if (currentUserId.HasValue)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, currentUserId.Value.ToString());
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var currentUserId = dbContext.CurrentUserId;

        if (currentUserId.HasValue)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, currentUserId.Value.ToString());
            await base.OnDisconnectedAsync(exception);
        }
    }
}
