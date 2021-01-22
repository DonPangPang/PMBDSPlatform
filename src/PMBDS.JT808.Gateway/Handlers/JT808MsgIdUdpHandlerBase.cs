using System;
using System.Collections.Generic;
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using PMBDS.JT808.Gateway.Metadata;
using PMBDS.JT808.Gateway.SessionManagers;

namespace PMBDS.JT808.Gateway.Handlers
{
    public abstract class JT808MsgIdUdpHandlerBase
    {
        protected IJT808UdpSessionManager sessionManager { get; }

        protected JT808MsgIdUdpHandlerBase(IJT808UdpSessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
            this.HandlerDict = new Dictionary<ushort, Func<JT808Request, JT808Response>>()
            {
                {
                  JT808MsgId.终端通用应答.ToUInt16Value<JT808MsgId>(),
                  new Func<JT808Request, JT808Response>(this.Msg0x0001)
                },
                {
                  JT808MsgId.终端鉴权.ToUInt16Value<JT808MsgId>(),
                  new Func<JT808Request, JT808Response>(this.Msg0x0102)
                },
                {
                  JT808MsgId.终端心跳.ToUInt16Value<JT808MsgId>(),
                  new Func<JT808Request, JT808Response>(this.Msg0x0002)
                },
                {
                  JT808MsgId.终端注销.ToUInt16Value<JT808MsgId>(),
                  new Func<JT808Request, JT808Response>(this.Msg0x0003)
                },
                {
                  JT808MsgId.终端注册.ToUInt16Value<JT808MsgId>(),
                  new Func<JT808Request, JT808Response>(this.Msg0x0100)
                },
                {
                  JT808MsgId.位置信息汇报.ToUInt16Value<JT808MsgId>(),
                  new Func<JT808Request, JT808Response>(this.Msg0x0200)
                },
                {
                  JT808MsgId.定位数据批量上传.ToUInt16Value<JT808MsgId>(),
                  new Func<JT808Request, JT808Response>(this.Msg0x0704)
                },
                {
                  JT808MsgId.数据上行透传.ToUInt16Value<JT808MsgId>(),
                  new Func<JT808Request, JT808Response>(this.Msg0x0900)
                }
            };
        }

        public Dictionary<ushort, Func<JT808Request, JT808Response>> HandlerDict { get; protected set; }

        public virtual JT808Response Msg0x0001(JT808Request request) => (JT808Response)null;

        public virtual JT808Response Msg0x0002(JT808Request request)
        {
            //this.sessionManager.Heartbeat(request.Package.Header.TerminalPhoneNo);
            return new JT808Response(JT808MsgId.平台通用应答.Create<JT808_0x8001>(request.Package.Header.TerminalPhoneNo, new JT808_0x8001()
            {
                //MsgId = request.Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.成功,
                MsgNum = request.Package.Header.MsgNum
            }));
        }

        public virtual JT808Response Msg0x0003(JT808Request request) => new JT808Response(JT808MsgId.平台通用应答.Create<JT808_0x8001>(request.Package.Header.TerminalPhoneNo, new JT808_0x8001()
        {
            //MsgId = request.Package.Header.MsgId,
            JT808PlatformResult = JT808PlatformResult.成功,
            MsgNum = request.Package.Header.MsgNum
        }));

        public virtual JT808Response Msg0x0100(JT808Request request) => new JT808Response(JT808MsgId.终端注册应答.Create<JT808_0x8100>(request.Package.Header.TerminalPhoneNo, new JT808_0x8100()
        {
            Code = "J" + request.Package.Header.TerminalPhoneNo,
            JT808TerminalRegisterResult = JT808TerminalRegisterResult.成功,
            AckMsgNum = request.Package.Header.MsgNum
        }));

        public virtual JT808Response Msg0x0102(JT808Request request) => new JT808Response(JT808MsgId.平台通用应答.Create<JT808_0x8001>(request.Package.Header.TerminalPhoneNo, new JT808_0x8001()
        {
            //MsgId = request.Package.Header.MsgId,
            JT808PlatformResult = JT808PlatformResult.成功,
            MsgNum = request.Package.Header.MsgNum
        }));

        public virtual JT808Response Msg0x0200(JT808Request request) => new JT808Response(JT808MsgId.平台通用应答.Create<JT808_0x8001>(request.Package.Header.TerminalPhoneNo, new JT808_0x8001()
        {
            //MsgId = request.Package.Header.MsgId,
            JT808PlatformResult = JT808PlatformResult.成功,
            MsgNum = request.Package.Header.MsgNum
        }));

        public virtual JT808Response Msg0x0704(JT808Request request) => new JT808Response(JT808MsgId.平台通用应答.Create<JT808_0x8001>(request.Package.Header.TerminalPhoneNo, new JT808_0x8001()
        {
            //MsgId = request.Package.Header.MsgId,
            JT808PlatformResult = JT808PlatformResult.成功,
            MsgNum = request.Package.Header.MsgNum
        }));

        public virtual JT808Response Msg0x0900(JT808Request request) => new JT808Response(JT808MsgId.平台通用应答.Create<JT808_0x8001>(request.Package.Header.TerminalPhoneNo, new JT808_0x8001()
        {
            //MsgId = request.Package.Header.MsgId,
            JT808PlatformResult = JT808PlatformResult.成功,
            MsgNum = request.Package.Header.MsgNum
        }));
    }
}