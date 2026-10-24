using System.Diagnostics;
using WordlePaint.Core;
using WordlePaint.Searcher;

namespace WordlePaint.Tests;

public class Tests
{
    [Theory]
    [InlineData("rebus", "rebus", LetterColor.Green, LetterColor.Green, LetterColor.Green, LetterColor.Green, LetterColor.Green)]
    [InlineData("store", "robot", LetterColor.Yellow, LetterColor.Yellow, LetterColor.Black, LetterColor.Black, LetterColor.Yellow)]
    [InlineData("robot", "store", LetterColor.Black, LetterColor.Yellow, LetterColor.Yellow, LetterColor.Yellow, LetterColor.Black)]
    [InlineData("beret", "sheet", LetterColor.Black, LetterColor.Black, LetterColor.Yellow, LetterColor.Green, LetterColor.Green)]
    [InlineData("sheet", "beret", LetterColor.Black, LetterColor.Yellow, LetterColor.Black, LetterColor.Green, LetterColor.Green)]
    [InlineData("xxxxe", "eeeep", LetterColor.Yellow, LetterColor.Black, LetterColor.Black, LetterColor.Black, LetterColor.Black)]
    [InlineData("xxxxe", "eeeee", LetterColor.Black, LetterColor.Black, LetterColor.Black, LetterColor.Black, LetterColor.Green)]
    [InlineData("xxxee", "eexxx", LetterColor.Yellow, LetterColor.Yellow, LetterColor.Green, LetterColor.Yellow, LetterColor.Yellow)]
    public void MatchWords(string goal, string guess, LetterColor resultCell1, LetterColor resultCell2, LetterColor resultCell3, LetterColor resultCell4, LetterColor resultCell5)
        => Assert.Equal(new(resultCell1, resultCell2, resultCell3, resultCell4, resultCell5), Row.MatchWords(goal, guess));

    [Theory]
    [InlineData(LetterColor.Green, LetterColor.Green, LetterColor.Green, LetterColor.Green, LetterColor.Green, "rebus", "rebus", true)]
    [InlineData(LetterColor.Green, LetterColor.Green, LetterColor.Yellow, LetterColor.Green, LetterColor.Green, "rebus", "rebus", false)]
    [InlineData(LetterColor.Green, LetterColor.Green, LetterColor.Black, LetterColor.Green, LetterColor.Green, "rebus", "rebus", false)]
    [InlineData(LetterColor.Yellow, LetterColor.Yellow, LetterColor.Black, LetterColor.Black, LetterColor.Yellow, "store", "robot", true)]
    [InlineData(LetterColor.Yellow, LetterColor.Yellow, LetterColor.Black, LetterColor.Yellow, LetterColor.Yellow, "store", "robot", false)]
    [InlineData(LetterColor.Black, LetterColor.Black, LetterColor.Yellow, LetterColor.Green, LetterColor.Green, "beret", "sheet", true)]
    [InlineData(LetterColor.Black, LetterColor.Black, LetterColor.Black, LetterColor.Green, LetterColor.Green, "beret", "sheet", false)]
    [InlineData(LetterColor.Black, LetterColor.Black, LetterColor.Yellow, LetterColor.Yellow, LetterColor.Green, "beret", "sheet", false)]
    public void RowIsSatisfiedBy(LetterColor resultCell1, LetterColor resultCell2, LetterColor resultCell3, LetterColor resultCell4, LetterColor resultCell5, string goal, string guess, bool result)
        => Assert.Equal(new Row(resultCell1, resultCell2, resultCell3, resultCell4, resultCell5).IsSatisfiedBy(goal, guess), result);

    [Fact]
    public void MatchWordsAndIsSatisfiedByAreEquivalent()
        => Assert.Equal(
            Searcher.Searcher.SearchGuessesThatMatchPatternForAllGoals(Pattern.presets["green67"], null),
            Searcher.Searcher.SearchGuessesThatSatisfyPatternForAllGoals(Pattern.presets["green67"], null));
}
