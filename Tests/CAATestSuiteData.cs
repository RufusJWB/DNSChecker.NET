using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [Serializable]
    public class CAATestSuiteData : ISerializable
    {
        public string Domain { get; private set; }
        private readonly string label;

        public CAATestSuiteData(string domain, string label)
        {
            this.Domain = domain;
            this.label = label;
        }

        public override string ToString()
        {
            return this.label;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Domain", this.Domain, typeof(string));
            info.AddValue("Label", this.label, typeof(string));
        }
    }
}