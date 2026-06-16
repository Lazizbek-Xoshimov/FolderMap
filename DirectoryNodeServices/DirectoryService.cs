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

    public bool BackPath()
    {
        if (directory.Parent is not null)
        {
            SetPath(directory.Parent.FullName);
            directory = new DirectoryInfo(Path);
            Name = directory.Name;

            return true;
        }

        return false;
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

    public IEnumerable<FileInfo> FilterFiles(string extension)
    {
        return directory.EnumerateFiles().Where(file => file.Extension.Equals(extension));
    }

    public void ShowTree(string path)
    {
        string space = "";

        if (!directory.EnumerateDirectories().Count().Equals(0))
        {
            foreach (var folder in directory.EnumerateDirectories())
            {
                Console.WriteLine(space + folder.Name);
                directory = new DirectoryInfo(folder.FullName);
                space += " ";
                ShowTree(folder.FullName);
            }
        }

        if (!directory.EnumerateFiles().Count().Equals(0))
        {
            foreach (var file in directory.EnumerateFiles())
            {
                Console.WriteLine(space + file.Name);
            }
        }
    }
}