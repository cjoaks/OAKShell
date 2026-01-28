using OAKShell.Classes;

namespace OAKShell.Interfaces;

public interface IProcessService
{
    Task<ProcessResult> ExecuteProcessAsync(string command, string arguments, string? workingDirectory = null); 
}
