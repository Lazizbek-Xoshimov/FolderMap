namespace FolderMap.Menus.Colors;

public static class ConsoleColors
{
    public static void Error(string text)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void Warning(string text)
    {
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void Info(string text)
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void FileColor(string text)
    {
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void DirectoryColor(string text)
    {
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}