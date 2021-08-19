using System;
using System.Reflection;
using System.Diagnostics;
using System.Linq;

using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers.WithCaller
{
    public class CallerEnricher : ILogEventEnricher
    {
        public static int SkipFramesCount { get; set; } = 3;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            int skipFramesCount = SkipFramesCount;
            while (true)
            {
                StackFrame stack = new StackFrame(skipFramesCount);
                if (!stack.HasMethod())
                {
                    logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue("<unknown method>")));
                    return;
                }

                MethodBase method = stack.GetMethod();
                if (method.DeclaringType.Assembly != typeof(Log).Assembly)
                {
                    string caller = $"{method.DeclaringType.FullName}.{method.Name}({string.Join(", ", method.GetParameters().Select(pi => pi.ParameterType.FullName))})";
                    logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue(caller)));
                    return;
                }

                skipFramesCount++;
            }
        }
    }
}
