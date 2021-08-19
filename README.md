** how to use, example:
    // create logger with email and file sink
    private static void CreateLogger()
    {
        Log.CloseAndFlush();

        var logConfig = new LoggerConfiguration()
                        .MinimumLevel.ControlledBy(AppConfig.LoggingLevelSwitch)
                        .WriteTo.Async(f => f.File
                        (
                            AppConfig.LogFileTemplate,
                            flushToDiskInterval: TimeSpan.FromMilliseconds(AppConfig.LogFlushInterval),
                            rollingInterval: RollingInterval.Day,
                            levelSwitch: AppConfig.LoggingLevelSwitch
                        ));

        if (!string.IsNullOrWhiteSpace(AppConfig.LogEmailRecipients))
            logConfig.WriteTo.Email
            (
                fromEmail: $"{Environment.MachineName}@steelcase.com",
                toEmail: AppConfig.LogEmailRecipients,
                mailSubject: $"Log Event from {ConfigBase.ExecutingAssemblyName} ",
                restrictedToMinimumLevel: LogEventLevel.Fatal,
                mailServer: AppConfig.SMTPServer,
                batchPostingLimit: AppConfig.LogBatchPostingLimit,
                period: TimeSpan.FromMinutes(AppConfig.LogBatchPeriod)
            );

        if (Environment.UserInteractive)
            logConfig = logConfig.WriteTo.Console(levelSwitch: AppConfig.LoggingLevelSwitch);

        Log.Logger = logConfig.CreateLogger();
    }
    // create logger without any values from config
    private static void CreateMinimumLogger()
    {
        var logFile = Path.Combine(AppConfig.AssemblyDirectory, "Logs", nameof(ReportPrintService) + ".log");
        var logConfig = new LoggerConfiguration()
                                .MinimumLevel.ControlledBy(AppConfig.LoggingLevelSwitch)
                                .WriteTo.File
                                (
                                    AppConfig.LogFileTemplate,
                                    rollingInterval: RollingInterval.Day
                                );
        if (System.Diagnostics.Debugger.IsAttached)
        {
            AppConfig.LoggingLevelSwitch.MinimumLevel = LogEventLevel.Debug;
            logConfig = logConfig.WriteTo.Console();
        }

        Log.Logger = logConfig.CreateLogger();
    }
