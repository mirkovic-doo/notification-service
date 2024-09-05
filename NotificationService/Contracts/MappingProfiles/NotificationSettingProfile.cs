using AutoMapper;
using NotificationService.Contracts.Data;
using NotificationService.Controllers.NotificationSettings.Requests;
using NotificationService.Controllers.NotificationSettings.Responses;
using NotificationService.Domain;

namespace NotificationService.Contracts.MappingProfiles;

public class NotificationSettingProfile : Profile
{
    public NotificationSettingProfile()
    {
        CreateMap<NotificationSettingRequest, NotificationSettingInput>();
        CreateMap<NotificationSettingInput, NotificationSetting>();

        CreateMap<NotificationSetting, NotificationSettingResponse>();
    }
}
