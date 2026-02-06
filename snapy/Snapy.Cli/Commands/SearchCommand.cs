
public class SearchCommand : ISnapyCommand
{
        public void Execute(string[] args)
    {
        if (args.Length >3)
        {
            Console.WriteLine("Use the required structure!");
            return ;
        }
        string searchData=args[1];
        SqliteRepository.SearchTextFromDBS(searchData);

    }

}