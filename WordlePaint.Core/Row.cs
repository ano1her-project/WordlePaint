using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WordlePaint.Core;

public class Row
{
    public LetterColor[] cells = new LetterColor[5];

    public Row(LetterColor[] p_cells)
    {
        cells = p_cells;
    }

    public Row (LetterColor cell1, LetterColor cell2, LetterColor cell3, LetterColor cell4, LetterColor cell5) : this([cell1, cell2, cell3, cell4, cell5]) { }

    public override bool Equals(object? obj)
    {
        var other = obj as Row;
        if (cells.Length != other.cells.Length)
            return false;
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i] != other.cells[i])
                return false;
        }
        return true;
    }

    public static Row FromString(string s)
    {
        if (s.Length != 5)
            throw new Exception();
        LetterColor[] result = new LetterColor[5];
        for (int i = 0; i < 5; i++)
        {
            result[i] = s[i] switch
            {
                'B' => LetterColor.Black,
                'X' => LetterColor.Black,
                'Y' => LetterColor.Yellow,
                'G' => LetterColor.Green,
                _ => throw new Exception()
            };
        }
        return new(result);
    }

    public static Row MatchWords(string goal, string guess)
    {
        LetterColor[] result = new LetterColor[5];
        var remainingLetters = goal.ToCharArray().ToList();
        // first pass - check for greens
        for (int i = 0; i < 5; i++)
        {
            if (guess[i] != goal[i])
                continue;
            if (!remainingLetters.Contains(guess[i]))
                throw new UnreachableException($"The goal contains the letter '{guess[i]}' while the remaining letters list does not. (1st pass)");
            result[i] = LetterColor.Green;
            remainingLetters.Remove(guess[i]);
        }
        // second pass - color yellows and blacks
        for (int i = 0; i < 5; i++)
        {
            if (result[i] == LetterColor.Green) // already handled, skip
                continue;
            if (remainingLetters.Contains(guess[i]))
            {
                if (!goal.Contains(guess[i]))
                    throw new UnreachableException($"The remaining letters list contains the letter '{guess[i]}' while the goal does not. (2nd pass)");
                if (guess[i] == goal[i])
                    throw new UnreachableException($"The letter '{guess[i]}' in the guess is in the correct spot but wasn't caught in the first pass. (2nd pass)");
                result[i] = LetterColor.Yellow;
                remainingLetters.Remove(guess[i]);
            }
            else
                result[i] = LetterColor.Black;
        }
        // we have to do this so that guesses with duplicate letters are colored correctly.
        return new Row(result);
    }
}
