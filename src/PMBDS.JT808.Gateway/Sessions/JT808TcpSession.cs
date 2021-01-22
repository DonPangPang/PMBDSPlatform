using SuperSocket.Server;

namespace PMBDS.JT808.Gateway.Sessions
{
    public class JT808TcpSession: AppSession
    {
        // Session身份认证标识
        public string Identify { get; set; }
    }
}