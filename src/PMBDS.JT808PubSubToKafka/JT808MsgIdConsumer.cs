using System;
using System.Threading;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PMBDS.PubSub.Abstractions;

namespace PMBDS.JT808PubSubToKafka
{
    public class JT808MsgIdConsumer: IJT808Consumer
    {
        public CancellationTokenSource Cts => new CancellationTokenSource();
        public string TopicName => JT808PubSubConstants.JT808TopicName;

        private readonly ILogger<JT808MsgIdConsumer> logger;

        private ConsumerBuilder<string, byte[]> consumerBuilder;

        public JT808MsgIdConsumer(
            IOptions<ConsumerConfig> consumerConfigAccessor,
            ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<JT808MsgIdConsumer>();
            consumerBuilder = new ConsumerBuilder<string, byte[]>(null);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void OnMessage(Action<(string MsgId, byte[] data)> callback)
        {
            throw new NotImplementedException();
        }
        public void Subscribe()
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe()
        {
            throw new NotImplementedException();
        }
    }
}
