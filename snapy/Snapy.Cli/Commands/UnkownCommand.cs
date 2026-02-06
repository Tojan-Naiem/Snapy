public class UnknownCommand:ISnapyCommand
{
        public void Execute(string[] args)
    {
        Console.WriteLine("Unknown command");
    }

}