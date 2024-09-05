namespace NotificationService.Controllers.NotificationSettings.Responses;

public record NotificationSettingResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public bool IsReservationRequestNotificationEnabled { get; set; }
    public bool IsReservationResponseNotificationEnabled { get; set; }
    public bool IsReservationCancellationNotificationEnabled { get; set; }
    public bool IsReviewRecievedNotificationEnabled { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
