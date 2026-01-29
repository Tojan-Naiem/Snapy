public class OrganizeCommand : ICommand
{
    public void Execute(string args)
    {
        string screenshotPath=args;
       CreateDirectories(screenshotPath);
        Console.WriteLine(args);
    }
    public void CreateDirectories(string path)
    {
        for(int i = 0; i < Categories.categories.Length; i++)
        {
            CreateDirectory(path,Categories.categories[i]);
        }

    }
    public void CreateDirectory(string path,string dirName)
    {
        string newDir=path+"/"+dirName;
        if (Directory.Exists(newDir))
        {
            Console.WriteLine("Directory "+dirName+" already exists");
            return;
        }
        DirectoryInfo di=Directory.CreateDirectory(newDir);
        if (!Directory.Exists(newDir))
        {
            Console.WriteLine("Directory does not exists");
            return;
        }
        Console.WriteLine("Successfully created Directory "+dirName+" !!");

        
    }

}