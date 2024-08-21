using NotificationService.Domain.Base;

namespace NotificationService.Domain;

public class Notification : Entity, IEntity, IAuditedEntity
{
    public Notification() : base() { }

    public Notification(Guid senderId, Guid recieverId, Guid entityId, NotificationType type, bool isRead) : base()
    {
        SenderId = senderId;
        RecieverId = recieverId;
        EntityId = entityId;
        Type = type;
        IsRead = isRead;
    }

    public Guid SenderId { get; set; }
    public Guid RecieverId { get; set; }
    public Guid EntityId { get; set; }
    public NotificationType Type { get; set; }
    public bool IsRead { get; set; }
}
