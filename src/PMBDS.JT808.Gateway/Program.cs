using SuperSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using JT808.Protocol;
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using PMBDS.JT808.Gateway.Handlers;
using PMBDS.JT808.Gateway.Helpers;
using PMBDS.JT808.Gateway.Metadata;
using PMBDS.JT808.Gateway.MsgIdHandlers;
using PMBDS.JT808.Gateway.PipelineFilters;
using PMBDS.JT808.Gateway.SessionManagers;
using PMBDS.JT808.Gateway.Sessions;
using PMBDS.JT808PubSubToKafka;
using SuperSocket.SessionContainer;

namespace PMBDS.JT808.Gateway
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ISessionContainer tcpSessionContainer = null;
            ISessionContainer udpSessionContainer = null;

            IJT808TcpSessionManager tcpSessionManager = null;
            IJT808SessionManagerBase udpSessionManager = null;

            var tcpHost = SuperSocketHostBuilder.Create<byte[], JT808PipelineFilter>(args)
                .ConfigureSuperSocket(options =>
                {
                    options.AddListener(new ListenOptions
                    {
                        Ip = "Any",
                        Port = 4040
                    })
                    .AddListener(new ListenOptions()
                    {
                        Ip = "Any",
                        Port = 8888
                    });
                })
                .UseSession<JT808TcpSession>()
                .UseSessionHandler(s =>
                {
                    //s["Identify"] = "0x001";
                    return default;
                })
                .UsePackageHandler(async (s, p) =>
                {
                    if (tcpSessionManager == null)
                    {
                        throw new ArgumentNullException(nameof(tcpSessionManager));
                    };
                    #region 解包/应答/转发
                    //Console.WriteLine(p.ToString());
                    //JT808Package jT808Package = JT808MsgId.位置信息汇报.Create("123456789012",
                    //    new JT808_0x0200
                    //    {
                    //        AlarmFlag = 1,
                    //        Altitude = 40,
                    //        GPSTime = DateTime.Parse("2018-10-15 10:10:10"),
                    //        Lat = 12222222,
                    //        Lng = 132444444,
                    //        Speed = 60,
                    //        Direction = 0,
                    //        StatusFlag = 2,
                    //        JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>
                    //        {
                    //            { JT808Constants.JT808_0x0200_0x01,new JT808_0x0200_0x01{Mileage = 100}},
                    //            { JT808Constants.JT808_0x0200_0x02,new JT808_0x0200_0x02{Oil = 125}}
                    //        }
                    //    });
                    //jT808Package.Header.ManualMsgNum = 1;
                    //byte[] data = new JT808Serializer().Serialize(jT808Package);
                    //await s.SendAsync(data: new ReadOnlyMemory<byte>(data));
                    //Console.WriteLine(s["Identify"].ToString());
                    #endregion


                    #region Kafka转发测试
                    //using (var provider = new JT808MsgIdProducer(new ProducerConfig(){BootstrapServers = "127.0.0.1:9092"}))
                    //{
                    //    provider.ProduceAsync(p.Header.MsgId.ToString(), p.Header.TerminalPhoneNo, new JT808Serializer().Serialize(p, version:JT808Version.JTT2019));
                    //    //provider.Flush();
                    //}
                    #endregion

                    using (var provider = new JT808MsgIdProducer(new ProducerConfig() { BootstrapServers = "127.0.0.1:9092" }))
                    {
                        var header = new JT808Serializer().HeaderDeserialize(p, JT808Version.JTT2019);


                        var tcp = new JT808MsgIdTcpCustomHandler(provider, new NullLoggerFactory(), tcpSessionManager);
                        var package = tcp.HandlerDict[header.Header.MsgId];
                        var pack = package(new JT808Request(header,p));
                        var receive = new JT808Serializer().Serialize(pack.Package, JT808Version.JTT2019);
                        await s.SendAsync(new ReadOnlyMemory<byte>(receive));
                    }

                })
                .ConfigureErrorHandler((s, v) =>
                {
                    Console.WriteLine($"\n[{DateTime.Now}] [TCP] Error信息:" + s.SessionID.ToString() + Environment.NewLine);
                    return default;
                })
                .UseMiddleware<InProcSessionContainerMiddleware>()
                .UseInProcSessionContainer()
                .BuildAsServer();

            tcpSessionContainer = tcpHost.GetSessionContainer();
            tcpSessionManager = new JT808TcpSerssionManager(tcpSessionContainer);


            //var udpHost = SuperSocketHostBuilder.Create<JT808Package, JT808PipelineFilter>(args)
            //    .ConfigureSuperSocket(options =>
            //    {
            //        options.AddListener(new ListenOptions
            //        {
            //            Ip = "Any",
            //            Port = 4041
            //        })
            //        .AddListener(new ListenOptions()
            //        {
            //            Ip = "Any",
            //            Port = 8886
            //        });
            //    })
            //    .UseSession<JT808TcpSession>()
            //    .UseSessionHandler(s =>
            //    {
            //        s["Identify"] = "0x001";
            //        return default;
            //    })
            //    .UsePackageHandler(async (s, p) =>
            //    {
            //        if (udpSessionManager == null) throw new ArgumentNullException(nameof(udpSessionManager));
            //        #region 解包/应答/转发
            //        Console.WriteLine(p.ToString());
            //        JT808Package jT808Package = JT808MsgId.位置信息汇报.Create("123456789012",
            //            new JT808_0x0200
            //            {
            //                AlarmFlag = 1,
            //                Altitude = 40,
            //                GPSTime = DateTime.Parse("2018-10-15 10:10:10"),
            //                Lat = 12222222,
            //                Lng = 132444444,
            //                Speed = 60,
            //                Direction = 0,
            //                StatusFlag = 2,
            //                JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>
            //                {
            //                    { JT808Constants.JT808_0x0200_0x01,new JT808_0x0200_0x01{Mileage = 100}},
            //                    { JT808Constants.JT808_0x0200_0x02,new JT808_0x0200_0x02{Oil = 125}}
            //                }
            //            });
            //        jT808Package.Header.ManualMsgNum = 1;
            //        byte[] data = new JT808Serializer().Serialize(jT808Package);
            //        await s.SendAsync(new ReadOnlyMemory<byte>(data));
            //        Console.WriteLine(s["Identify"].ToString());
            //        #endregion
            //    })
            //    .ConfigureErrorHandler((s, v) =>
            //    {
            //        Console.WriteLine($"\n[{DateTime.Now}] [TCP] Error信息:" + s.SessionID.ToString() + Environment.NewLine);
            //        return default;
            //    })
            //    .UseMiddleware<InProcSessionContainerMiddleware>()
            //    .UseInProcSessionContainer()
            //    .UseUdp()
            //    .BuildAsServer();

            //udpSessionContainer = udpHost.GetSessionContainer();
            //udpSessionManager = new JT808UdpSessionManagerL(udpSessionContainer);

            await tcpHost.StartAsync();
            //await udpHost.StartAsync();

            if (Console.ReadKey().KeyChar.Equals('q'))
            {
                await tcpHost.StopAsync();
            }
        }
    }
}
