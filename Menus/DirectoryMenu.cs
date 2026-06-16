using System.Reflection;
using FolderMap.Menus.Commands;
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
        Option option;
        
        do
        {
            Console.Write($"$({directoryService.GetPath()}): ");

            bool isTrueCommand = Enum.TryParse<Option>(Console.ReadLine(), out option);

            if (isTrueCommand is false)
                option = Option.another;

            SelectionMenu(option);
    
        } while (!option.Equals(Option.quit));
    }

    public void SelectionMenu(Option option)
    {
        switch (option)
        {
            case Option.all: ViewAllFilesDireactoryMenu(); break;
            case Option.back: BackMenu(); break;
            case Option.move: AccessMenu(); break;
            case Option.only: FilterFilesMenu(); break;
            case Option.help: HelpMenu(); break;
            case Option.tree: ShowTreeMenu(); break;
            case Option.quit: break;
            case Option.another: Console.WriteLine("You choose a different number."); HelpMenu(); break;
        }
    }

    public void ViewAllFilesDireactoryMenu()
    {
        foreach(var directory in directoryService.GetAllDirectories())
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(directory.Name);
            Console.ResetColor();
        }

        foreach(var file in directoryService.GetAllFiles())
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{file.Name} ({file.Length / 1000.0} kb)");
            Console.ResetColor();
        }
    }

    public void HelpMenu()
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("all - Show all files and folders in this path.");
        Console.WriteLine("back - Go back");
        Console.WriteLine("move - Access the folder");
        Console.WriteLine("only - Only files with extencion");
        Console.WriteLine("tree - Show a tree of all in path");
        Console.WriteLine("quit - Exit the program");
        Console.ResetColor();
    }

    public void BackMenu()
    {
        bool isBack = directoryService.BackPath();

        if (isBack is false)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"Cannot be moved.");
            Console.ResetColor();
        }
    }

    public void AccessMenu()
    {
        Console.Write("Enter a folder name: ");
        string name = Console.ReadLine();

        bool isMove = directoryService.AccessPath(name);

        if (isMove is false)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{name} is not folder.");   
            Console.ResetColor();
        }
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
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"{file.Name} ({file.Length / 1000.0} kb)");
                Console.ResetColor();
            }
        }
        else
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("There are no files with this extension.");        
            Console.ResetColor();
        }
    }

    public void ShowTreeMenu()
    {
        Console.WriteLine($"{directoryService.GetPath()}");
        directoryService.ShowTree(directoryService.GetPath());
    }
}