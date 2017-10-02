using Autofac;

namespace DNSChecker.NET.Services.Helper
{
    public class IoCBuilder
    {
        public static IContainer Container(DnsClient.ILookupClient lookupClient)
        {
            lookupClient.ThrowDnsErrors = false;

            var builder = new ContainerBuilder();

            builder.RegisterInstance(lookupClient).As<DnsClient.ILookupClient>().SingleInstance();

            builder.RegisterType<DNSWalker.DNSWalker>().AsImplementedInterfaces();
            builder.RegisterType<Checker.Checker>().AsImplementedInterfaces();

            var Container = builder.Build();
            return Container;
        }
    }
}