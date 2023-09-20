using AutoMapper;
using ProjectCopyServer.Grains.Grain.Users;
using ProjectCopyServer.Grains.State.Users;
using ProjectCopyServer.Samples.Users;
using ProjectCopyServer.Samples.Users.Dto;
using ProjectCopyServer.Samples.Users.Eto;

namespace ProjectCopyServer;

public class ProjectCopyServerApplicationAutoMapperProfile : Profile
{
    public ProjectCopyServerApplicationAutoMapperProfile()
    {
        CreateMap<UserSourceInput, UserGrainDto>().ReverseMap();
        // CreateMap<UserGrainDto, UserState>().ReverseMap();
        CreateMap<UserGrainDto, UserDto>().ReverseMap();
        CreateMap<UserGrainDto, UserInformationEto>().ReverseMap();
    }
}