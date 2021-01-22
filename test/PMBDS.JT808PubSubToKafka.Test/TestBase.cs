using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Confluent.Kafka;
using PMBDS.JT808PubSubToKafka;
using PMBDS.PubSub.Abstractions;

namespace PMBDS.JT808PubSubToKafka.Test
{
    public class TestBase
    {
        public IServiceProvider ServiceProvider { get; }

        public TestBase()
        {
            var serverHostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ILoggerFactory, LoggerFactory>();
                    services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
                    services.Configure<ProducerConfig>(hostContext.Configuration.GetSection("KafkaProducerConfig"));
                    services.Configure<ConsumerConfig>(hostContext.Configuration.GetSection("KafkaConsumerConfig"));
                    services.AddSingleton<JT808MsgIdProducer>();
                    services.AddSingleton<JT808MsgIdConsumer>();
                //services.AddSingleton<JT808SessionPublishingProducer>();
                //services.AddSingleton<JT808SessionPublishingConsumer>();
                //services.AddSingleton<JT808UnificationPushToWebSocketProducer>();
                //services.AddSingleton<JT808UnificationPushToWebSocketConsumer>();

                //ref:http://www.cnblogs.com/catcher1994/p/handle-multi-implementations-with-same-interface-in-dotnet-core.html
                services.AddSingleton(factory =>
                    {
                        Func<string, IJT808Producer> accesor = key =>
                        {
                            switch (key)
                            {
                                case JT808PubSubConstants.JT808TopicName:
                                    return factory.GetRequiredService<JT808MsgIdProducer>();
                                //case JT808PubSubConstants.UnificationPushToWebSocket:
                                //    return factory.GetRequiredService<JT808UnificationPushToWebSocketProducer>();
                                default:
                                    throw new ArgumentException($"Not Support key : {key}");
                            }
                        };
                        return accesor;
                    });
                    services.AddSingleton(factory =>
                    {
                        Func<string, IJT808Consumer> accesor = key =>
                        {
                            switch (key)
                            {
                                case JT808PubSubConstants.JT808TopicName:
                                    return factory.GetRequiredService<JT808MsgIdConsumer>();
                                //case JT808PubSubConstants.UnificationPushToWebSocket:
                                //    return factory.GetRequiredService<JT808UnificationPushToWebSocketConsumer>();
                                default:
                                    throw new ArgumentException($"Not Support key : {key}");
                            }
                        };
                        return accesor;
                    });

                //var serviceProvider = services.BuildServiceProvider();

                //var jT808MsgIdProducer = serviceProvider.GetRequiredService<JT808MsgIdProducer>();
                //var jT808MsgIdConsumer = serviceProvider.GetRequiredService<JT808MsgIdConsumer>();

                //var jT808SessionPublishingProducer = serviceProvider.GetRequiredService<JT808SessionPublishingProducer>();
                //var jT808SessionPublishingConsumer = serviceProvider.GetRequiredService<JT808SessionPublishingConsumer>();


                //var jT808UnificationPushToWebSocketProducer = serviceProvider.GetRequiredService<JT808UnificationPushToWebSocketProducer>();
                //var jT808UnificationPushToWebSocketConsumer = serviceProvider.GetRequiredService<JT808UnificationPushToWebSocketConsumer>();

                //jT808MsgIdConsumer.Subscribe();

                //jT808MsgIdConsumer.OnMessage(JT808MsgId.位置信息汇报.ToValueString(), (msg) =>
                //{

                //});

                //jT808MsgIdConsumer.OnMessage(JT808MsgId.终端注册应答.ToValueString(), (msg) =>
                //{

                //});

                //jT808SessionPublishingConsumer.OnMessage(JT808Constants.SessionOffline, (msg) =>
                //{

                //});

                //jT808SessionPublishingConsumer.Subscribe();

                //jT808UnificationPushToWebSocketConsumer.OnMessage(JT808PubSubConstants.UnificationPushToWebSocket, (msg) =>
                //{

                //});
                //jT808UnificationPushToWebSocketConsumer.Subscribe();

                //Thread.Sleep(3000);
                //var data = new byte[] { 1, 2, 3, 4, 5 };
                //var data1 = new byte[] { 1, 2, 3, 4, 5, 6 };

                //jT808MsgIdProducer.ProduceAsync(JT808MsgId.位置信息汇报.ToValueString(), data);
                //jT808MsgIdProducer.ProduceAsync(JT808MsgId.终端注册应答.ToValueString(), data1);
                //jT808SessionPublishingProducer.PublishAsync(JT808Constants.SessionOffline, "13812345678,13812345679");
                //jT808UnificationPushToWebSocketProducer.ProduceAsync(JT808PubSubConstants.UnificationPushToWebSocket, data);
            });
            var build = serverHostBuilder.Build();
            ServiceProvider = build.Services;
        }
    }
}