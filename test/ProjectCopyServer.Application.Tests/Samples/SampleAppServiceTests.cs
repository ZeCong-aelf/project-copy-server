using System;
using System.Threading.Tasks;
using AElf;
using ProjectCopyServer.Samples.Users;
using ProjectCopyServer.Samples.Users.Dto;
using Shouldly;
using Xunit;

namespace ProjectCopyServer.Samples;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
[Collection(ProjectCopyServerTestConsts.CollectionDefinitionName)]
public class UserSampleAppServiceTests : ProjectCopyServerApplicationTestBase
{
    private readonly IUserAppService _userAppService;

    public UserSampleAppServiceTests()
    {
        _userAppService = GetRequiredService<IUserAppService>();
    }

    [Fact]
    public async Task AddQuery()
    {
        var userId = Guid.NewGuid();
        var user = new UserSourceInput()
        {
            Id = userId,
            Name = "Alice",
            AelfAddress = "5jdh2RSNkogQ4wj3BpzC8hdAgnK6tAuQmq454SNdUCqCzLruR",
            CaHash = HashHelper.ComputeFrom("").ToHex(),
        };
        
        var result = await _userAppService.AddUserAsync(user);
        result.Id.ShouldBe(userId);
        result.Name.ShouldBe("Alice");


        var query = await _userAppService.GetById(userId.ToString());
        query.ShouldNotBeNull();
        query.Id.ShouldBe(userId);
        result.Name.ShouldBe("Alice");

    }
}
