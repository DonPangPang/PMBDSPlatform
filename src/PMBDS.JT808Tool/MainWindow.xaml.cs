using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Confluent.Kafka;
using JT808.Protocol;
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using PMBDS.JT808PubSubToKafka;
using SuperSocket;
using SuperSocket.SessionContainer;

namespace PMBDS.JT808Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // 用于存储车辆注册情况
        // 每次重启车辆情况会被清空, 鉴权时发送4:数据库中无该<终端>
        // <号码, 鉴权码>
        private Dictionary<string, string> Cars = new Dictionary<string, string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private Queue<JT808Package> JT808Queue;

        void Init()
        {
            var config = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "127.0.0.1:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,

                EnableAutoOffsetStore = false//<----this
            };

            var consumer = new JT808MsgIdConsumer(config, new NullLoggerFactory());
            consumer.Subscribe();
            consumer.OnMessage((msg) =>
            {
                Dispatcher.Invoke(() =>
                {
                    var p = new JT808Serializer().Deserialize(msg.data, JT808Version.JTT2019);
                    TbShowMsg.Text += $"{msg.MsgId}:{JsonConvert.SerializeObject(p).ToString()}" + Environment.NewLine;
                });
            });


            Thread.Sleep(1000);
        }

        private void BtnReceiveConsumer_OnClick(object sender, RoutedEventArgs e)
        {
            Init();
        }
    }
}
