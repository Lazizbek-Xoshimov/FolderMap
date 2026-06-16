using System.Reflection;
using FolderMap.Menus.Colors;
using FolderMap.Menus.Commands;
using FolderMap.Services.DirectoryNodeServices;

namespace FolderMap.Menus;

public class DirectoryMenu
{
    private IDirectoryService directoryService;
    int position;

    public DirectoryMenu()
    {
        directoryService = new DirectoryService();
        position = 0;
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
            case Option.find: FindFileMenu(); break;
            case Option.sort: SortFilesMenu(); break;
            case Option.quit: break;
            case Option.another: Console.WriteLine("You choose a different number."); HelpMenu(); break;
        }
    }

    public void ViewAllFilesDireactoryMenu()
    {
        foreach(var directory in directoryService.GetAllDirectories())
        {
            ConsoleColors.DirectoryColor(directory.Name);
        }

        foreach(var file in directoryService.GetAllFiles())
        {
            ConsoleColors.FileColor($"{file.Name} ({file.Length / 1000.0} kb)");
        }
    }

    public void HelpMenu()
    {
        Console.Clear();
        ConsoleColors.Info("""
                            all - Show all files and folders in this path.
                            back - Go back
                            move - Access the folder
                            only - Only files with extencion
                            tree - Show a tree of all in path
                            find - Search for a file
                            sort - Sort files by name, size and date
                            quit - Exit the program
                            """);
    }

    public void BackMenu()
    {
        bool isBack = directoryService.BackPath();

        if (isBack is false)
            ConsoleColors.Error("Cannot be moved.");
    }

    public void AccessMenu()
    {
        Console.Write("Enter a folder name: ");
        string name = Console.ReadLine();

        bool isMove = directoryService.AccessPath(name);

        if (isMove is false)
            ConsoleColors.Warning($"{name} is not folder.");
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
                ConsoleColors.FileColor($"{file.Name} ({file.Length / 1000.0} kb)");
            }
        }
        else
            ConsoleColors.Warning("There are no files with this extension.");
    }

    public void ShowTreeMenu()
    {
        Console.WriteLine($"{directoryService.GetPath()}");
        directoryService.ShowTree(directoryService.GetPath());
    }

    public void FindFileMenu()
    {
        Console.Clear();
        Console.Write("Enter a file name: ");
        string fileName = Console.ReadLine();

        FileInfo file = directoryService.FindFile(directoryService.GetPath(), fileName);

        if (file is null)
            ConsoleColors.Warning($"{fileName} is not found.");
        else
            ConsoleColors.FileColor($"{fileName} file in {file.FullName} path.");
    }

    public void SortFilesMenu()
    {
        ConsoleKeyInfo press;
        do
        {
            Console.Clear();
            Console.WriteLine($"$({directoryService.GetPath()}): ");
            ConsoleColors.Info("""
                                  name
                                  date
                                  size
                                """);

            switch (position)
            {
                case 0: Console.SetCursorPosition(0, 1); break;                        
                case 1: Console.SetCursorPosition(0, 2); break;
                case 2: Console.SetCursorPosition(0, 3); break;
            }

            Console.Write(">");
            press = Console.ReadKey(true);

            if (press.Key.Equals(ConsoleKey.DownArrow))
            {
                position ++;

                if (position > 2)
                    position = 0;
            }
            else if (press.Key.Equals(ConsoleKey.UpArrow))
            {
                position --;

                if (position < 0)
                    position = 2;
            }

        } while (!press.Key.Equals(ConsoleKey.Enter));

        Console.Clear();
        foreach (var file in directoryService.SortFiles(Enum.Parse<SortOption>(position.ToString())))
        {
            ConsoleColors.FileColor($"{file.Name} ({file.Length / 1000.0} kb) {file.CreationTime}");
        }
    }
}