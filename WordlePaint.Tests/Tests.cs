using WordlePaint.Core;

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
}
