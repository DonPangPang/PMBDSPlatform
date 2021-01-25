using System;
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
            //IAppSession jt808TcpSession;
            //if (string.IsNullOrEmpty(identify) || !this.SessionIdDict.TryGetValue(identify, out jt808TcpSession))
            //    return;
            //jt808TcpSession.LastActiveTime = DateTime.Now;
            //this.SessionIdDict.TryUpdate(identify, jt808TcpSession, jt808TcpSession);
        }

        public void TryAdd(IAppSession session)
        {
            session["identify"] = "12345678910";
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