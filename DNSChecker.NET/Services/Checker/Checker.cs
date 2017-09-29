using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DnsClient.Protocol;

namespace DNSChecker.NET.Services.Checker
{
    public class Checker : IChecker
    {
        private Services.DNSWalker.IDNSWalker DNSWalker;

        public Checker(Services.DNSWalker.IDNSWalker dnsWalker)
        {
            this.DNSWalker = dnsWalker ?? throw new ArgumentNullException(nameof(dnsWalker));
        }

        public bool HasRecord(string domain, string[] acceptableValues, ResourceRecordType resourceRecordType)
        {
            IEnumerable<DnsResourceRecord> results;
            try
            {
                results = DNSWalker.WalkUp(domain, resourceRecordType);
            }
            // Be conservative - only issue if you know, that you can issue
            catch
            {
                return false;
            }

            return false;
        }
    }
}