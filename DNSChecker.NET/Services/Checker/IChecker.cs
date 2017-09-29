using System;
using System.Collections.Generic;
using System.Text;

namespace DNSChecker.NET.Services.Checker
{
    public interface IChecker
    {
        bool HasRecord(string domain, string[] acceptableValues, DnsClient.Protocol.ResourceRecordType resourceRecordType);
    }
}