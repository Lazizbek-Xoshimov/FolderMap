namespace FolderMap.Services.DirectoryNodeServices;

public class DirectoryService : IDirectoryService
{
    private string Path { get; set; } = @"D:\sources\repos\FolderMap";
    private DirectoryInfo directory;
    public string Name { get; set; }

    public DirectoryService()
    {
        directory = new DirectoryInfo(Path);
        Name = directory.Name;
    }

    public string GetPath()
    {
        return Path;
    }

    public void SetPath(string newPath)
    {
        Path = newPath;
    }

    public IEnumerable<DirectoryInfo> GetAllDirectories()
    {
        return directory.EnumerateDirectories();
    }

    public IEnumerable<FileInfo> GetAllFiles()
    {
        return directory.EnumerateFiles();
    }

    public void BackPath()
    {
        var pathArray = GetPath().Split('\\').Where(name => !name.Equals(Name));
        var newPath = string.Join('\\', pathArray);
        SetPath(newPath);
        directory = new DirectoryInfo(newPath);
        Name = directory.Name;
    }

    public bool AccessPath(string name)
    {
        if (directory.EnumerateDirectories().Select(directory => directory.Name).Contains(name))
        {
            Path += $"\\{name}";
            directory = new DirectoryInfo(Path);
            Name = directory.Name;

            return true;
        }

        return false;
    }
}