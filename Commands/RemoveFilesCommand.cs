using OAKShell.Interfaces;
using System.CommandLine;

namespace OAKShell.Commands;

public sealed class RemoveFilesCommand(
    IFileSystemOperationService fileOperationService) : IOAKShellCommand
{
    private readonly IFileSystemOperationService _fileSystemOperationService = fileOperationService;

    public string Verb => "remove";

    public string Description => "Remove files matching a given pattern, at the current directory."; 

    /// <summary>
    /// Build remove files command.
    /// </summary>
    /// <returns></returns>
    public Command Build()
    {
        var removeFiles = new Command(Verb, Description)
        {
            new Option<string>("--pattern")
            {
                Description = "The pattern matching the files to delete.",
                Required = true
            },
            new Option<bool>("--list")
        };
        removeFiles.SetAction(Handler); 
        return removeFiles; 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public async Task Handler(ParseResult result)
    {
        var pattern = result.GetRequiredValue<string>("--pattern"); 

    }
}
