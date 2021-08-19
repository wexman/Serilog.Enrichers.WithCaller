# https://nuget.pkg.github.com/pmetz-steelcase/index.json

Pushing to steelcase public nuget server:
[nuget version]
1.0.2

[feed]
from verion 5.0 the only feed is "GitHub pmetz-steelcase" (https://nuget.pkg.github.com/pmetz-steelcase/)

[pack]
pack is done automatically on build

[push]
D:\Source\nuget push -Source "GitHub" -ApiKey {your api key} "D:\Source\GitHub\pmetz-steelcase\Serilog.Enrichers.WithCaller\bin\Release\Serilog.Enrichers.WithCaller.{verion}.nupkg" -SkipDuplicate


[delete]
from github web

[list all versions]
not supported by nuget.exe
