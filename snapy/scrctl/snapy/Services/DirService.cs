
public class DirService
{
    public static int CountFiles(string dirPath)
    {
        return Directory.EnumerateFiles(dirPath, "*", SearchOption.AllDirectories).Count();
    }
    public static string TotoalFilesLength(string dirPath)
    {
        long totalLength = 0;

        try
        {
            string[] myFiles = Directory.GetFiles(dirPath);
            foreach (var file in Directory.EnumerateFiles(dirPath, "*", SearchOption.AllDirectories))
            {
                totalLength += FileService.GetFileLength(file);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception while getting files length: " + e.Message);
        }

        return FormatSize(totalLength);
    }
    public static string FormatSize(long bytes)
    {
        double size=bytes;
        string []units={"B","KB","MB","GB","TB"};
        int unit=0;
        while (size >= 1024 && unit < units.Length - 1)
        {
            size/=1024;
            unit++;
        }
        return $"{size:F2}{units[unit]}";
    }
}