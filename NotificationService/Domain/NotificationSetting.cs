using NotificationService.Domain.Base;

namespace NotificationService.Domain;

public class NotificationSetting : Entity, IEntity, IAuditedEntity
{
    public Guid UserId { get; set; }
    public bool IsReservationRequestNotificationEnabled { get; set; } = true;
    public bool IsReservationConfirmedNotificationEnabled { get; set; } = true;
    public bool IsReservationRejectedNotificationEnabled { get; set; }
    public bool IsReservationDeletedNotificationEnabled { get; set; }
    public bool IsReservationCancelledNotificationEnabled { get; set; } = true;
    public bool IsReviewRecievedNotificationEnabled { get; set; } = true;
}
