using ProjectCopyServer.Users.Eto;
using Volo.Abp.EventBus;

namespace ProjectCopyServer.Samples.Users.Eto;

[EventName("UserInformationEto")]
public class UserInformationEto : AbstractEto<UserGrainDto>
{
    public UserInformationEto(UserGrainDto data) : base(data)
    {
    }
    
}