using AutoMapper;
using NotificationService.Controllers.Notification.Requests;
using NotificationService.Controllers.Notification.Responses;
using NotificationService.Domain;

namespace NotificationService.Contracts.MappingProfiles;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<NotificationRequest, Notification>();
        CreateMap<Notification, NotificationResponse>();
    }
}
