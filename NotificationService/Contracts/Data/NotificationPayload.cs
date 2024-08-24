﻿using NotificationService.Domain;

namespace NotificationService.Contracts.Data;

public class NotificationPayload
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public Guid EntityId { get; set; }
    public NotificationType Type { get; set; }
}
