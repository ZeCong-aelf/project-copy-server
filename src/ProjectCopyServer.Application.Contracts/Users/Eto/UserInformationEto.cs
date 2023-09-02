using ProjectCopyServer.Users.Index;
using Volo.Abp.EventBus;

namespace ProjectCopyServer.Users.Eto;

[EventName("UserInformationEto")]
public class UserInformationEto : AbstractEto<UserIndex>
{
}