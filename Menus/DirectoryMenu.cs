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
        string option = "";
        do
        {
            Console.WriteLine(directoryService.GetPath());
            Console.Write("$ (--help): ");
            option = Console.ReadLine();

            SelectionMenu(option);
            
        } while (!option.Equals("quit"));
    }

    public void SelectionMenu(string option)
    {
        switch (option)
        {
            case "dir": ViewAllFilesDireactoryMenu(); break;
            case "cd..": BackMenu(); break;
            case "mv": AccessMenu(); break;
            case "--help": HelpMenu(); break;
            default: Console.WriteLine("You choose a different number."); break;
        }
    }

    public void ViewAllFilesDireactoryMenu()
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

    public void HelpMenu()
    {
        Console.WriteLine("dir - Show all files and folders in this path.");
        Console.WriteLine("cd.. - Go back");
        Console.WriteLine("mv - Access the folder");
        Console.WriteLine("quit - Exit the program");
    }

    public void BackMenu()
    {
        directoryService.BackPath();
    }

    public void AccessMenu()
    {
        Console.Write("Enter a folder name: ");
        string name = Console.ReadLine();

        bool isMove = directoryService.AccessPath(name);

        if (isMove is false)
            Console.WriteLine($"{name} is not folder.");
    }
}