using System;
using System.Collections.Generic;
using System.Text;

namespace DNSChecker.NET.Services.Checker
{
    public interface IChecker
    {
        bool CAACheck(string domain, string[] acceptableValues);

        bool SOACheck(string domain, string[] acceptableValues);
    }
}