using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckerUI
{
    public partial class MainWindow : Form
    {
        private readonly SynchronizationContext synchronizationContext;

        public MainWindow()
        {
            InitializeComponent();
            synchronizationContext = SynchronizationContext.Current;
        }

        private async void CheckDomains_Click(object sender, EventArgs e)
        {
            this.CheckResults.Text = "Starting check, will take some time, window will freeze";
            this.Enabled = false;

            //todo: Check entries by syntax
            var allDomainsToCheck = this.DomainsToCheck.Text.Replace(" ", "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var allowedSOARecords = this.SOARecords.Text.Replace(" ", "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var allowedCAARecords = this.CAARecords.Text.Replace(" ", "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            DnsClient.LookupClient lookUpClient = new DnsClient.LookupClient(DnsClient.NameServer.GooglePublicDns, DnsClient.NameServer.GooglePublicDnsIPv6, DnsClient.NameServer.GooglePublicDns2, DnsClient.NameServer.GooglePublicDns2IPv6);

            var container = DNSChecker.NET.Services.Helper.IoCBuilder.Container(lookUpClient);

            string resultString = string.Empty;
            using (var scope = container.BeginLifetimeScope())
            {
                var checker = scope.Resolve<DNSChecker.NET.Services.Checker.IChecker>();
                await Task.Run(() =>
                {
                    string totalMessage = string.Empty;
                    foreach (var domain in allDomainsToCheck)
                    {
                        var resultCAA = checker.CAACheck(domain, allowedCAARecords);
                        var resultSOA = checker.SOACheck(domain, allowedSOARecords);
                        totalMessage += $"{domain} SOA:{resultSOA} / CAA:{resultCAA} ==> {((resultCAA && resultSOA) ? "Can issue" : "DON'T ISSUE")}" + System.Environment.NewLine;
                        UpdateUI(totalMessage);
                    }
                });
            }

            this.Enabled = true;
        }

        public void UpdateUI(string value)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                this.CheckResults.Text = (string)o;
            }), value);
        }
    }
}