using AutoMapper;
using ProjectCopyServer.Users.Eto;
using ProjectCopyServer.Users.Index;


namespace ProjectCopyServer.EntityEventHandler.Core;

public class ProjectCopyServerEventHandlerAutoMapperProfile : Profile
{
    public ProjectCopyServerEventHandlerAutoMapperProfile()
    {
        CreateMap<UserInformationEto, UserIndex>();
    }
}