using WordlePaint.Core;

namespace WordlePaint.Search;

public class Searcher
{
    public static string[] GuessesThatSatisfyPatternForGoal(Pattern targetPattern, string goal, out bool succeeded)
    {
        succeeded = true;
        var result = new string[targetPattern.rows.Length];
        for (int i = 0; i < result.Length; i++)
        {
            foreach (var guess in Wordlist.guesses)
            {
                if (!targetPattern.rows[i].IsSatisfiedBy(goal, guess))
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

    public static string[][] AllGuessesThatSatisfyPatternForGoal(Pattern targetPattern, string goal)
    {
        var result = new string[targetPattern.rows.Length][];
        for (int i = 0; i < result.Length; i++)
        {
            List<string> ithRowResult = [];
            foreach (var guess in Wordlist.guesses)
            {
                if (targetPattern.rows[i].IsSatisfiedBy(goal, guess))
                    ithRowResult.Add(guess);
            }
            result[i] = ithRowResult.ToArray();
        }
        return result;
    }

    public static int GuessesThatSatisfyPatternForAllGoals(Pattern targetPattern, Action<Pattern, string, string[], int, int>? solutionAnnouncement)
    {
        int amtOfGoalsWithSolutions = 0;
        for (int i = 0; i < Wordlist.goals.Length; i++)
        {
            var result = GuessesThatSatisfyPatternForGoal(targetPattern, Wordlist.goals[i], out bool succeeded);
            if (succeeded)
            {
                amtOfGoalsWithSolutions++;
                if (solutionAnnouncement is not null)
                    solutionAnnouncement(targetPattern, Wordlist.goals[i], result, i, amtOfGoalsWithSolutions);
            }
        }
        return amtOfGoalsWithSolutions;
    }
}
