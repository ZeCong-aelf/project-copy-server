using AutoMapper;
using ProjectCopyServer.Samples.Users;
using ProjectCopyServer.Samples.Users.Dto;
using ProjectCopyServer.Samples.Users.Eto;
using ProjectCopyServer.Users.Index;

namespace ProjectCopyServer;

public class ProjectCopyServerApplicationAutoMapperProfile : Profile
{
    public ProjectCopyServerApplicationAutoMapperProfile()
    {
        CreateMap<UserSourceInput, UserGrainDto>().ReverseMap();
        CreateMap<UserGrainDto, UserDto>().ReverseMap();
        CreateMap<UserGrainDto, UserInformationEto>().ReverseMap();
        CreateMap<UserIndex, UserDto>().ReverseMap();
    }
}