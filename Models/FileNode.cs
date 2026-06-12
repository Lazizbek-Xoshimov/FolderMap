namespace FolderMap.Models;

public class FileNode
{
    public DirectoryNode DirectoryName { get; set; }
    public int Size { get; set; }
    public string Name { get; set; }
}