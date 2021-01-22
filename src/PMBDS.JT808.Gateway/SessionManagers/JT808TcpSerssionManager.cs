using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSocket;

namespace PMBDS.JT808.Gateway.SessionManagers
{
    public class JT808TcpSerssionManager: IJT808TcpSessionManager
    {
        private readonly ISessionContainer _sessionContainer;

        public JT808TcpSerssionManager(ISessionContainer sessionContainer)
        {
            _sessionContainer = sessionContainer;
        }

        public int Count => _sessionContainer.GetSessionCount();

        public Task<IAppSession> GetSessionByIdentify(string identify)
        {
            return Task.Run(() =>
            {
                return _sessionContainer.GetSessions().FirstOrDefault(x => x["Identify"].Equals(identify));
            });
        }

        public Task<IAppSession> GetSessionBySessionId(string sessionId)
        {
            return Task.Run(() => _sessionContainer.GetSessionByID(sessionId));
        }

        public Task<IEnumerable<IAppSession>> GetSessions()
        {
            return Task.Run(()=> _sessionContainer.GetSessions());
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