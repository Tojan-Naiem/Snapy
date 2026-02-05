public class CategoryDirectoryService : DirectoryService
{
    public static bool CheckDirectories(string folderPath)
    {
        foreach(var s in Categories.categories)
        if(!Directory.Exists(folderPath+"/"+s))
         return false;
        return true;
        
    }
    public static void DeleteAllCategoryDirectories(string folderPath)
    {
        foreach(var s in Categories.categories)
        {
            string dirPath=folderPath+"/"+s;
            string []myFiles=Directory.GetFiles(dirPath);
        foreach(var filePath in myFiles)
        {
           string newPathFile=folderPath+"/"+filePath.Substring(filePath.LastIndexOf("/"));
           File.Copy(filePath,newPathFile);
           File.Delete(filePath);
        }
        Directory.Delete(dirPath);
        }

    }
}