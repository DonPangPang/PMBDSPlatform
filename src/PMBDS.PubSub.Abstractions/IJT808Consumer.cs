using System;
using System.Threading;

namespace PMBDS.PubSub.Abstractions
{
    public interface IJT808Consumer: IJT808PubSub, IDisposable
    {
        void OnMessage(Action<(string MsgId, byte[] data)> callback);
        CancellationTokenSource Cts { get; }
        void Subscribe();
        void Unsubscribe();
    }
}
