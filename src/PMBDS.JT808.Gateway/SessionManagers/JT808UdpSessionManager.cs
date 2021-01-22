using System.Collections.Concurrent;
using System.Threading.Tasks;
using SuperSocket;

namespace PMBDS.JT808.Gateway.SessionManagers
{
    public class JT808UdpSessionManagerL: IJT808UdpSessionManager
    {
        public JT808UdpSessionManagerL(ISessionContainer sessionContainer)
        {

        }

        public Task<IAppSession> GetSessionByIdentify(string identify)
        {
            throw new System.NotImplementedException();
        }

        public Task<IAppSession> GetSessionBySessionId(string sessionId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ConcurrentDictionary<string, IAppSession>> GetSessions()
        {
            throw new System.NotImplementedException();
        }

        public void Heartbeat(string identify)
        {
            throw new System.NotImplementedException();
        }

        public void TryAdd(IAppSession session)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveSessionByIdentify(string identify)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveSessionBySessionId(string sessionId)
        {
            throw new System.NotImplementedException();
        }
    }
}