using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.Logging;
using PMBDS.JT808.Gateway.Metadata;
using PMBDS.PubSub.Abstractions;

namespace PMBDS.JT808.Gateway.MsgIdHandlers
{
    public class JT808MsgIdTcpCustomHandler: JT808MsgIdTcpHandlerBase
    {
        private readonly IJT808Producer jT808Producer;
        private readonly ILogger<JT808MsgIdTcpCustomHandler> logger;

        public JT808MsgIdTcpCustomHandler(
            IJT808Producer jT808Producer,
            ILoggerFactory loggerFactory,
            /*JT808TcpSessionManager sessionManager*/) : base(/*sessionManager*/)
        {
            logger = loggerFactory.CreateLogger<JT808MsgIdTcpCustomHandler>();
            this.jT808Producer = jT808Producer;
            logger.LogDebug("test");
        }
        public override JT808Response Msg0x0001(JT808Request request)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(string.Join(" ", request.OriginalPackage));
                logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            jT808Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), request.Package.Header.TerminalPhoneNo, request.OriginalPackage);
            return base.Msg0x0001(request);
        }
        public override JT808Response Msg0x0002(JT808Request request)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(string.Join(" ", request.OriginalPackage));
                logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            jT808Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), request.Package.Header.TerminalPhoneNo, request.OriginalPackage);
            return base.Msg0x0002(request);
        }
        public override JT808Response Msg0x0003(JT808Request request)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(string.Join(" ", request.OriginalPackage));
                logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            jT808Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), request.Package.Header.TerminalPhoneNo, request.OriginalPackage);
            return base.Msg0x0003(request);
        }
        public override JT808Response Msg0x0100(JT808Request request)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(string.Join(" ", request.OriginalPackage));
                logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            jT808Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), request.Package.Header.TerminalPhoneNo, request.OriginalPackage);
            return base.Msg0x0100(request);
        }
        public override JT808Response Msg0x0102(JT808Request request)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(string.Join(" ", request.OriginalPackage));
                logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            jT808Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), request.Package.Header.TerminalPhoneNo, request.OriginalPackage);
            return base.Msg0x0102(request);
        }
        public override JT808Response Msg0x0200(JT808Request request)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(string.Join(" ", request.OriginalPackage));
                logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            jT808Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), request.Package.Header.TerminalPhoneNo, request.OriginalPackage);
            return base.Msg0x0200(request);
        }
        public override JT808Response Msg0x0704(JT808Request request)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(string.Join(" ", request.OriginalPackage));
                logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            jT808Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), request.Package.Header.TerminalPhoneNo, request.OriginalPackage);
            return base.Msg0x0704(request);
        }
        public override JT808Response Msg0x0900(JT808Request request)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(string.Join(" ", request.OriginalPackage));
                logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            jT808Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), request.Package.Header.TerminalPhoneNo, request.OriginalPackage);
            return base.Msg0x0900(request);
        }
        public JT808Response Msg0x0705(JT808Request request)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(string.Join(" ", request.OriginalPackage));
                logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            jT808Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), request.Package.Header.TerminalPhoneNo, request.OriginalPackage);
            return new JT808Response(JT808MsgId.平台通用应答.Create(request.Package.Header.TerminalPhoneNo, new JT808_0x8001()
            {
                //MsgId = request.Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.成功,
                MsgNum = request.Package.Header.MsgNum
            }));
        }
        public JT808Response Msg0x0801(JT808Request request)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(string.Join(" ", request.OriginalPackage));
                logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request.Package));
            }
            jT808Producer.ProduceAsync(request.Package.Header.MsgId.ToString(), request.Package.Header.TerminalPhoneNo, request.OriginalPackage);
            return null;
        }
    }
}