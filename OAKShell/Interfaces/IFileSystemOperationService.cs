namespace OAKShell.Interfaces;

public interface IFileSystemOperationService
{
    string[] FindFiles(string pattern, string? directoryPath = null);
    void RemoveFiles(string[] filePathArray); 
    bool DirectoryExists(string path, bool createIfNotExists = false);
}
