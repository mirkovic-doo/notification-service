using NotificationService.Domain;

namespace NotificationService.Controllers.Notification.Responses;

public record NotificationResponse
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public Guid RecieverId { get; set; }
    public Guid EntityId { get; set; }
    public NotificationType Type { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedById { get; set; }
    public Guid UpdatedById { get; set; }
}
