using AutoMapper;
using ProjectCopyServer.Grains.Grain.Users;
using ProjectCopyServer.Users.Dto;

namespace ProjectCopyServer;

public class ProjectCopyServerApplicationAutoMapperProfile : Profile
{
    public ProjectCopyServerApplicationAutoMapperProfile()
    {
        CreateMap<UserSourceInput, UserGrainDto>()
            .ForMember(opt => opt.Id, d => d.MapFrom(src => src.UserId));
    }
}
