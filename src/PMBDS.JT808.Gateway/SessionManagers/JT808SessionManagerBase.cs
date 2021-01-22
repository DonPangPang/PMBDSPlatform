using System.Collections.Generic;
using System.Threading.Tasks;
using SuperSocket;

namespace PMBDS.JT808.Gateway.SessionManagers
{
    public class JT808SessionManagerBase: IJT808SessionManagerBase
    {
        public JT808SessionManagerBase(ISessionContainer sessionContainer)
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

        public Task<IEnumerable<IAppSession>> GetSessions()
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