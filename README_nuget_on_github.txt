# https://nuget.pkg.github.com/pmetz-steelcase/index.json

Pushing to steelcase public nuget server:
[nuget version]
{PackageVersion}

[feed]
from verion 5.0 the only feed is "GitHub pmetz-steelcase" (https://nuget.pkg.github.com/pmetz-steelcase/)

[pack]
pack is done automatically on build

[push]
D:\Source\nuget.exe push -Source "GitHub pmetz-steelcase" -ApiKey AzureDevOps D:\Source\GitHub\pmetz-steelcase\Serilog.Enrichers.WithCaller\bin\Release\Serilog.Enrichers.WithCaller.{PackageVersion}.nupkg

[delete]
D:\Source\nuget.exe delete -Source "GitHub pmetz-steelcase" -ApiKey AzureDevOps Serilog.Enrichers.WithCaller {PackageVersion}

[list all versions]
D:\Source\nuget.exe list -AllVersions -Source "GitHub pmetz-steelcase" Serilog.Enrichers.WithCaller
