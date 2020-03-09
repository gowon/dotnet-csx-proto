#!/usr/bin/env dotnet-script
#r "nuget: Bullseye, 3.2.0"
#r "nuget: System.CommandLine, 2.0.0-beta1.20104.2"
#load "BuildContext.csx"

using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.CommandLine.IO;
using System.Linq;
using Bullseye;
using static Bullseye.Targets;

public static class BuildToolsHelper
{
  public static void BuildAndRun(params string[] args)
  {
    var cmd = new RootCommand()
    {
        new Option(new[] { "--include" }, "Include projects from list of targets.") { Argument = new Argument<List<string>>() { Name = "projects", Arity = ArgumentArity.OneOrMore } },
        new Option(new[] { "--exclude" }, "Exclude projects from list of targets.") { Argument = new Argument<List<string>>() { Name = "projects", Arity = ArgumentArity.OneOrMore } },
        new Option(new[] { "--environment" }, "Target environment.") {
            Argument = new Argument<string>(() => "Local") { Name = "environment",Arity = ArgumentArity.ExactlyOne }
        }
    };

    // translate from Bullseye to System.CommandLine
    cmd.Add(new Argument("targets") { Arity = ArgumentArity.ZeroOrMore, Description = "The targets to run or list." });
    foreach (var option in Options.Definitions)
    {
      cmd.Add(new Option(new[] { option.ShortName, option.LongName }.Where(n => !string.IsNullOrWhiteSpace(n)).ToArray(), option.Description));
    }

    cmd.Handler = CommandHandler.Create((ParseResult parseResult, IConsole console) =>
      {
        // hydrating BuildContext
        BuildContext.Instance.Includes = parseResult.ValueForOption<List<string>>("--include");
        BuildContext.Instance.Excludes = parseResult.ValueForOption<List<string>>("--exclude");
        BuildContext.Instance.Environment = ParseTargetEnvironment(parseResult.ValueForOption<string>("--environment"));

        console.Out.WriteLine($"{parseResult}");

        // translate from System.CommandLine to Bullseye
        var targets = parseResult.CommandResult.Tokens.Select(token => token.Value);
        var options = new Options(Options.Definitions.Select(o => (o.LongName, parseResult.ValueForOption<bool>(o.LongName))));
        RunTargetsAndExit(targets, options);
      });

    cmd.Invoke(args);
  }

  public static TargetEnvironment ParseTargetEnvironment(string environment)
  {
    switch (environment.ToLowerInvariant())
    {
      case "prod":
      case "production":
        return TargetEnvironment.Production;
      case "stage":
      case "staging":
        return TargetEnvironment.Staging;
      case "dev":
      case "develop":
      case "development":
        return TargetEnvironment.Development;
      default:
        return TargetEnvironment.Local;
    }
  }
}