public class InfoCommand : ISnapyCommand
{
    public void Execute(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Use the required structure!");
            return;
        }
        string FilePath = args[1];
        // if (!File.Exists(FilePath))
        // {
        //     Console.WriteLine("Invalid Path!");
        //     return;
        // }
        ImageInfoService info=new ImageInfoService();
        ImageInfo imgInfo=info.GetImageInfo(FilePath);
        Console.WriteLine($"File name : {imgInfo.FileName}");
        Console.WriteLine($"Category name : {imgInfo.Category}");
        Console.WriteLine($"size :{imgInfo.Size}");
        Console.WriteLine($"Dimensions : {imgInfo.Dimensions}");
        Console.WriteLine($"Creation time : {imgInfo.CreationTime}");
         Console.WriteLine($"Text  : [{imgInfo.Text}]");





    }

}