using OAKShell.Interfaces;
using System.CommandLine;

namespace OAKShell.Commands;

public sealed class RemoveFilesCommand(IFileSystemOperationService fileOperationService) : IOAKShellCommand
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
            new Option<string>("--pattern", "--file", "-f")
            {
                Description = "The pattern matching the files to delete.",
                Required = true
            },
            new Option<bool>("--list", "-l")
            {
                Description = "If included, the files matching the given pattern will only be listed in the console, not deleted."
            }, 
            new Option<string>("--path", "-p")
            {
                Description = "File path to directory to search."
            }
        };
        removeFiles.SetAction(Handler); 
        return removeFiles; 
    }

    /// <summary>
    /// Handler for remove files command.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task Handler(ParseResult input)
    {
        var pattern = input.GetRequiredValue<string>("--pattern");
        var listOnly = input.GetValue<bool>("--list");
        var directoryPath = input.GetValue<string>("--path");
        if (directoryPath != null && !_fileSystemOperationService.DirectoryExists(directoryPath))
        {
            Console.Error.WriteLine($"Specififed directory {directoryPath} does not exist."); 
            return;
        }
        var filePathArray = _fileSystemOperationService.FindFiles(pattern, directoryPath);
        if (listOnly)
        {
            foreach (var filePath in filePathArray)
                Console.WriteLine(filePath);
        }
        else
            _fileSystemOperationService.RemoveFiles(filePathArray);
    }
}
