public class RestartCommand : ICommand
{
    public void Execute(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Use the required structure!");
            return ;
        }
        string FolderPath=args[2];
        if (!Directory.Exists(FolderPath))
        {
            Console.WriteLine("Invalid Path!");
            return ;
        }
        if (!CheckDirectories(FolderPath))
        {
             Console.WriteLine("THIS folder Doesn't organized before !");
             return ;
        }
        DeleteDirectories(FolderPath);
        
    }
    public bool CheckDirectories(string folderPath)
    {
        foreach(var s in Categories.categories)
        if(!Directory.Exists(folderPath+"/"+s))
         return false;
        return true;
        
    }
    public void DeleteDirectories(string folderPath)
    {
        foreach(var s in Categories.categories)
        {
            string dirPath=folderPath+"/"+s;
            string []myFiles=Directory.GetFiles(dirPath);
        foreach(var filePath in myFiles)
        {
           string newPathFile=folderPath+"/"+filePath.Substring(filePath.LastIndexOf("/"));
           File.Copy(filePath,newPathFile);
           File.Delete(filePath);
        }
        Directory.Delete(dirPath);
        }

    }
}