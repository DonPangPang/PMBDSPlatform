namespace PMBDS.PubSub.Abstractions
{
    public interface IJT808Producer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgId">消息Id</param>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="data">data</param>
        void ProduceAsync(string msgId, string terminalNo, byte[] data);
    }
}