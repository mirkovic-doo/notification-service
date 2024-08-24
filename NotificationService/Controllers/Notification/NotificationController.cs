using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Services;
using NotificationService.Controllers.Notification.Requests;
using NotificationService.Controllers.Notification.Responses;
using NotificationEntity = NotificationService.Domain.Notification;

namespace NotificationService.Controllers.Notification;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
public class NotificationController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly INotificationService notificationService;

    public NotificationController(
        IMapper mapper,
        INotificationService notificationService)
    {
        this.mapper = mapper;
        this.notificationService = notificationService;
    }

    [HttpGet("{id}", Name = nameof(GetById))]
    [ProducesResponseType(typeof(NotificationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var notification = await notificationService.GetByIdAsync(id);

        return Ok(mapper.Map<NotificationResponse>(notification));
    }

    [HttpGet("unread", Name = nameof(GetMyUnreadNotifications))]
    [ProducesResponseType(typeof(List<NotificationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyUnreadNotifications()
    {
        var notifications = await notificationService.GetMyUnreadNotificationsAsync();

        return Ok(mapper.Map<List<NotificationResponse>>(notifications));
    }

    [HttpGet("my", Name = nameof(GetMyNotifications))]
    [ProducesResponseType(typeof(List<NotificationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyNotifications()
    {
        var notifications = await notificationService.GetMyNotificationsAsync();

        return Ok(mapper.Map<List<NotificationResponse>>(notifications));
    }

    [HttpPost(Name = nameof(CreateNotification))]
    [ProducesResponseType(typeof(NotificationResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateNotification([FromBody] NotificationRequest request)
    {
        var notification = await notificationService.CreateAsync(mapper.Map<NotificationEntity>(request));

        return CreatedAtAction(nameof(CreateNotification), mapper.Map<NotificationResponse>(notification));
    }

    [HttpPut("mark/as/read/{id}", Name = nameof(MarkNotificationAsRead))]
    [ProducesResponseType(typeof(NotificationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> MarkNotificationAsRead([FromRoute] Guid id)
    {
        var notification = await notificationService.MarkAsReadAsync(id);
        return Ok(notification);
    }

    [HttpPut("mark/all/as/read", Name = nameof(MarkAllAsRead))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> MarkAllAsRead()
    {
        await notificationService.MarkAllAsReadAsync();
        return Ok();
    }

    [HttpDelete("{id}", Name = nameof(DeleteNotification))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteNotification([FromRoute] Guid id)
    {
        await notificationService.DeleteAsync(id);

        return Ok();
    }
}
