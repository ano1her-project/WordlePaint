using WordlePaint.Core;

namespace WordlePaint.Interface;

internal class Program
{
    static Pattern targetPattern = Pattern.presets["green67"];

    static void Main(string[] args)
    {
        while (true)
            CommandLoop();
    }

    static void CommandLoop()
    {
        Console.Write("> ");
        var input = Console.ReadLine();
    }
}
