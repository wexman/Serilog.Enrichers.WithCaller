# Serilog.Enrichers.WithCaller

** how to use, code example see unit test code **

Performance of this approach is low due to Reflection and Diagnostic. However, it's helpful if you need the method Parameters and Values in your log output.

Example OutputTemplate: [{Timestamp:HH:mm:ss} {Level:u3}] {Message} (at {Caller}){NewLine}{Exception}
