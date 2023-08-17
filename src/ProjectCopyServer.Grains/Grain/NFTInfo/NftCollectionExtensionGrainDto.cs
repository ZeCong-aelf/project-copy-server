namespace ProjectCopyServer.Grains.Grain.NFTInfo;

public class NftCollectionExtensionGrainDto
{
    public string Id    { get; set; }
    public string ChainId { get; set; }
    public string NFTSymbol { get; set; }

    public string LogoImage { get; set; }
        
    public string FeaturedImage { get; set; }
        
    public string Description { get; set; }

    public string TransactionId { get; set; }
    public string ExternalLink { get; set; } 

    public string OldFile { get; set; }
}