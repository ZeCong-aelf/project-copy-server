using System.Collections.Generic;

namespace ProjectCopyServer.Samples.Http;

public class TransactionResultDto
{
    public string TransactionId { get; set; }
    public string Status { get; set; }
    public List<LogDto> Logs { get; set; }
    public string Bloom { get; set; }
    public long BlockNumber { get; set; }
    public TransactionDto Transaction { get; set; }
    public string BlockHash { get; set; }
    public string ReturnValue { get; set; }
}

public class TransactionDto
{
    public string From { get; set; }
    public string To { get; set; }
    public long RefBlockNumber { get; set; }
    public string RefBlockPrefix { get; set; }
    public string MethodName { get; set; }
    public string Params { get; set; }
    public string Signature { get; set; }
}

public class LogDto
{
    public string Address { get; set; }
    public string Name { get; set; }
    public List<string> Indexed { get; set; }
    public string NonIndexed { get; set; }
}