using System;
using System.Threading.Tasks;
using AElf;
using ProjectCopyServer.Samples.Users.Dto;
using Shouldly;
using Xunit;

namespace ProjectCopyServer.Samples;

public partial class SampleTest
{

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
