using AutoMapper;
using ProjectCopyServer.Grains.Grain.Users;
using ProjectCopyServer.Grains.State.Users;
using ProjectCopyServer.Samples.Users;
using ProjectCopyServer.Samples.Users.Dto;
using ProjectCopyServer.Samples.Users.Eto;
using ProjectCopyServer.Users.Eto;

namespace ProjectCopyServer.Grains;

public class SymbolMarketGrainsAutoMapperProfile : Profile
{
    public SymbolMarketGrainsAutoMapperProfile()
    {
        CreateMap<UserGrainDto, UserState>().ReverseMap();
        CreateMap<UserGrainDto, UserDto>().ReverseMap();
        CreateMap<UserGrainDto, UserInformationEto>().ReverseMap();
        
    }
}