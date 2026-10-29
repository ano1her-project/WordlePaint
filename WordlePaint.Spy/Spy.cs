using WordlePaint.Core;
using WordlePaint.Search;

namespace WordlePaint.Spy;

public class Spy
{
    static readonly Pattern[][] history = ParseDataFile("");

    static Pattern[][] ParseDataFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        List<Pattern[]> result = [];
        List<Pattern> day = [];
        List<Row> rows = [];
        foreach (var line in lines)
        {
            if (line == "end pattern")
            {
                day.Add(new(rows.ToArray()));
                rows = [];
                continue;
            }
            if (line == "end day")
            {
                result.Add(day.ToArray());
                day = [];
                continue;
            }
            rows.Add(Row.FromString(line));
        }
        return result.ToArray();
    }

    static string[] ReverseEngineerPossibleGoalsOfDay(Pattern[] day)
    {
        List<string> possibleGoals = [];
        foreach (var goal in Wordlist.goals)
        {
            foreach (var guessPattern in day)
            {
                var guesses = Searcher.GuessesThatSatisfyPatternForGoal(guessPattern, goal, out var succeeded);
                if (!succeeded)
                    break;

            }
            possibleGoals.Add(goal);
        }
        return possibleGoals.ToArray();
    }
}
