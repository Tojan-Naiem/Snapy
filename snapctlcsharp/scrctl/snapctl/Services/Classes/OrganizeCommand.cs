using SixLabors.ImageSharp;

public class OrganizeCommand : ICommand
{
    private ImageClassifier ImageClassifier;
    public void Execute(string args)
    {
        ImageClassifier=new ImageClassifier("/home/tojan/Documents/Python Projects/snapctl/py/clip_image.onnx",
        "/home/tojan/Documents/Python Projects/snapctl/py/clip_text.onnx",
        "/home/tojan/Documents/Python Projects/snapctl/py/text_embeddings.bin"
        );
        string path=args;
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