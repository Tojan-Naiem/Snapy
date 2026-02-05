public class InfoCommand : ISnapyCommand
{
        public void Execute(string[] args)
    {
         if (args.Length != 2)
        {
            Console.WriteLine("Use the required structure!");
            return;
        }
        string FolderPath = args[1];
        if (!Directory.Exists(FolderPath))
        {
            Console.WriteLine("Invalid Path!");
            return;
        }
    }

}