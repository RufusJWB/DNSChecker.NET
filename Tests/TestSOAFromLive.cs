using Autofac;
using Xunit;

namespace Tests
{
    public class TestSOAFromLive
    {
        [Theory]
        [InlineData("sir.cio.siemens.com", new object[] { "siemens.com", "siemens.net", "siemens.de" })]
        [InlineData("www.siemens.com", new object[] { "siemens.com", "siemens.net", "siemens.de" })]
        [InlineData("www.siemens.co.jp", new object[] { "siemens.com", "siemens.net", "siemens.de" })]
        [InlineData("critical1.basic.caatestsuite.com", new object[] { "caatestsuite.com" })]

        // based on https://github.com/quirins/caa-test
        [InlineData("db.crossbear.net", new object[] { "linode.com" })]
        [InlineData("db.crossbear.org", new object[] { "linode.com" })]
        [InlineData("db.measr.net", new object[] { "linode.com" })]
        [InlineData("db.perenaster.com", new object[] { "linode.com" })]

        public void TestSOAOkay(string domain, string[] allowedSOAs)
        {
            DnsClient.LookupClient lookUpClient = new DnsClient.LookupClient();
            lookUpClient.EnableAuditTrail = true;
           ////lookUpClient.ThrowDnsErrors = true;
            lookUpClient.UseCache = false;
            var container = DNSChecker.NET.Services.Helper.IoCBuilder.Container(lookUpClient);

            using (var scope = container.BeginLifetimeScope())
            {
                var checker = scope.Resolve<DNSChecker.NET.Services.Checker.IChecker>();
                var result = checker.SOACheck(domain, allowedSOAs);

                Assert.True(result);
            }
        }

        [Theory]
        [InlineData("critical1.basic.caatestsuite.com", new object[] { "siemens.com", "siemens.net", "siemens.de" })] // Not Siemens at all
        [InlineData("missing.caatestsuite-dnssec.com", new object[] { "siemens.com", "siemens.net", "siemens.de" })] // Problem with DNSSec
        [InlineData("www.siemens.no", new object[] { "siemens.com", "siemens.net", "siemens.de" })] // Hosted at Telenor

        // based on https://github.com/quirins/caa-test
        [InlineData("db.crossbear.net", new object[] { "siemens.com", "siemens.net", "siemens.de" })]
        [InlineData("db.crossbear.org", new object[] { "siemens.com", "siemens.net", "siemens.de" })]
        [InlineData("db.measr.net", new object[] { "siemens.com", "siemens.net", "siemens.de" })]
        [InlineData("db.perenaster.com", new object[] { "siemens.com", "siemens.net", "siemens.de" })]

        public void TestSOANotOkay(string domain, string[] allowedSOAs)
        {
            DnsClient.LookupClient lookUpClient = new DnsClient.LookupClient();
            lookUpClient.EnableAuditTrail = true;
           ////lookUpClient.ThrowDnsErrors = true;
            lookUpClient.UseCache = false;
            var container = DNSChecker.NET.Services.Helper.IoCBuilder.Container(lookUpClient);

            using (var scope = container.BeginLifetimeScope())
            {
                var checker = scope.Resolve<DNSChecker.NET.Services.Checker.IChecker>();
                var result = checker.SOACheck(domain, allowedSOAs);

                Assert.False(result);
            }
        }
    }
}