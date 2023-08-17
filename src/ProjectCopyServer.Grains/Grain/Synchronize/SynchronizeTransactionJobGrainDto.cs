namespace ProjectCopyServer.Grains.Grain.Synchronize;

public class SynchronizeTransactionJobGrainDto
{
    public string Id { get; set; }
    public string FromChainId { get; set; }
    public string ToChainId { get; set; }
    public string Symbol { get; set; }
    public string TxHash { get; set; }
    public string ValidateTokenTxId { get; set; }
    public string ValidateTokenTx { get; set; }
    public long ValidateTokenHeight { get; set; }
    public string CrossChainCreateTokenTxId { get; set; }

    // Owner agent
    public string ValidateOwnerAgentTxId { get; set; }
    public string ValidateOwnerAgentTx { get; set; }
    public string CrossChainSyncOwnerAgentTxId { get; set; }
    public long ValidateOwnerAgentHeight { get; set; }

    // Issuer agent
    public string ValidateIssuerAgentTxId { get; set; }
    public string ValidateIssuerAgentTx { get; set; }
    public string CrossChainSyncIssuerAgentTxId { get; set; }
    public long ValidateIssuerAgentHeight { get; set; }

    public string Message { get; set; }
    public string Status { get; set; }
}

public class CreateSynchronizeTransactionJobGrainDto
{
    public string Id { get; set; }
    public string FromChainId { get; set; }
    public string ToChainId { get; set; }
    public string Symbol { get; set; }
    public string TxHash { get; set; }
    public string Status { get; set; }
}

public class GetTokenStatusBySymbolGrainDto
{
    public string ToChainId { get; set; }
    public string Symbol { get; set; }
}