using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Services;
using NotificationService.Contracts.Data;
using NotificationService.Controllers.NotificationSettings.Requests;
using NotificationService.Controllers.NotificationSettings.Responses;

namespace NotificationService.Controllers.NotificationSettings;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
public class NotificationSettingController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly INotificationSettingService notificationSettingService;

    public NotificationSettingController(IMapper mapper, INotificationSettingService notificationSettingService)
    {
        this.mapper = mapper;
        this.notificationSettingService = notificationSettingService;
    }

    [HttpGet("{userId}", Name = nameof(GetUserNotificationSetting))]
    [ProducesResponseType(typeof(NotificationSettingResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserNotificationSetting([FromRoute] Guid userId)
    {
        return Ok(mapper.Map<NotificationSettingResponse>(await notificationSettingService.GetOrCreateUserNotificationSettingAsync(userId)));
    }

    [HttpPut(Name = nameof(UpdateNotificationSetting))]
    [ProducesResponseType(typeof(NotificationSettingResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateNotificationSetting([FromBody] NotificationSettingRequest request)
    {
        var updatedSetting = await notificationSettingService.UpdateAsync(mapper.Map<NotificationSettingInput>(request));
        return Ok(mapper.Map<NotificationSettingResponse>(updatedSetting));
    }
}
