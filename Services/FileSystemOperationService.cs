using Microsoft.Extensions.Logging;
using OAKShell.Interfaces;

namespace OAKShell.Services;

public sealed class FileSystemOperationService(
    ILogger<FileSystemOperationService> logger) : IFileSystemOperationService
{

    private readonly ILogger<FileSystemOperationService> _logger = logger;

    /// <summary>
    /// Find files in the current directory matching the given pattern. 
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public string[] FindFiles(string pattern) => Directory.GetFiles(Environment.CurrentDirectory, pattern); 


    /// <summary>
    /// Remove all files in the given array of file paths. 
    /// </summary>
    /// <param name="filePathArray"></param>
    public void RemoveFiles(string[] filePathArray)
    {
        if (filePathArray.Length == 0) return;
        var deletedCount = 0; 
        foreach (var filePath in filePathArray)
        {
            try
            {
                File.Delete(filePath);
                ++deletedCount; 
            }
            catch (IOException ex)
            { 
                _logger.LogError("Error deleting file: {Message}", ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError("Permission denied: {Message}", ex.Message);
            }
        }
        if (_logger.IsEnabled(LogLevel.Information))
            _logger.LogInformation("Successfully deleted {FileCount} files.", deletedCount); 
    }

}
