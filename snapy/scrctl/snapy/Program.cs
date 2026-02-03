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
     
        if(args.Length>0)
        {
            if (args[0] == "snapy")
            {
            ICommand command=null;
            if (args[1].ToUpper()=="ORGANIZE")
            {
                command=new OrganizeCommand();
            }
            else if(args[1].ToUpper()=="SEARCH")
            {
                    command=new SearchCommand();    
            }
            else if(args[1].ToUpper()=="RESTART")
                command=new RestartCommand();



            if(command!=null)
            command.Execute(args);
            }
           
        }
        
    }
    

}