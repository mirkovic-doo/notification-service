namespace NotificationService.Domain.Base;

// <summary>
// This represents anonymously created audited entity.
// It is created by system.
// </summary>
public interface IAuditedEntity
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
