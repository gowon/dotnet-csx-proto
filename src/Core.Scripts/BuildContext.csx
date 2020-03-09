#!/usr/bin/env dotnet-script

public enum TargetEnvironment
{
  Local,
  Development,
  Staging,
  Production
}

public sealed class BuildContext
{
  private static readonly Lazy<BuildContext> lazy =
    new Lazy<BuildContext>(() => new BuildContext());

  public static BuildContext Instance { get { return lazy.Value; } }

  private BuildContext()
  {
    Includes = new List<string>();
    Excludes = new List<string>();
  }

  public List<string> Includes { get; set; }
  public List<string> Excludes { get; set; }
  public TargetEnvironment Environment { get; set; }

  public override string ToString()
  {
    return $"{string.Join(",", Includes)};{string.Join(",", Excludes)};{Environment}";
  }
}