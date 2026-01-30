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
            int startIndex=2,count=startIndex-1;
            if (args[1] == "organize")
            {
                command=new OrganizeCommand();
                count=startIndex-1;
            }
                else if(args[1]=="search")
                {
                    DBConnection dBConnection=new DBConnection();
                    dBConnection.SetUpDatabase();
                    command=new SearchCommand();
                    

                }


            if(command!=null)
            command.Execute(string.Join(" ",args,startIndex,count));
            }
           
        }
        
    }
    

}