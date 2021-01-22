using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace PMBDS.JT808PubSubToKafka.Test
{
    public class ConsumerTest: TestBase
    {
        [Fact]
        public void Test1()
        {
            var jT808MsgIdProducer = ServiceProvider.GetRequiredService<JT808MsgIdProducer>();

            jT808MsgIdProducer.ProduceAsync("512", "1234567890", new byte[] { 0, 1, 2, 3 });
            jT808MsgIdProducer.ProduceAsync("512", "4534567896", new byte[] { 0, 1, 2, 3, 4 });
            jT808MsgIdProducer.ProduceAsync("1024", "123456", new byte[] { 0, 1, 2, 3, 4 });
            jT808MsgIdProducer.ProduceAsync("1024", "1234567", new byte[] { 0, 1, 2, 3, 4 });
        }

        [Fact]
        public void Test2()
        {
            var jT808MsgIdConsumer = ServiceProvider.GetRequiredService<JT808MsgIdConsumer>();

            jT808MsgIdConsumer.OnMessage((msg) =>
            {

            });
            jT808MsgIdConsumer.Subscribe();
            Thread.Sleep(10000);

        }
    }
}
