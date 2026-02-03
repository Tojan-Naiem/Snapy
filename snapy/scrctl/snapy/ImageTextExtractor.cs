using System;
using System.IO;
using System.Runtime.InteropServices;
using Tesseract;
using System.Diagnostics;

public class ImageTextExtractor
{
    static ImageTextExtractor()
    {
        // Force load the real .so files before Tesseract's interop loader runs
        NativeLibrary.Load("/usr/lib/x86_64-linux-gnu/liblept.so.5");
        NativeLibrary.Load("/usr/lib/x86_64-linux-gnu/libtesseract.so.5");
    }

    public static void Extract(string folderPath)
    {
        try
        {
            DBConnection connection = new DBConnection();
            connection.SetUpDatabase();
            string[] myFiles = Directory.GetFiles(folderPath);
            foreach (var file in myFiles)
            {
                if (!connection.IsImageProcessed(file))
                {
                    Console.WriteLine("I'm in at : "+file);
                    var text = ExtractTextFromImage(file, "eng+ara");
                    connection.InsertData(file, text);
                }

            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception in the extract method in ImageTextExtractor " + e.GetBaseException());
        }
    }

    public static string ExtractTextFromImage(string filePath, string lang)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "tesseract",
            Arguments = $"\"{filePath}\" stdout -l {lang}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(psi);
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (process.ExitCode != 0)
            throw new Exception($"Tesseract error: {error}");

        return output;
    }
}