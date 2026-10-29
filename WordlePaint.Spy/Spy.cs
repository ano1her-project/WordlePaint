using WordlePaint.Core;
using WordlePaint.Search;

namespace WordlePaint.Spy;

public class Spy
{
    static readonly Day[] history = ParseDataFile("D:\\Soubory\\Dokumenty\\C# Projekty\\WordlePaint\\WordlePaint.Spy\\data.txt");

    static Day[] ParseDataFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        List<Day> result = [];
        List<PlayersPattern> dayPatterns = [];
        List<Row> rows = [];
        foreach (var line in lines)
        {
            if (line.StartsWith("end pattern"))
            {
                var words = line.Split();
                if (words.Length != 3)
                    throw new InvalidDataException();
                dayPatterns.Add(new(words[2], new(rows.ToArray())));
                rows = [];
                continue;
            }
            if (line == "end day")
            {
                result.Add(new(dayPatterns.ToArray()));
                dayPatterns = [];
                continue;
            }
            // else
            rows.Add(Row.FromString(line));
        }
        return result.ToArray();
    }

    static string[] ReverseEngineerPossibleGoalsOfDay(Day day, out Dictionary<string, string[]> possibleFirstGuessesByPlayerName)
    {
        List<string> possibleGoals = [];
        possibleFirstGuessesByPlayerName = [];
        foreach (var goal in Wordlist.goals)
        {
            Dictionary<string, string[]> firstGuessesByPlayerNameForGoal = [];
            bool goalSucceeded = true;
            foreach (var guessPattern in day.patterns)
            {
                var guesses = Searcher.AllGuessesThatSatisfyPatternForGoal(guessPattern.pattern, goal, out var goalSucceededForPattern);
                firstGuessesByPlayerNameForGoal.Add(guessPattern.playerName, guesses[0]);
                if (!goalSucceededForPattern)
                {
                    goalSucceeded = false;
                    break;
                }
            }
            if (goalSucceeded)
            {
                possibleGoals.Add(goal);
                possibleFirstGuessesByPlayerName = Combine(possibleFirstGuessesByPlayerName, firstGuessesByPlayerNameForGoal);
            }            
        }
        return possibleGoals.ToArray();
    }

    public static string[] ReverseEngineerLatestGoal()
    {        
        Dictionary<string, string[]> allPossibleFirstGuessesEverByPlayerName = [];
        for (int i = 0; i < history.Length - 1; i++)
        {
            ReverseEngineerPossibleGoalsOfDay(history[i], out var possibleFirstGuessesByPlayerName);
            allPossibleFirstGuessesEverByPlayerName = Combine(allPossibleFirstGuessesEverByPlayerName, possibleFirstGuessesByPlayerName);
        }
        //
        List<string> possibleGoalsToday = [];
        foreach (var goal in Wordlist.goals)
        {
            bool goalSucceeded = true;
            foreach (var guessPattern in history[^1].patterns)
            {
                var guesses = Searcher.AllGuessesThatSatisfyPatternForGoal(guessPattern.pattern, goal, out var goalSucceededForPattern);
                bool firstGuessesMatchHistory = false;
                foreach (var firstGuess in guesses[0])
                {
                    if (allPossibleFirstGuessesEverByPlayerName.ContainsKey(guessPattern.playerName) && 
                        allPossibleFirstGuessesEverByPlayerName[guessPattern.playerName].Contains(firstGuess))
                    {
                        firstGuessesMatchHistory = true;
                        break;
                    }
                }
                if (!goalSucceededForPattern || !firstGuessesMatchHistory)
                {
                    goalSucceeded = false;
                    break;
                }
            }
            if (goalSucceeded)
                possibleGoalsToday.Add(goal);
        }
        return possibleGoalsToday.ToArray();
    }

    //

    public static Dictionary<string, string[]> Combine(Dictionary<string, string[]> a, Dictionary<string, string[]> b)
    {
        Dictionary<string, string[]> result = [];
        foreach (var aKvp in a)
        {
            if (b.TryGetValue(aKvp.Key, out var bValues))
                result[aKvp.Key] = aKvp.Value.Concat(bValues).ToArray();
            else
                result[aKvp.Key] = aKvp.Value;
        }
        foreach (var bKvp in b) // only keys that aren't in a
            result.TryAdd(bKvp.Key, bKvp.Value);
        return result;
    }
}
