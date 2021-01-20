using JT808.Protocol;

namespace PMBDS.JT808.Gateway.Metadata
{
    public class JT808Request
    {
        public JT808HeaderPackage Package { get; }

        public byte[] OriginalPackage { get; }

        public JT808Request(JT808HeaderPackage package, byte[] originalPackage)
        {
            this.Package = package;
            this.OriginalPackage = originalPackage;
        }
    }
}