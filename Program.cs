using FolderMap.Menus;

DirectoryMenu directoryMenu = new DirectoryMenu();

Console.BackgroundColor = ConsoleColor.Magenta;
Console.ForegroundColor = ConsoleColor.Black;
Console.WriteLine("Directory Tree Viewer");
Console.ResetColor();

directoryMenu.BaseMenu();