using NotificationService.Domain;

namespace NotificationService.Controllers.Notification.Requests;

public record NotificationRequest
{
    public Guid SenderId { get; set; }
    public Guid RecieverId { get; set; }
    public Guid EntityId { get; set; }
    public NotificationType Type { get; set; }
    public bool IsRead { get; set; }
}
