using System.IO;

namespace WordlePaint.Core;

public class Wordlist
{
    public readonly static string[] goals = File.ReadAllLines("D:\\Soubory\\Dokumenty\\C# Projekty\\WordlePaint\\WordlePaint.Core\\wordle-answers-alphabetical.txt");
    public readonly static string[] guesses = File.ReadAllLines("D:\\Soubory\\Dokumenty\\C# Projekty\\WordlePaint\\WordlePaint.Core\\wordle-allowed-guesses.txt").Concat(goals).ToArray();
}