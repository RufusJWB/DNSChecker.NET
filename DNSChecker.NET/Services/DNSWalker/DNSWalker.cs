using DnsClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DNSChecker.NET.Services.DNSWalker
{
    public class DNSWalker : IDNSWalker
    {
        static DNSWalker()
        {
            TLDList = File.ReadAllLines("tlds-alpha-by-domain.txt");
        }

        private DnsClient.ILookupClient LookupClient;

        public DNSWalker(DnsClient.ILookupClient lookupClient)
        {
            this.LookupClient = lookupClient ?? throw new ArgumentNullException(nameof(lookupClient));
        }

        private static string[] TLDList;

        public IEnumerable<T> WalkUp<T>(string domain) where T : DnsClient.Protocol.DnsResourceRecord
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                throw new ArgumentNullException(nameof(domain));
            }

            // Fix wildcard domains
            if (domain.StartsWith("*."))
            {
                domain = domain.Substring(2);
            }

            QueryType queryType = 0;

            switch (typeof(T).Name)
            {
                case nameof(DnsClient.Protocol.CaaRecord):
                    queryType = QueryType.CAA;
                    break;

                case nameof(DnsClient.Protocol.SoaRecord):
                    queryType = QueryType.SOA;
                    break;

                default:
                    throw new NotImplementedException($"{typeof(T).Name} is not supported");
            }

            var result = LookupClient.Query(domain, queryType);
            if (result.HasError)
            {
                throw new InvalidOperationException(result.ErrorMessage);
            }

            var searchedRecord = result.Answers.OfType<T>();

            // Let CAA(X) be the record set returned in response to performing a CAA record query on
            // the label X, P(X) be the DNS label immediately above X in the DNS hierarchy, and A(X)
            // be the target of a CNAME or DNAME alias record chain specified at the label X.

            // If CAA(X) is not empty, R(X) = CAA(X), otherwise If A(X) is not null, and CAA(A(X)) is
            // not empty, then R(X) = CAA(A(X)), otherwise [done automatically]
            if (searchedRecord.Count() != 0)
            {
                return searchedRecord;
            }

            // todo: Restrict amount of redirects
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
            if (!TLDList.Contains(domain.ToUpper()))
            {
                domain = domain.Substring(domain.IndexOf(".") + 1);
                return WalkUp<T>(domain);
            }

            // R(X) is empty.
            return Enumerable.Empty<T>();
        }
    }
}