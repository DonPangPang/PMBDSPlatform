using PMBDS.PubSub.Abstractions;

namespace PMBDS.JT808PubSubToKafka.JT808Partitions
{
    public class JT808MsgIdProducerPartitionFactoryImpl: IJT808ProducerPartitionFactory
    {
        public int CreatePartition(string topicName, string msgId, string terminalNo)
        {
            var key1Byte1 = JT808HashAlgorithm.ComputeMd5(terminalNo);
            var p = JT808HashAlgorithm.Hash(key1Byte1, 2) % 8;
            return (int) p;
        }
    }
}