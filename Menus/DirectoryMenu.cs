using System.Reflection;
using FolderMap.Menus.Colors;
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
            case Option.find: FindFileMenu(); break;
            case Option.sort: SortFilesMenu(); break;
            case Option.files: FilesMenu(); break;
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
                            files - Show files and operations on them
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
        {
            ConsoleColors.FileColor($"{fileName} file in {file.FullName} path.");

            Console.WriteLine("Would you like to get information about this file?");
            Console.Write("(yes/no): ");
            string answer = Console.ReadLine();    

            if (answer.ToLower().Equals("yes"))
            {
                Console.WriteLine($"File name: {file.Name}");
                Console.WriteLine($"File size: {file.Length / 1000.0} kb");
                Console.WriteLine($"Number of lines in file: {File.ReadAllLines(file.FullName).Count()}");
                Console.WriteLine($"Number of words in the file: {File.ReadAllText(file.FullName).Split(' ').Count()}");
                Console.WriteLine($"File created: {file.CreationTime}");
                Console.WriteLine($"File updated: {file.LastWriteTime}");
            }
        }
    }

    public void SortFilesMenu()
    {
        int position = SelectWithArrow(new string[] {"name", "date", "size"});

        Console.Clear();
        foreach (var file in directoryService.SortFiles(Enum.Parse<SortOption>(position.ToString())))
        {
            ConsoleColors.FileColor($"{file.Name} ({file.Length / 1000.0} kb) {file.CreationTime}");
        }
    }

    public int SelectWithArrow(IEnumerable<string> informations)
    {
        int position = 0;
        ConsoleKeyInfo press;

        do
        {
            Console.Clear();
            Console.WriteLine($"$({directoryService.GetPath()}): ");
            
            foreach (var info in informations)
            {
                ConsoleColors.Info("  " + info);
            }

            Console.SetCursorPosition(0, position + 1);

            ConsoleColors.InfoWrite(">");
            press = Console.ReadKey(true);

            if (press.Key.Equals(ConsoleKey.DownArrow))
            {
                position ++;

                if (position > informations.Count() - 1)
                    position = 0;
            }
            else if (press.Key.Equals(ConsoleKey.UpArrow))
            {
                position --;

                if (position < 0)
                    position = informations.Count() - 1;
            }

        } while (!press.Key.Equals(ConsoleKey.Enter));

        return position;
    }

    public void FilesMenu()
    {
        var files = directoryService.GetAllFiles().ToList();
        int positionFile = SelectWithArrow(files.Select(file => file.Name));

        string[] fileSelections = {"Add text", "Read text", "File information", "Back"};
        int positionSelection = SelectWithArrow(fileSelections);

        Console.Clear();
        switch (positionSelection)
        {
            case 0:
            {
                using StreamWriter streamWriter = files[positionFile].AppendText();

                Console.Write("Enter the line you want to add: ");
                string addingText = Console.ReadLine();

                streamWriter.WriteLine(addingText);
                break;
            }
            case 1:
            {
                using StreamReader streamReader = files[positionFile].OpenText();
                
                Console.WriteLine($"{files[positionFile].Name} file information: ");
                string dataOfFile = streamReader.ReadToEnd();
                Console.WriteLine(dataOfFile);
                break;
            }
            case 2:
            {
                Console.WriteLine($"File name: {files[positionFile].Name}");
                Console.WriteLine($"File size: {files[positionFile].Length / 1000.0} kb");
                Console.WriteLine($"File created: {files[positionFile].CreationTime}");
                Console.WriteLine($"File updated: {files[positionFile].LastWriteTime}");
                break;
            }
            case 3:
            {
                break;
            }
        }
    }
}