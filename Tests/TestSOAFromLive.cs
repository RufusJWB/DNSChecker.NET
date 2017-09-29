using Autofac;
using Xunit;

namespace Tests
{
    public class TestSOAFromLive
    {
        [Theory]
        [InlineData("www.siemens.com", new object[] { "siemens.com", "siemens.net", "siemens.de" })]
        [InlineData("www.siemens.co.jp", new object[] { "siemens.com", "siemens.net", "siemens.de" })]
        [InlineData("critical1.basic.caatestsuite.com", new object[] { "caatestsuite.com" })]
        public void TestSOAOkay(string domain, string[] allowedSOAs)
        {
            var container = DNSChecker.NET.Services.Helper.IoCBuilder.Container(new DnsClient.LookupClient());

            using (var scope = container.BeginLifetimeScope())
            {
                var checker = scope.Resolve<DNSChecker.NET.Services.Checker.IChecker>();
                var result = checker.SOACheck(domain, allowedSOAs);

                Assert.True(result);
            }
        }

        [Theory]
        [InlineData("sir.cio.siemens.com", new object[] { "siemens.com", "siemens.net", "siemens.de" })] // Not existing, even if Siemens
        [InlineData("critical1.basic.caatestsuite.com", new object[] { "siemens.com", "siemens.net", "siemens.de" })] // Not Siemens at all
        [InlineData("missing.caatestsuite-dnssec.com", new object[] { "siemens.com", "siemens.net", "siemens.de" })] // Problem with DNSSec
        [InlineData("www.siemens.no", new object[] { "siemens.com", "siemens.net", "siemens.de" })] // Hosted at Telenor
        public void TestSOANotOkay(string domain, string[] allowedSOAs)
        {
            var container = DNSChecker.NET.Services.Helper.IoCBuilder.Container(new DnsClient.LookupClient());

            using (var scope = container.BeginLifetimeScope())
            {
                var checker = scope.Resolve<DNSChecker.NET.Services.Checker.IChecker>();
                var result = checker.SOACheck(domain, allowedSOAs);

                Assert.False(result);
            }
        }
    }
}