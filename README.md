# dotnet-csx-proto

## Objectives

### Create own build tool framework using C# scripting and libraries

- If the tool runs completely in .Net Core, then the tool is as portable as the SDK
  - PowerShell 7 is powerful, but:
    - Isn't the native bash on many environments
    - Scripting complex cli is much harder than using native C#
    - modules are harder to design, package and transport
- NuGet integration is powerful
- The only syntactical sugar that exists is what you create :exclamation:

```pwsh
dotnet script main.csx -- --include a,b,c --exclude d,e --environment Production
```

### Moonshots

[Expect](https://en.wikipedia.org/wiki/Expect) is a wonderful *nix tool to script interactions with other cli.
There have been some Windows tools/ports, but they are old and not .Net Core friendly ([Await](https://github.com/LeeHolmes/await), [Expect.NET](https://github.com/wiwanek/Expect.NET), [DotNetExpect](https://github.com/CBonnell/dotnetexpect)).
It would be great if we could port this to .Net Core and allow interactive scripting (possibly with YAML/JSON), `dotnet-expect`.

## References

### Tools/Frameworks

- [dotnet script](https://github.com/filipw/dotnet-script)
- [Bullseye](https://github.com/adamralph/bullseye) - target scaffolding and running
- [SimpleExec](https://github.com/adamralph/simple-exec) - external process wrapping
- [System.CommandLine](https://github.com/dotnet/command-line-api) - cli parser

### Scripting/Tooling Examples

- [DotNet.Build](https://github.com/seesharper/dotnet-build)
- [github-changelog](https://github.com/seesharper/github-changelog)

### Other

- [Awesome Actions](https://github.com/sdras/awesome-actions)
- [dotnet-tools](https://github.com/natemcmaster/dotnet-tools)

### Articles

- <https://pknopf.com/post/2019-03-10-you-dont-need-cake-anymore-the-way-to-build-dotnet-projects-going-forward/>
