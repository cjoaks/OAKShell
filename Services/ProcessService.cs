using OAKShell.Classes;
using OAKShell.Interfaces;
using System.Diagnostics;


namespace OAKShell.Services;

public sealed class ProcessService : IProcessService
{
    /// <summary>
    /// Execute an external process with the given parameters.
    /// </summary>
    /// <param name="workingDirectory"></param>
    /// <param name="fileName"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    public async Task<ProcessResult> ExecuteProcessAsync(string fileName, string arguments, string? workingDirectory = null)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            WorkingDirectory = workingDirectory ?? Environment.CurrentDirectory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };
        using var process = new Process
        {
            StartInfo = processStartInfo
        };
        process.Start();
        await process.WaitForExitAsync();
        return new ProcessResult
        {
            ExitCode = process.ExitCode,
            StandardOutput = await process.StandardOutput.ReadToEndAsync(),
            StandardError = await process.StandardError.ReadToEndAsync()
        }; 
    }
}
