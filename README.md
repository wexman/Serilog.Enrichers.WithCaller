# Serilog.Enrichers.WithCaller

** how to use, code example see unit test code **

Performance of this approach is low due to Reflection and Diagnostic. However, it's helpful if you need the method Parameters and Values in your log output.

For better performance use the serilog template configuration, example OutputTemplate: {Timestamp:HH:mm:ss,fff} {Level:u3} {FileName} [{MemberName}] {Message:lj}{NewLine}{Exception}
