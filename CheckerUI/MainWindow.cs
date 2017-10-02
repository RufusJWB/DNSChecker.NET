using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckerUI
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckDomains_Click(object sender, EventArgs e)
        {
            //todo: Check entries by syntax
            var allDomainsToCheck = this.DomainsToCheck.Text.Replace(" ", "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var allowedSOARecords = this.SOARecords.Text.Replace(" ", "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var allowedCAARecords = this.CAARecords.Text.Replace(" ", "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            DnsClient.LookupClient lookUpClient = new DnsClient.LookupClient();

            var container = DNSChecker.NET.Services.Helper.IoCBuilder.Container(lookUpClient);

            string resultString = string.Empty;
            using (var scope = container.BeginLifetimeScope())
            {
                var checker = scope.Resolve<DNSChecker.NET.Services.Checker.IChecker>();

                foreach (var domain in allDomainsToCheck)
                {
                    var resultCAA = checker.CAACheck(domain, allowedCAARecords);
                    var resultSOA = checker.CAACheck(domain, allowedSOARecords);

                    resultString += $"{domain} SOA:{resultSOA} / CAA:{resultCAA}" + System.Environment.NewLine;
                }
            }

            this.CheckResults.Text = resultString;
        }
    }
}