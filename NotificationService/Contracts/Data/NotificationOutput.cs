using NotificationService.Domain;

namespace NotificationService.Contracts.Data;

public record NotificationOutput
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public NotificationType Type { get; set; }
}
