using OAKShell.Interfaces;
using System.CommandLine;

namespace OAKShell.Commands;

public sealed class GitDeployCommand(IProcessService processService) : IOAKShellCommand
{
    private readonly IProcessService _processService = processService;

    public string Verb => "deploy-git";

    public string Description => "Merges origin/source into origin/destination and updates local branches."; 

    /// <summary>
    /// Build the sync-git command.
    /// </summary>
    /// <returns></returns>
    public Command Build()
    {
        var deployGit = new Command(Verb, Description)
        {
            new Option<string>("--path", "--repo", "-p")
            {
                Description = "Optional path to Git repository outside of the current working directory."
            }, 
            new Option<string>("--source", "-s")
            {
                Description = "Source branch where code to deploy is coming from.",
                Required = true
            }, 
            new Option<string>("--destination", "--dest", "-d")
            {
                Description = "Destination branch to deploy code to.", 
                Required = true
            }
        }; 
        deployGit.SetAction(Handler);
        return deployGit;
    }

    /// <summary>
    /// Handler for sync-git command.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task Handler(ParseResult input)
    {
        var sourceBranch = input.GetRequiredValue<string>("--source");
        var destinationBranch = input.GetRequiredValue<string>("--destination"); 
        var repoPath = input.GetValue<string>("--path");
        string[] commandSequence = [
                $"checkout {sourceBranch}", "pull origin",
                $"checkout {destinationBranch}", "pull origin", 
                $"merge {sourceBranch}", "push origin"
                ];
        foreach (var command in commandSequence)
        {
            var success = await ExecuteGitCommandAsync(command, repoPath);
            if (!success) return; 
        }
        Console.WriteLine($"{sourceBranch} successfully deployed to {destinationBranch}.");
    }

    /// <summary>
    /// Execute a git command using the process service.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="workingDirectory"></param>
    /// <returns></returns>
    private async Task<bool> ExecuteGitCommandAsync(string command, string? workingDirectory = null)
    {
        var result = await _processService.ExecuteProcessAsync("git", command, workingDirectory);
        if (result.ExitCode != 0)
            Console.Error.WriteLine($"Failed to execute command \"git {command}\": {result.StandardError}"); 
        return result.ExitCode == 0;
    }
}
