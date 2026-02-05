
public class ImageInfoService
{
    private readonly ImageTextRepository conn;
    public ImageInfoService()
    {
        conn=new ImageTextRepository();
                 conn.SetUpDatabase();

    }
    public ImageInfo GetImageInfo(string FilePath)
    {
        return new ImageInfo{
            FileName=Path.GetFileName(FilePath),
            Category=GetCategory(FilePath),
            Size=DirectoryService.FormatSize(FileService.GetFileLength(FilePath)),
            Dimensions=FileService.GetImageDimensions(FilePath),
            CreationTime=FileService.GetFileCreationTime(FilePath),
            Text=GetOrExtractImage(FilePath)
    };
    }
    private string GetCategory(string FilePath)
    {
        string dir=Path.GetDirectoryName(FilePath)??"";
        string category=Path.GetFileName(dir);
        return Categories.categories.Contains(category)?category:"null";
    }
    private string? GetOrExtractImage(string FilePath)
    {
        string text=conn.GetImageText(FilePath);
        if(!string.IsNullOrWhiteSpace(text))
        return text;
        ImageTextExtractor.ExtractSingle(FilePath);
        return conn.GetImageText(FilePath);


    }
}