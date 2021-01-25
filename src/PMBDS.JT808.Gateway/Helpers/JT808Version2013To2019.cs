using System.ComponentModel.DataAnnotations;

namespace PMBDS.JT808.Gateway.Helpers
{
    public class JT808Version2013To2019
    {
        public static byte[] Do(byte[] version2013)
        {
            byte[] buffer = new byte[version2013.Length + 5];
            version2013.CopyTo(buffer, 0);
            new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 }.CopyTo(buffer, 5);
            return buffer;
        }
    }
}