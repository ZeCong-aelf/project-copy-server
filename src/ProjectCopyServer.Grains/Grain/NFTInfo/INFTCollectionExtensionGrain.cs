using Orleans;

namespace ProjectCopyServer.Grains.Grain.NFTInfo;

public interface INFTCollectionExtensionGrain : IGrainWithStringKey
{
    Task<GrainResultDto<NftCollectionExtensionGrainDto>> CreateNftCollectionExtensionAsync(NftCollectionExtensionGrainDto input);


}