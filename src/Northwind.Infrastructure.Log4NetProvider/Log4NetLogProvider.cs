using Northwind.MusicStore.BusinessInterfaces;
using log4net;

namespace Northwind.Infrastructure.Log4NetProvider
{

    public class Log4NetLogProvider : ILogProvider
    {
        private readonly ILog _log;

        public Log4NetLogProvider(ILog log)
        {
            _log = log;
        }

        public void Debug(object message)
        {
            _log.Debug(message);
        }

        public void DebugFormat(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }

        public void Info(object message)
        {
            _log.Info(message);
        }

        public void InfoFormat(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }

        public void Warn(object message)
        {
            _log.Warn(message);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _log.WarnFormat(format, args);
        }

        public void Error(object message)
        {
            _log.Error(message);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _log.ErrorFormat(format, args);
        }

        public void Fatal(object message)
        {
            _log.Fatal(message);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _log.FatalFormat(format, args);
        }
    }

}
