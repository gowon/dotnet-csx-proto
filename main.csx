#!/usr/bin/env dotnet-script
#load ".\src\Core.Scripts\BuildToolsHelper.csx"
#load "targets.csx"

using static BuildToolsHelper;

BuildAndRun(Args.ToArray());