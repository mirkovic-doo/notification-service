namespace NotificationService.Contracts.Data;

public record NotificationSettingInput
{
    public Guid Id { get; set; }
    public bool IsReservationRequestNotificationEnabled { get; set; }
    public bool IsReservationResponseNotificationEnabled { get; set; }
    public bool IsReservationCancellationNotificationEnabled { get; set; }
    public bool IsReviewRecievedNotificationEnabled { get; set; }
}
