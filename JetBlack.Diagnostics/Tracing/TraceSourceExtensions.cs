using System;
using System.Diagnostics;

namespace JetBlack.Diagnostics.Tracing
{
    public static class TraceSourceExtensions
    {
        public static void TraceEvent(this TraceSource traceSource, TraceEventType eventType, int id, string message)
        {
            if (traceSource.Switch.ShouldTrace(eventType))
                traceSource.TraceEvent(eventType, id, message);
        }

        public static void Verbose(this TraceSource traceSource, int id, string message)
        {
            TraceEvent(traceSource, TraceEventType.Verbose, id, message);
        }

        public static void Verbose(this TraceSource traceSource, string message)
        {
            TraceEvent(traceSource, TraceEventType.Verbose, 0, message);
        }

        public static void Verbose(this TraceSource traceSource, int id, string format, params object[] args)
        {
            TraceEvent(traceSource, TraceEventType.Verbose, id, string.Format(format, args));
        }

        public static void Verbose(this TraceSource traceSource, string format, params object[] args)
        {
            TraceEvent(traceSource, TraceEventType.Verbose, 0, string.Format(format, args));
        }

        public static void Information(this TraceSource traceSource, int id, string message)
        {
            TraceEvent(traceSource, TraceEventType.Information, id, message);
        }

        public static void Information(this TraceSource traceSource, string message)
        {
            TraceEvent(traceSource, TraceEventType.Information, 0, message);
        }

        public static void Information(this TraceSource traceSource, int id, string format, params object[] args)
        {
            TraceEvent(traceSource, TraceEventType.Information, id, string.Format(format, args));
        }

        public static void Information(this TraceSource traceSource, string format, params object[] args)
        {
            TraceEvent(traceSource, TraceEventType.Information, 0, string.Format(format, args));
        }

        public static void Warning(this TraceSource traceSource, int id, string message)
        {
            TraceEvent(traceSource, TraceEventType.Warning, id, message);
        }

        public static void Warning(this TraceSource traceSource, string message)
        {
            TraceEvent(traceSource, TraceEventType.Warning, 0, message);
        }

        public static void Warning(this TraceSource traceSource, int id, string format, params object[] args)
        {
            TraceEvent(traceSource, TraceEventType.Warning, id, string.Format(format, args));
        }

        public static void Warning(this TraceSource traceSource, string format, params object[] args)
        {
            TraceEvent(traceSource, TraceEventType.Warning, 0, string.Format(format, args));
        }

        public static void Error(this TraceSource traceSource, int id, string message)
        {
            TraceEvent(traceSource, TraceEventType.Error, id, message);
        }

        public static void Error(this TraceSource traceSource, string message)
        {
            TraceEvent(traceSource, TraceEventType.Error, 0, message);
        }

        public static void Error(this TraceSource traceSource, int id, string format, params object[] args)
        {
            TraceEvent(traceSource, TraceEventType.Error, id, string.Format(format, args));
        }

        public static void Error(this TraceSource traceSource, string format, params object[] args)
        {
            TraceEvent(traceSource, TraceEventType.Error, 0, string.Format(format, args));
        }

        public static void Error(this TraceSource traceSource, Exception exception)
        {
            TraceEvent(traceSource, TraceEventType.Error, 0, string.Format("{0}\n{1}", exception.Message, exception.StackTrace));
        }

        public static void Error(this TraceSource traceSource, string message, Exception exception)
        {
            TraceEvent(traceSource, TraceEventType.Error, 0, string.Format("{0}\n{1}\n{2}", message, exception.Message, exception.StackTrace));
        }

        public static void Critical(this TraceSource traceSource, int id, string message)
        {
            TraceEvent(traceSource, TraceEventType.Critical, id, message);
        }

        public static void Critical(this TraceSource traceSource, string message)
        {
            TraceEvent(traceSource, TraceEventType.Critical, 0, message);
        }

        public static void Critical(this TraceSource traceSource, int id, string format, params object[] args)
        {
            TraceEvent(traceSource, TraceEventType.Critical, id, string.Format(format, args));
        }

        public static void Critical(this TraceSource traceSource, string format, params object[] args)
        {
            TraceEvent(traceSource, TraceEventType.Critical, 0, string.Format(format, args));
        }
    }
}
