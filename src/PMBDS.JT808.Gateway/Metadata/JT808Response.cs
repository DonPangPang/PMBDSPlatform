using JT808.Protocol;

namespace PMBDS.JT808.Gateway.Metadata
{
    public class JT808Response
    {
        public JT808Package Package { get; set; }

        public int MinBufferSize { get; set; }

        public JT808Response()
        {
        }

        public JT808Response(JT808Package package, int minBufferSize = 1024)
        {
            this.Package = package;
            this.MinBufferSize = minBufferSize;
        }
    }
}