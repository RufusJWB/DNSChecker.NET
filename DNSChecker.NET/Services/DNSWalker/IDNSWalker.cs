using DnsClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNSChecker.NET.Services.DNSWalker
{
    public interface IDNSWalker
    {
        IEnumerable<DnsClient.Protocol.DnsResourceRecord> WalkUp(string query, DnsClient.Protocol.ResourceRecordType resourceRecordType);
    }
}