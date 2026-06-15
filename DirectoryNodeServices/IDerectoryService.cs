namespace FolderMap.Services.DirectoryNodeServices;

public interface IDirectoryService
{
    public string GetPath();
    public void SetPath(string newPath);
    public IEnumerable<DirectoryInfo> GetAllDirectories();
    public IEnumerable<FileInfo> GetAllFiles();
    public void BackPath();
    public bool AccessPath(string name);
}