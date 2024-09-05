namespace NotificationService.Domain.Base;

public class Entity : IEntity, IAuditedEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
