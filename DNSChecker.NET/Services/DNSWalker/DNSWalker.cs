using DnsClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DNSChecker.NET.Services.DNSWalker
{
    public class DNSWalker : IDNSWalker
    {
        static DNSWalker()
        {
            TLDList = new[] { "com", "net", "de" };
        }

        private DnsClient.ILookupClient LookupClient;

        public DNSWalker(DnsClient.ILookupClient lookupClient)
        {
            this.LookupClient = lookupClient ?? throw new ArgumentNullException(nameof(lookupClient));
        }

        private static string[] TLDList;

        public IEnumerable<DnsClient.Protocol.DnsResourceRecord> WalkUp(string query, DnsClient.Protocol.ResourceRecordType resourceRecordType)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            // Fix wildcard domains
            if (query.StartsWith("*."))
            {
                query = query.Substring(2);
            }

            var result = LookupClient.Query(query, (QueryType)resourceRecordType);
            if (result.HasError)
            {
                throw new InvalidOperationException(result.ErrorMessage);
            }

            var searchedRecord = result.Answers.OfRecordType(resourceRecordType);

            // Let CAA(X) be the record set returned in response to performing a CAA record query on
            // the label X, P(X) be the DNS label immediately above X in the DNS hierarchy, and A(X)
            // be the target of a CNAME or DNAME alias record chain specified at the label X.

            // If CAA(X) is not empty, R(X) = CAA(X), otherwise If A(X) is not null, and CAA(A(X)) is
            // not empty, then R(X) = CAA(A(X)), otherwise [done automatically]
            if (searchedRecord.Count() != 0)
            {
                return searchedRecord;
            }

            ////// If A(X) is not null, and CAA(A(X)) is not empty, then R(X) = CAA(A(X)), otherwise
            ////result = LookupClient.Query(query, QueryType.CNAME);
            ////if (result.HasError)
            ////{
            ////    throw new InvalidOperationException(result.ErrorMessage);
            ////}
            ////var cnameRecord = result.Answers.OfRecordType(DnsClient.Protocol.ResourceRecordType.CNAME).SingleOrDefault() as DnsClient.Protocol.CNameRecord;
            ////if (cnameRecord != null)
            ////{
            ////    WalkUp(cnameRecord.CanonicalName, resourceRecordType);
            ////}

            // If X is not a top - level domain, then R(X) = R(P(X)), otherwise
            if (!TLDList.Contains(query))
            {
                query = query.Substring(query.IndexOf(".") + 1);
                return WalkUp(query, resourceRecordType);
            }

            // R(X) is empty.
            return Enumerable.Empty<DnsClient.Protocol.DnsResourceRecord>();
        }
    }
}