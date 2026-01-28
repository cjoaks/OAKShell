namespace OAKShell.Interfaces;

public interface IFileSystemOperationService
{
    string[] FindFiles(string pattern);
    void RemoveFiles(string[] filePathArray); 
}
