using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperSocket;

namespace PMBDS.JT808.Gateway.SessionManagers
{
    public interface IJT808SessionManagerBase
    {
        //ConcurrentDictionary<string, IAppSession> sessions = new ConcurrentDictionary<string, IAppSession>();
        Task<IAppSession> GetSessionByIdentify(string identify);
        Task<IAppSession> GetSessionBySessionId(string sessionId);
        Task<IEnumerable<IAppSession>> GetSessions();

        void Heartbeat(string identify);
        void TryAdd(IAppSession session);

        void RemoveSessionByIdentify(string identify);
        void RemoveSessionBySessionId(string sessionId);
    }
}