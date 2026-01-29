using OAKShell.Interfaces;

namespace OAKShell.Services;

public sealed class FileSystemOperationService : IFileSystemOperationService
{
    /// <summary>
    /// Find files in the current directory matching the given pattern. 
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public string[] FindFiles(string pattern, string? directoryPath = null) 
        => Directory.GetFiles(directoryPath ?? Environment.CurrentDirectory, pattern);


    /// <summary>
    /// Remove all files in the given array of file paths. 
    /// </summary>
    /// <param name="filePathArray"></param>
    /// <returns></returns>
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
                Console.WriteLine($"Error deleting file: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Permission denied: {ex.Message}");
            }
        }
        Console.WriteLine($"Successfully deleted {deletedCount} files."); 
    }

    /// <summary>
    /// Check if the given directory exists.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="createIfNotExists"></param>
    /// <returns></returns>
    public bool DirectoryExists(string path, bool createIfNotExists = false)
    {
        var directoryExists = Directory.Exists(path);
        if (!directoryExists && createIfNotExists)
        {
            try
            {
                Directory.CreateDirectory(path);
                directoryExists = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create directory: {ex.Message}"); 
            }
        }
        return directoryExists;
    }
}
