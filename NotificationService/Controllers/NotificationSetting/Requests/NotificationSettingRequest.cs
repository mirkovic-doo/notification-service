namespace NotificationService.Controllers.NotificationSettings.Requests;

public record NotificationSettingRequest
{
    public required Guid Id { get; set; }
    public bool IsReservationRequestNotificationEnabled { get; set; } = true;
    public bool IsReservationConfirmedNotificationEnabled { get; set; } = true;
    public bool IsReservationRejectedNotificationEnabled { get; set; } = true;
    public bool IsReservationDeletedNotificationEnabled { get; set; } = true;
    public bool IsReservationCancelledNotificationEnabled { get; set; } = true;
    public bool IsReviewRecievedNotificationEnabled { get; set; } = true;
}
