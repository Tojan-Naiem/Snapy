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
    
}