using FolderMap.Menus.Commands;

namespace FolderMap.Services.DirectoryNodeServices;

public class DirectoryService : IDirectoryService
{
    private string Path { get; set; } = @"D:\sources\repos\FolderMap";
    private DirectoryInfo directory;

    private string space = "";

    public DirectoryService()
    {
        directory = new DirectoryInfo(Path);
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
        var currentDir = new DirectoryInfo(path);

        foreach (var folder in currentDir.EnumerateDirectories())
        {
            Console.WriteLine(space + folder.Name);
            space += "-";
            ShowTree(folder.FullName);
            space = space.Substring(0, space.Length - 1); 
        }

        foreach (var file in currentDir.EnumerateFiles())
        {
            Console.WriteLine(space + $"{file.Name} ({file.Length / 1000.0} kb)");
        }
    }

    public FileInfo FindFile(string path, string fileName)
    {   
        DirectoryInfo currentDir = new DirectoryInfo(path);
        FileInfo fileInfo = currentDir.EnumerateFiles().FirstOrDefault(file => file.Name.Equals(fileName));

        if (fileInfo is not null)
            return fileInfo;

        foreach (var folder in currentDir.EnumerateDirectories())
        {
            fileInfo = FindFile(folder.FullName, fileName);

            if (fileInfo is not null)
                return fileInfo;
        }

        return null;
    }

    public IEnumerable<FileInfo> SortFiles(SortOption sortOption)
    {
        IEnumerable<FileInfo> files = directory.EnumerateFiles();

        switch(sortOption)
        {
            case SortOption.name: files = files.OrderBy(file => file.Name); break;    
            case SortOption.date: files = files.OrderBy(file => file.CreationTime); break;    
            case SortOption.size: files = files.OrderBy(file => file.Length); break;    
        }

        return files;
    }
}