namespace FolderMap.Models;

public class DirectoryNode
{
    public string ParentDirectory { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }

    public IEnumerable<DirectoryNode> Directories { get; set; }
    public IEnumerable<FileNode> Files { get; set; }

    public DateTime CreatedAt { get; set; }
}