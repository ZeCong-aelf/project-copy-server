using Orleans;

namespace ProjectCopyServer.Grains.Grain.NFTInfo;

public interface INftInfoExtensionGrain : IGrainWithStringKey
{
    Task<GrainResultDto<NftInfoExtensionGrainDto>> CreateNftInfoExtensionAsync(NftInfoExtensionGrainDto input);
}