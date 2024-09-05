namespace NotificationService.Contracts.Data;

public record NotificationSettingInput
{
    public Guid Id { get; set; }
    public bool IsReservationRequestNotificationEnabled { get; set; }
    public bool IsReservationConfirmedNotificationEnabled { get; set; }
    public bool IsReservationRejectedNotificationEnabled { get; set; }
    public bool IsReservationDeletedNotificationEnabled { get; set; }
    public bool IsReservationCancelledNotificationEnabled { get; set; }
    public bool IsReviewRecievedNotificationEnabled { get; set; }
}
