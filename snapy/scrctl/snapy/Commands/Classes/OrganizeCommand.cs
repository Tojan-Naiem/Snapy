using SixLabors.ImageSharp;

public class OrganizeCommand : ICommand
{
    private ImageClassifier ImageClassifier;
    public void Execute(string []args)
    {
        // create Image Classifier and add pathes for onnx
        ImageClassifier=new ImageClassifier(
            ModelPaths.ImageOnnx,
            ModelPaths.TextOnnx,
            ModelPaths.TextEmbeddings
        );
        try
        {
        if (args.Length != 3)
        {
            Console.WriteLine("Use the required structure!");
            return;
        }
        string path=args[2];
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Invalid Path!");
                return;
            }
        string [] myFiles=Directory.GetFiles(path);
        CreateDirectories(path);
        foreach(var filePath in myFiles)
        {
           string dirName=ImageClassifier.classifyImage(filePath);
           string newFilePath=path+"/"+dirName+"/"+filePath.Substring(filePath.LastIndexOf("/"));
           File.Copy(filePath,newFilePath);
           File.Delete(filePath);
        }

        }
        catch(Exception e)
        {
            Console.WriteLine("Exception happen ! "+e.ToString());
        }
       
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
        try
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
  
        }catch(Exception e)
        {
            Console.WriteLine("Exception Happen ! "+e.GetBaseException());
        }
     
        
    }

}