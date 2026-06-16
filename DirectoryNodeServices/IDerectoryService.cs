using FolderMap.Menus.Commands;

namespace FolderMap.Services.DirectoryNodeServices;

public interface IDirectoryService
{
    public string GetPath();
    public void SetPath(string newPath);
    public IEnumerable<DirectoryInfo> GetAllDirectories();
    public IEnumerable<FileInfo> GetAllFiles();
    public bool BackPath();
    public bool AccessPath(string name);
    public IEnumerable<FileInfo> FilterFiles(string extension);
    public void ShowTree(string path = null);
    public FileInfo FindFile(string path = "", string fileName = "");
    public IEnumerable<FileInfo> SortFiles(SortOption sortOption);
}