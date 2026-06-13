using FolderMap.Services.DirectoryNodeServices;

namespace FolderMap.Menus;

public class DirectoryMenu
{
    private IDirectoryService directoryService;

    public DirectoryMenu()
    {
        directoryService = new DirectoryService();
    }

    public void BaseMenu()
    {
        Console.WriteLine(directoryService.GetPath());

        Console.Write("$ (--help): ");
        string option = Console.ReadLine();
        SelectionMenu(option);
    }

    public void SelectionMenu(string option)
    {
        switch (option)
        {
            case "dir": ViewAllFilesDireactory(); break;
            case "--help": Help(); break;
            default: Console.WriteLine("You choose a different number."); break;
        }
    }

    public void ViewAllFilesDireactory()
    {
        foreach(var directory in directoryService.GetAllDirectories())
        {
            Console.WriteLine(directory.Name);
        }

        foreach(var file in directoryService.GetAllFiles())
        {
            Console.WriteLine(file.Name + $" ({file.Length / 1000.0} kb)");
        }
    }

    public void Help()
    {
        Console.WriteLine("dir - Show all files and folders in this path.");
    }
}