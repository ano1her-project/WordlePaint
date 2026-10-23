using System.Diagnostics;
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
        Console.WriteLine("current target pattern:");
        PrettyPrinter.Print(targetPattern);
        //
        Console.Write("> ");
        var input = Console.ReadLine();
        var subs = input.Split();
        switch (subs[0])
        {
            case "help":
                Console.WriteLine("help                            - Lists all commands.");
                Console.WriteLine("pattern presets                 - Lists all pattern presets.");
                Console.WriteLine("pattern set preset (presetName) - Sets the current target pattern.");
                Console.WriteLine("match (goal) (guess)            - Shows you what a guess would look like for a particular goal, like if you were playing normal Wordle.");
                Console.WriteLine("searchfor (goal)                - Searches for a series of guesses that would create the target pattern for a specific goal word.");
                Console.WriteLine("searchfor all                   - Searches for a series of guesses that would create the target pattern for all goal words, then prints out a summary.");
                break;
            case "pattern":
                if (subs.Length == 2 && subs[1] == "presets")
                {
                    foreach (var kvp in Pattern.presets)
                    {
                        Console.WriteLine(kvp.Key);
                        PrettyPrinter.Print(kvp.Value);
                    }
                }
                else if (subs.Length == 4 && subs[1] == "set" && subs[2] == "preset")
                    targetPattern = Pattern.presets[subs[3]];
                else
                    Console.WriteLine("invalid arguments.");
                break;
            case "match":
                if (subs.Length == 3)
                    PrettyPrinter.Print(Row.MatchWords(subs[1], subs[2]), true);
                else
                    Console.WriteLine("invalid arguments.");
                break;
            case "searchfor":
                if (subs.Length != 2)
                {
                    Console.WriteLine("invalid arguments.");
                    break;
                }
                if (subs[1] == "all")
                {
                    int amtOfGoalWithSolutions = 0;
                    var sw = Stopwatch.StartNew();
                    for (int i = 0; i < Wordlist.goals.Length; i++)
                    {
                        bool succeeded = false;
                        var result = SearchGuessesToSatisfyPatternForGoal(Wordlist.goals[i], out succeeded);
                        if (succeeded)
                        {
                            amtOfGoalWithSolutions++;
                            Console.WriteLine($"found one! - {Wordlist.goals[i]} - word {i}/{Wordlist.goals.Length} in the list. it's the {amtOfGoalWithSolutions}th one.");
                            PrettyPrinter.Print(targetPattern, result);
                        }
                    }
                    var time = sw.Elapsed;
                    Console.WriteLine($"finished in {time}:");
                    Console.WriteLine($"found solutions for {amtOfGoalWithSolutions}/{Wordlist.goals.Length} goals.");
                }
                else
                {
                    var sw = Stopwatch.StartNew();
                    var result = SearchGuessesToSatisfyPatternForGoal(subs[1], out _);
                    var time = sw.Elapsed;
                    Console.WriteLine($"found in {time}:");
                    Console.WriteLine(subs[1]);
                    PrettyPrinter.Print(targetPattern, result);
                }
                break;
            default:
                Console.WriteLine("invalid command.");
                break;
        }
    }

    static string[] SearchGuessesToSatisfyPatternForGoal(string goal, out bool succeeded)
    {
        succeeded = true;
        var result = new string[6];
        for (int i = 0; i < 6; i++)
        {
            foreach (var guess in Wordlist.guesses)
            {
                if (!Row.MatchWords(goal, guess).Equals(targetPattern.rows[i]))
                    continue;
                result[i] = guess;
                break;
            }
            if (result[i] is null)
            {
                result[i] = "_";
                succeeded = false;
            }
        }
        return result;
    }
}
