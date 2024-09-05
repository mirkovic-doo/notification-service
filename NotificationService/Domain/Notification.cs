using NotificationService.Domain.Base;

namespace NotificationService.Domain;

public class Notification : Entity, IEntity, IAuditedEntity
{
    public Notification() : base() { }

    public Notification(Guid senderId, Guid receiverId, Guid entityId, NotificationType type, bool isRead) : base()
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        EntityId = entityId;
        Type = type;
        IsRead = isRead;
    }

    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public Guid EntityId { get; set; }
    public NotificationType Type { get; set; }
    public bool IsRead { get; set; }
}
