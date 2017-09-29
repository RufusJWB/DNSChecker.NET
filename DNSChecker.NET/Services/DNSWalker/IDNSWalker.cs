using System.Collections.Generic;

namespace DNSChecker.NET.Services.DNSWalker
{
    public interface IDNSWalker
    {
        IEnumerable<T> WalkUp<T>(string domain) where T : DnsClient.Protocol.DnsResourceRecord;
    }
}