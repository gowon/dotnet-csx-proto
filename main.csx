#!/usr/bin/env dotnet-script
#r "nuget: Bullseye, 3.2.0"
#r "nuget: SimpleExec, 6.2.0"

using static Bullseye.Targets;
using static SimpleExec.Command;

Target("default", () =>
{
  System.Console.WriteLine("Hello, world!");
  Run("dotnet", "--version");
});

Target("clean", () =>
{
  Run("rm", "-r ./output");
});

Target("build", () =>
{
  Run("dotnet", "build");
});

Target("publish", DependsOn("clean"), () =>
{
  Run("dotnet", "publish -o ./output");
});

RunTargetsAndExit(Args);
