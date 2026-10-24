using WordlePaint.Core;

namespace WordlePaint.Searcher;

public class Searcher
{
    // using Row.MatchWords():

    [Obsolete]
    public static string[] SearchGuessesThatMatchPatternForGoal(Pattern targetPattern, string goal, out bool succeeded) 
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

    [Obsolete]
    public static int SearchGuessesThatMatchPatternForAllGoals(Pattern targetPattern, Action<Pattern, string, string[], int, int>? solutionAnnouncement)
    {
        int amtOfGoalsWithSolutions = 0;
        for (int i = 0; i < Wordlist.goals.Length; i++)
        {
            var result = SearchGuessesThatMatchPatternForGoal(targetPattern, Wordlist.goals[i], out bool succeeded);
            if (succeeded)
            {
                amtOfGoalsWithSolutions++;
                if (solutionAnnouncement is not null)
                    solutionAnnouncement(targetPattern, Wordlist.goals[i], result, i, amtOfGoalsWithSolutions);
            }
        }
        return amtOfGoalsWithSolutions;
    }

    // using Row.IsSatisfiedBy():

    public static string[] SearchGuessesThatSatisfyPatternForGoal(Pattern targetPattern, string goal, out bool succeeded)
    {
        succeeded = true;
        var result = new string[6];
        for (int i = 0; i < 6; i++)
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

    public static int SearchGuessesThatSatisfyPatternForAllGoals(Pattern targetPattern, Action<Pattern, string, string[], int, int>? solutionAnnouncement)
    {
        int amtOfGoalsWithSolutions = 0;
        for (int i = 0; i < Wordlist.goals.Length; i++)
        {
            var result = SearchGuessesThatSatisfyPatternForGoal(targetPattern, Wordlist.goals[i], out bool succeeded);
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
