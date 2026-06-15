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
            Console.Write($"$({directoryService.GetPath()}): ");
            option = Console.ReadLine();

            SelectionMenu(option);
            
        } while (!option.Equals("quit"));
    }

    public void SelectionMenu(string option)
    {
        switch (option)
        {
            case "all": ViewAllFilesDireactoryMenu(); break;
            case "back": BackMenu(); break;
            case "forth": AccessMenu(); break;
            case "filter": FilterFilesMenu(); break;
            case "help": HelpMenu(); break;
            default: Console.WriteLine("You choose a different number."); HelpMenu(); break;
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
            Console.WriteLine($"{file.Name} ({file.Length / 1000.0} kb)");
        }
    }

    public void HelpMenu()
    {
        Console.WriteLine("all - Show all files and folders in this path.");
        Console.WriteLine("back - Go back");
        Console.WriteLine("forth - Access the folder");
        Console.WriteLine("filter - only files with extencion");
        Console.WriteLine("quit - Exit the program");
    }

    public void BackMenu()
    {
        bool isBack = directoryService.BackPath();

        if (isBack is false)
            Console.WriteLine($"Cannot be moved.");
    }

    public void AccessMenu()
    {
        Console.Write("Enter a folder name: ");
        string name = Console.ReadLine();

        bool isMove = directoryService.AccessPath(name);

        if (isMove is false)
            Console.WriteLine($"{name} is not folder.");
    }

    public void FilterFilesMenu()
    {
        Console.Write("Enter the file extension: ");
        string extension = Console.ReadLine();

        var files = directoryService.FilterFiles(extension);

        if (files is not null)
        {
            foreach (FileInfo file in files)
            {
                Console.WriteLine($"{file.Name} ({file.Length / 1000.0} kb)");
            }
        }
        else
            Console.WriteLine("There are no files with this extension.");        
    }
}