namespace NotificationService.Controllers.NotificationSettings.Responses;

public record NotificationSettingResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public bool IsReservationRequestNotificationEnabled { get; set; }
    public bool IsReservationConfirmedNotificationEnabled { get; set; }
    public bool IsReservationRejectedNotificationEnabled { get; set; }
    public bool IsReservationDeletedNotificationEnabled { get; set; }
    public bool IsReservationCancelledNotificationEnabled { get; set; }
    public bool IsReviewRecievedNotificationEnabled { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
