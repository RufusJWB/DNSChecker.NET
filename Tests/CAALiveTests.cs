using Autofac;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Tests
{
    public class CAALiveTests
    {
        [Theory]
        [InlineData("empty.basic.caatestsuite.com")] // Tests proper processing of 0 issue ";"
        [InlineData("deny.basic.caatestsuite.com")] // Tests proper processing of 0 issue "caatestsuite.com"
        ////[InlineData("big.basic.caatestsuite.com")] // Tests proper processing of gigantic CAA record set (1001 records) containing 0 issue "caatestsuite.com"
        [InlineData("critical1.basic.caatestsuite.com")] // Tests proper processing of an unknown critical property: 128 caatestsuitedummyproperty "test"
        [InlineData("critical2.basic.caatestsuite.com")] // Tests proper processing of an unknown critical property when another flag is set: 130 caatestsuitedummyproperty "test"
        [InlineData("sub1.deny.basic.caatestsuite.com")] // Tests basic tree climbing, when CAA record exists at parent
        [InlineData("sub2.sub1.deny.basic.caatestsuite.com")] // Tests basic tree climbing, when CAA record exists at grandparent
        [InlineData("*.deny.basic.caatestsuite.com")] // Tests proper application of issue property to a wildcard FQDN
        [InlineData("*.deny-wild.basic.caatestsuite.com")] // Tests proper application of issuewild property to a wildcard FQDN
        [InlineData("cname-deny.basic.caatestsuite.com")] // 	Tests proper processing of a CNAME, when CAA record exists at CNAME target
        [InlineData("cname-cname-deny.basic.caatestsuite.com")] // Tests proper processing of a CNAME-to-a-CNAME, when CAA record exists at ultimate CNAME target
        [InlineData("sub1.cname-deny.basic.caatestsuite.com")] // Tests proper processing of a CNAME, when parent is a CNAME and CAA record exists at CNAME target
        [InlineData("deny.permit.basic.caatestsuite.com")] // Tests proper rejection when parent name (permit.basic.caatestsuite.com) contains a permissible CAA record set
        [InlineData("ipv6only.caatestsuite.com")] // Tests proper processing of CAA record at an IPv6-only authoritative name server
        [InlineData("xss.caatestsuite.com")] // Tests rejection when the issue property contains HTML and JavaScript. Makes sure there are no XSS vulnerabilities in the CA's website.
        public void TestGetCAARecord(string domain)
        {
            var container = DNSChecker.NET.Services.Helper.IoCBuilder.Container(new DnsClient.LookupClient());

            using (var scope = container.BeginLifetimeScope())
            {
                var walker = scope.Resolve<DNSChecker.NET.Services.DNSWalker.IDNSWalker>();
                var result = walker.WalkUp(domain, DnsClient.Protocol.ResourceRecordType.CAA);

                Assert.NotEmpty(result);
            }
        }

        [Theory]
        [InlineData("sir.cio.siemens.com")] // No CAA record set, but server inval
        [InlineData("www.siemens.com")] // No CAA record set
        [InlineData("expired.caatestsuite-dnssec.com")] // Tests rejection when there is no CAA record but the DNSSEC signatures are expired
        [InlineData("missing.caatestsuite-dnssec.com")] // Tests rejection when there is no CAA record but the DNSSEC signatures are missing
        //// [InlineData("blackhole.caatestsuite-dnssec.com")] // Tests rejection when there is a DNSSEC validation chain to a nonresponsive name server
        [InlineData("servfail.caatestsuite-dnssec.com")] // Tests rejection when there is a DNSSEC validation chain to a name server returning SERVFAIL
        [InlineData("refused.caatestsuite-dnssec.com")] // Tests rejection when there is a DNSSEC validation chain to a name server returning REFUSED
        public void TestGetException(string domain)
        {
            var container = DNSChecker.NET.Services.Helper.IoCBuilder.Container(new DnsClient.LookupClient());

            using (var scope = container.BeginLifetimeScope())
            {
                var walker = scope.Resolve<DNSChecker.NET.Services.DNSWalker.IDNSWalker>();
                Assert.Throws<InvalidOperationException>(() => walker.WalkUp(domain, DnsClient.Protocol.ResourceRecordType.CAA));
            }
        }

        [Theory]
        [InlineData("www.siemens.com")] // No CAA record set
        public void TestNoCAARecord(string domain)
        {
            var container = DNSChecker.NET.Services.Helper.IoCBuilder.Container(new DnsClient.LookupClient());

            using (var scope = container.BeginLifetimeScope())
            {
                var walker = scope.Resolve<DNSChecker.NET.Services.DNSWalker.IDNSWalker>();
                var result = walker.WalkUp(domain, DnsClient.Protocol.ResourceRecordType.CAA);

                Assert.Empty(result);
            }
        }
    }
}