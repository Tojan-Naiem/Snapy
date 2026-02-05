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

    
}