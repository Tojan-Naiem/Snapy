using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

public class FileService
{
    public static long GetFileLength(string filePath)
    {
        try
        {
         FileInfo info=new FileInfo(filePath);
         long length=info.Length;
         return length;

        }catch(Exception e)
        {
        Console.WriteLine("Exception while getting file length: " + e.Message);
        return -1;
         }
    }
    public static string GetLastAccessTime(string filePath)
    {
         try
        {
         FileInfo info=new FileInfo(filePath);
         string date=info.LastAccessTime.ToString("F");
         return date;

        }catch(Exception e)
        {
        Console.WriteLine("Exception while getting file access time: " + e.Message);
        return null;
         }
    }
   
public static (int width, int height) GetImageDimensions(string path)
{
    using (var image = Image.Load<Rgba32>(path))
    {
        return (image.Width, image.Height);
    }
}
public static string GetFileCreationTime(string filePath)
    {
          try
        {
         FileInfo info=new FileInfo(filePath);
         string date=info.CreationTime.ToString("F");
         return date;

        }catch(Exception e)
        {
        Console.WriteLine("Exception while getting file creation time: " + e.Message);
        return null;
         }
    }
    
}