public class ImageInfo
{
    public string FileName { get; set; }
    public string Category { get; set; }
    public string Size { get; set; }
    public (int width, int height) Dimensions { get; set; }
    public string CreationTime { get; set; }
    public string? Text { get; set; }


}