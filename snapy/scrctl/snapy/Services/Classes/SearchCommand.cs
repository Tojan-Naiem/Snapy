
public class SearchCommand : ICommand
{
        public void Execute(string[] args)
    {
        if (args.Length !=5||args[3].ToUpper() != "FROM")
        {
            Console.WriteLine("Use the required structure!");
            return ;
        }
        string FolderPath=args[4];
        string searchData=args[2];
      
        if (!Directory.Exists(FolderPath))
        {
            Console.WriteLine("Invalid Path!");
            return ;
        }
        ImageTextExtractor.Extract(FolderPath);
        ImageTextRepository.SearchTextFromDBS(searchData);

    }

}