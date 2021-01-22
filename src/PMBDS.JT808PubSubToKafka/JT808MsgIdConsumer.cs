using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
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

        private readonly ILogger<JT808MsgIdConsumer> _logger;

        private IConsumer<string, byte[]> _consumer;

        public JT808MsgIdConsumer(
            IOptions<ConsumerConfig> consumerConfigAccessor,
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<JT808MsgIdConsumer>();
            _consumer = 
                new ConsumerBuilder<string, byte[]>(consumerConfigAccessor.Value).Build();
        }

        public void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
        }

        public void OnMessage(Action<(string MsgId, byte[] data)> callback)
        {
            Task.Run(() =>
            {
                while (!Cts.IsCancellationRequested)
                {
                    try
                    {
                        //如果不指定分区，根据kafka的机制会从多个分区中拉取数据
                        //如果指定分区，根据kafka的机制会从相应的分区中拉取数据
                        //consumer.Assign(new TopicPartition(TopicName,new Partition(0)));
                        var data = _consumer.Consume(Cts.Token);
                        if (_logger.IsEnabled(LogLevel.Debug))
                        {
                            _logger.LogDebug(
                                $"Topic: {data.Topic} Key: {data.Message.Key} Partition: {data.Partition} Offset: {data.Offset} Data:{string.Join("", data.Message.Value)} TopicPartitionOffset:{data.TopicPartitionOffset}");
                        }
                        callback((data.Message.Key, data.Message.Value));
                    }
                    catch (ConsumeException ex)
                    {
                        _logger.LogError(ex, TopicName);
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, TopicName);
                        Thread.Sleep(1000);
                    }
                }
            }, Cts.Token);
        }
        public void Subscribe()
        {
            _consumer.Subscribe(TopicName);
        }

        public void Unsubscribe()
        {
            _consumer.Unsubscribe();
        }
    }
}
