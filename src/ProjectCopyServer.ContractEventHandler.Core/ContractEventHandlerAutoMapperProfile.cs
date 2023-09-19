
using AutoMapper;
using ProjectCopyServer.Samples.Users.Eto;
using ProjectCopyServer.Users.Eto;
using ProjectCopyServer.Users.Index;

namespace ProjectCopyServer.ContractEventHandler
{
    public class ContractEventHandlerAutoMapperProfile : Profile
    {
        public ContractEventHandlerAutoMapperProfile()
        {
            CreateMap<UserInformationEto, UserIndex>();
        }
    }
}