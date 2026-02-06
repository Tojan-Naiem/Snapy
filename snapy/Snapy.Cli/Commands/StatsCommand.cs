

public class StatsCommand : ISnapyCommand
{
    public void Execute(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Use the required structure!");
            return;
        }
        string FolderPath = args[1];
        if (!Directory.Exists(FolderPath))
        {
            Console.WriteLine("Invalid Path!");
            return;
        }
        Console.WriteLine("──────────────────────────────");
        Console.WriteLine($"Folder: {FolderPath}");
        Console.WriteLine("──────────────────────────────");
        Console.WriteLine("Total files :" + DirectoryService.CountFiles(FolderPath));
        Console.WriteLine($"Total size: {DirectoryService.TotoalFilesLength(FolderPath)}");
        Console.WriteLine("──────────────────────────────");
        Console.WriteLine("Files by category:");

        foreach (var category in Categories.categories)
        {
            long countCategory = CategoryDirectoryService.CountCategoryFiles(FolderPath, category);
            if (countCategory == -1)
            {
                Console.WriteLine("Error in counting categories");
                return;
            }
            Console.WriteLine($"  {category,-20}: {countCategory}");

        }
        Console.WriteLine("──────────────────────────────");

        Console.WriteLine($"Last organized : {DirectoryService.LastAccessTime(FolderPath)}");
        Console.WriteLine("──────────────────────────────");

    }


}