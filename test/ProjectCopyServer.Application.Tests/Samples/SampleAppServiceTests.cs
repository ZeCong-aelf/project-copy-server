using System;
using System.Threading.Tasks;
using AElf;
using Microsoft.Extensions.DependencyInjection;
using ProjectCopyServer.Samples.Users;
using ProjectCopyServer.Samples.Users.Dto;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace ProjectCopyServer.Samples;

public class UserSampleAppServiceTests : ProjectCopyServerApplicationTestBase
{
    private readonly IUserAppService _userAppService;
    public UserSampleAppServiceTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _userAppService = GetRequiredService<IUserAppService>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        // services.AddSingleton(new XunitLogger<UserWriteProvider>(_output));
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
