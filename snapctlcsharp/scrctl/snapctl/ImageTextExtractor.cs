using System.Data.Common;
using Tesseract;

public class ImageTextExtractor
{
    public static void Extract(string folderPath)
    {
        try
        {
            DBConnection connection=new DBConnection();
            connection.SetUpDatabase();
            string [] myFiles=Directory.GetFiles(folderPath);
            foreach(var file in myFiles)
            {
                var text =ExtractTextFromImage(file,"eng+ara");
                connection.InsertData(text);
            }
            
        }catch(Exception e)
        {
            Console.WriteLine("Exception in the extract method in ImageTextExtractor "+e.GetBaseException());
        }

        
    }
    public static string ExtractTextFromImage(string filePath,string lang)
    {
        // for extract image with each language
        string tessPath="/home/tojan/Documents/Python Projects/snapctl/tessdata";
       using (var engine = new TesseractEngine(tessPath, lang, EngineMode.Default, "liblept.so.5"))
        {
            using (Pix pix = Pix.LoadFromFile(filePath))
        {
            using (Tesseract.Page page = engine.Process(pix))
            {
                return page.GetText();
            }
        }
        }
    }
}