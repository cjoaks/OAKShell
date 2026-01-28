using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OAKShell.Interfaces;
using System.CommandLine;

namespace OAKShell.Extensions;

public static class RootCommandExtensions
{
    /// <summary>
    /// Get an a command that has been added as a service to the passed Host object, and add it this RootCommand. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rootCommand"></param>
    /// <param name="host"></param>
    public static void AddOAKShellCommand<T>(this RootCommand rootCommand, IHost host) where T : IOAKShellCommand
        => rootCommand.Add(
            host.Services.GetRequiredService<T>().Build());

}
