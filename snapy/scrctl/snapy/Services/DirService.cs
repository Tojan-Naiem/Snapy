public class DirService
{
    public static int CountFiles(string dirPath){
       return Directory.EnumerateFiles(dirPath,"*",SearchOption.AllDirectories).Count();
    }
}