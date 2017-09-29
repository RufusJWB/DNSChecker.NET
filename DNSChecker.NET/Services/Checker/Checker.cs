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

        public bool CAACheck(string domain, string[] acceptableValues)
        {
            IEnumerable<CaaRecord> records;
            try
            {
                records = DNSWalker.WalkUp<DnsClient.Protocol.CaaRecord>(domain);
            }
            // Be conservative. If there is an error, return false
            catch (System.Exception ex)
            {
                return false;
            }

            // No CAA records set at all
            if (!records.Any())
            {
                return true;
            }

            // Check for a critical flag
            if (records.Any(x => (x.Flags & 128) != 0))
            {
                if (!CanProcessFlag(records.Where(x => (x.Flags & 128) != 0)))
                {
                    return false;
                }
            }

            IEnumerable<string> allowedIssuers;
            if (domain.StartsWith("*."))
            {
                allowedIssuers = records.Where(x => x.Tag == "issuewild").Select(x => x.Value);
            }
            else
            {
                allowedIssuers = records.Where(x => x.Tag == "issue").Select(x => x.Value);
            }
            foreach (string allowedIssuer in allowedIssuers)
            {
                string allowedIssuerDomain = string.Empty;
                if (allowedIssuer.Contains(";"))
                {
                    allowedIssuerDomain = allowedIssuer.Split(new string[] { ";" }, StringSplitOptions.None)[0];
                }
                else
                {
                    allowedIssuerDomain = allowedIssuer;
                }

                if (acceptableValues.Contains(allowedIssuerDomain))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CanProcessFlag(IEnumerable<CaaRecord> enumerable)
        {
            // todo: Check if critical flag can be processed
            return false;
        }

        public bool SOACheck(string domain, string[] acceptableValues)
        {
            IEnumerable<SoaRecord> records;
            try
            {
                records = DNSWalker.WalkUp<DnsClient.Protocol.SoaRecord>(domain);
            }
            // Be conservative. If there is an error, return false
            catch (System.Exception ex)
            {
                return false;
            }

            SoaRecord soaRecord;
            try
            {
                soaRecord = records.SoaRecords().Single();
            }
            // Be conservative. If there is an error, return false
            catch
            {
                return false;
            }

            string mName = soaRecord.MName.Value.ToLower();
            if (!mName.EndsWith("."))
            {
                // Every MName has to end with a '.'. If it doesn't, something is wrong here.
                return false;
            }

            foreach (string acceptableValue in acceptableValues.Select(x => $".{x.ToLower()}."))
            {
                if (mName.EndsWith(acceptableValue))
                {
                    // todo: Check TTL
                    return true;
                }
            }

            return false;
        }
    }
}