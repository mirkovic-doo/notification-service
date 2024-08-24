using NotificationService.Domain.Base;

namespace NotificationService.Domain;

public class NotificationSetting : Entity, IEntity, IAuditedEntity
{
    public Guid UserId { get; set; }
    public bool IsReservationRequestNotificationEnabled { get; set; } = true;
    public bool IsReservationResponseNotificationEnabled { get; set; } = true;
    public bool IsReservationCancellationNotificationEnabled { get; set; } = true;
    public bool IsReviewRecievedNotificationEnabled { get; set; } = true;
}
