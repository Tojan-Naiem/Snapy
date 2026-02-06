public class RestartCommand : ISnapyCommand
{
    public void Execute(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Use the required structure!");
            return ;
        }
        string FolderPath=args[1];
        if (!Directory.Exists(FolderPath))
        {
            Console.WriteLine("Invalid Path!");
            return ;
        }
        if (!CategoryDirectoryService.CheckDirectories(FolderPath))
        {
             Console.WriteLine("THIS folder Doesn't organized before !");
             return ;
        }
        CategoryDirectoryService.DeleteAllCategoryDirectories(FolderPath);
        Console.WriteLine("Succcessfully Restart ! ");
    }
   
  
}