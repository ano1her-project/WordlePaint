using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordlePaint.Core;

namespace WordlePaint.Interface;

public class PrettyPrinter
{
    static readonly string box = "██";

    public static void Print(Row row, bool writeLine)
    {
        for (int i = 0; i < row.cells.Length; i++)
        {
            Console.ForegroundColor = row.cells[i] switch { 
                LetterColor.Black => ConsoleColor.DarkGray,
                LetterColor.Yellow => ConsoleColor.Yellow,
                LetterColor.Green => ConsoleColor.Green
            };
            Console.Write(box);
        }
        if (writeLine)
            Console.WriteLine();
        Console.ResetColor();
    }

    public static void Print(Pattern pattern)
    {
        for (int i = 0; i < pattern.rows.Length; i++)
            Print(pattern.rows[i], true);
        Console.ResetColor();
    }

    public static void Print(Pattern pattern, string[] guesses)
    {
        if (pattern.rows.Length != guesses.Length)
            throw new Exception();
        for (int i = 0; i < pattern.rows.Length; i++)
        {
            Print(pattern.rows[i], false);
            Console.WriteLine($" {guesses[i]}");
        }
    }
}
