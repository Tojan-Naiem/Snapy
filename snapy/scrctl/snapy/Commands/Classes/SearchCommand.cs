
public class SearchCommand : ISnapyCommand
{
        public void Execute(string[] args)
    {
        if (args.Length !=4||args[2].ToUpper() != "FROM")
        {
            Console.WriteLine("Use the required structure!");
            return ;
        }
        string FolderPath=args[3];
        string searchData=args[1];
      
        if (!Directory.Exists(FolderPath))
        {
            Console.WriteLine("Invalid Path!");
            return ;
        }
        ImageTextRepository.SearchTextFromDBS(searchData);

    }

}