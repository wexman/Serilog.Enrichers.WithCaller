using Microsoft.VisualStudio.TestTools.UnitTesting;

using Serilog.Core;
using Serilog.Enrichers.WithCaller;
using Serilog.Sinks.InMemory;
using Serilog.Sinks.InMemory.Assertions;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Serilog.Enrichers.WithCaller.Tests
{
    [TestClass()]
    public class CallerEnricherTests
    {
        public static string LogMessageTemplate { get; } = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message} (at {Caller}){NewLine}{Exception}";
        public static LoggingLevelSwitch LoggingLevelSwitch { get; } = new LoggingLevelSwitch(Events.LogEventLevel.Verbose);

        public static InMemorySink InMemoryInstance => InMemorySink.Instance;

        public static ILogger CreateLogger(bool includeFileInfo)
        {
            return new LoggerConfiguration()
                .Enrich.WithCaller(includeFileInfo)
                .WriteTo.InMemory(outputTemplate: LogMessageTemplate)
                .CreateLogger();
        }

        [TestCleanup]
        public void Cleanup()
        {
            InMemoryInstance.Dispose();
        }

        //https://gist.github.com/nblumhardt/0e1e22f50fe79de60ad257f77653c813
        //https://github.com/serilog-contrib/SerilogSinksInMemory
        [TestMethod()]
        public void EnrichTest()
        {
            var logger = CreateLogger(includeFileInfo: false);
            logger.Error(new Exception(), "hello");
            InMemoryInstance.Should()
                .HaveMessage("hello")
                .Appearing().Once()
                .WithProperty("Caller")
                .WithValue("Serilog.Enrichers.WithCaller.Tests.CallerEnricherTests.EnrichTest()");
        }

        [TestMethod()]
        public void EnrichTestWithFileInfo()
        {
            var fileName = new StackFrame(fNeedFileInfo: true).GetFileName();
            var logger = CreateLogger(includeFileInfo: true);
            logger.Error(new Exception(), "hello");
            InMemoryInstance.Should()
                .HaveMessage("hello")
                .Appearing().Once()
                .WithProperty("Caller")
                .WithValue($"Serilog.Enrichers.WithCaller.Tests.CallerEnricherTests.EnrichTestWithFileInfo() {fileName}:57");
        }

    }
}