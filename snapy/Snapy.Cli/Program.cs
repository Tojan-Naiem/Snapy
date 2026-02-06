using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
public class Program
{
    public static void Main(string[] args)
    {

        Console.WriteLine("\n  ═══════════════════════════════════════════");
        Console.WriteLine("  SNAPY - Screenshot Organizer");
        Console.WriteLine("  ═══════════════════════════════════════════\n");

        string setUpMarker = Path.Combine(AppContext.BaseDirectory, ".setup_complete");
   
        if (!File.Exists(setUpMarker))
        {
            Console.WriteLine("  Setup not completed!");
            Console.WriteLine("  Please run './setup.sh' first.\n");
            return;
        }
        if (args.Length == 0)
        {
            ShowHelp();
            return;
        }
        string commandKey=args[0].ToUpper();
        ISnapyCommand command = args[0].ToUpper() switch
        {
            "ORGANIZE" => new OrganizeCommand(),
            "SEARCH" => new SearchCommand(),
            "RESTART" => new RestartCommand(),
            "STATS"=>new StatsCommand(),
            "INFO"=>new InfoCommand(),
            _=>new UnknownCommand()
        };
        if (command == null)
        {
            Console.WriteLine("Unknown command : " + args[0]);
            ShowHelp();
            return;
        }


        try
        {

            command.Execute(args);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n  Error: {ex.Message}");

            Console.WriteLine($"  Stack trace: {ex.StackTrace}\n");
        }





    }

    private static void ShowHelp()
    {
        Console.WriteLine("  Available commands:");
        Console.WriteLine("    snapy organize <path>        - Organize screenshots");
        Console.WriteLine("    snapy search <text> ");
        Console.WriteLine("    snapy restart <path>         - Undo organization");
        Console.WriteLine();
    }
}