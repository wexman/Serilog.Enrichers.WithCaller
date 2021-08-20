** how to use, code example see unit test code **

Performance of this approach is low due to Reflection and Diagnostic.
However, it's helpful if you need the method Parameters and Values

For better performance use the serilog template configuration, f.eg. 

OutputTemplate: {Timestamp:HH:mm:ss,fff} {Level:u3} {FileName} [{MemberName}] {Message:lj}{NewLine}{Exception}

