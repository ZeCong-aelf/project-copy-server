using Nest;

namespace ProjectCopyServer.Users.Index;

public class UserAddress
{
    [Keyword]public string ChainId { get; set; }
    [Keyword] public string Address { get; set; }
}