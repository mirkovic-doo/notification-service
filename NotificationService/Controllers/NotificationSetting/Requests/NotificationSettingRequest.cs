namespace NotificationService.Controllers.NotificationSettings.Requests;

public record NotificationSettingRequest
{
    public required Guid Id { get; set; }
    public bool IsReservationRequestNotificationEnabled { get; set; } = true;
    public bool IsReservationResponseNotificationEnabled { get; set; } = true;
    public bool IsReservationCancellationNotificationEnabled { get; set; } = true;
    public bool IsReviewRecievedNotificationEnabled { get; set; } = true;
}
