using AutoMapper;
using NotificationService.Contracts.Data;
using NotificationService.Controllers.Notification.Requests;
using NotificationService.Controllers.Notification.Responses;
using NotificationService.Domain;

namespace NotificationService.Contracts.MappingProfiles;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<NotificationRequest, Notification>();

        CreateMap<NotificationPayload, Notification>()
            .ForMember(d => d.IsRead, opt => opt.MapFrom(o => false));

        CreateMap<Notification, NotificationResponse>();
        CreateMap<Notification, NotificationOutput>();
    }
}
