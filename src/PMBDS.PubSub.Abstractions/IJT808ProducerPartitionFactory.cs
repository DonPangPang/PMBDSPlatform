namespace PMBDS.PubSub.Abstractions
{
    /// <summary>
    /// JT808生产者分区工厂
    /// 分区策略:
    /// 1. 可以根据设备终端号进行分区
    /// 2. 可以根据MsgId(消息Id)+ 谁被终端号进行分区
    /// </summary>
    public interface IJT808ProducerPartitionFactory
    {
        int CreatePartition(string topicName, string msgId, string terminalNo);
    }
}