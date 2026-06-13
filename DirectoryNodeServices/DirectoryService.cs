namespace FolderMap.Services.DirectoryNodeServices;

public class DirectoryService : IDirectoryService
{
    private string path;
    private DirectoryInfo directory;

    public DirectoryService()
    {
        path = @"D:\sources\repos\FolderMap";
        directory = new DirectoryInfo(path);
    }

    public string GetPath()
    {
        return path;
    }

    public void SetPath(string newPath)
    {
        path = newPath;
    }

    public IEnumerable<DirectoryInfo> GetAllDirectories()
    {
        return directory.EnumerateDirectories();
    }

    public IEnumerable<FileInfo> GetAllFiles()
    {
        return directory.EnumerateFiles();
    }

    public DirectoryInfo GetParentDirectory()
    {
        return directory.Parent;
    }
}