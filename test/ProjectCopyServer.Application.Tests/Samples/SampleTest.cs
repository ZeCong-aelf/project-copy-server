using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using ProjectCopyServer.Options;
using ProjectCopyServer.Samples.Users;
using Xunit.Abstractions;

namespace ProjectCopyServer.Samples;

public partial class SampleTest : ProjectCopyServerApplicationTestBase
{
    private readonly IUserAppService _userAppService;
    private readonly ISampleService _sampleService;

    public SampleTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _userAppService = GetRequiredService<IUserAppService>();
        _sampleService = GetRequiredService<ISampleService>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.AddSingleton(MockHttpFactory());
        services.AddSingleton(MockGraphQl());
        services.AddSingleton(MockChainOption());
        services.AddSingleton(MockDistributeLock());
    }


    private IOptionsMonitor<ChainOption> MockChainOption()
    {
        var option = new ChainOption
        {
            NodeApis = new Dictionary<string, string>
            {
                ["AELF"] = "http://127.0.0.1:9200/aelf",
                ["tDVV"] = "http://127.0.0.1:9200/tDVV",
                ["tDVW"] = "http://127.0.0.1:9200/tDVW",
            },
            AccountPrivateKey = new Dictionary<string, string>
            {
                ["User1"] = "0f10fd7f49be3f5f65c550f6cb2497e8a293c16e144b1256a492c351ef7a2a88"
            }
        };
        var mock = new Mock<IOptionsMonitor<ChainOption>>();
        mock.Setup(o => o.CurrentValue).Returns(option);
        return mock.Object;
    }
}