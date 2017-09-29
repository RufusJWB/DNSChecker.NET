using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNSChecker.NET.Services.Helper
{
    public class IoCBuilder
    {
        public static IContainer Container(DnsClient.ILookupClient lookupClient)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(lookupClient).As<DnsClient.ILookupClient>().SingleInstance();

            builder.RegisterType<DNSWalker.DNSWalker>().AsImplementedInterfaces();

            var Container = builder.Build();
            return Container;
        }
    }
}