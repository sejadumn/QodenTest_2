using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Dictionary<string, double> CalculateRelativeFrequencies(string text)
    {
        string[] words = text.Split();

        Dictionary<string, int> wordCounts = new Dictionary<string, int>();

        foreach (string word in words)
        {
            if (wordCounts.ContainsKey(word))
                wordCounts[word]++;
            else
                wordCounts[word] = 1;
        }

        int totalWords = words.Length;

        Dictionary<string, double> relativeFrequencies = new Dictionary<string, double>();

        foreach (KeyValuePair<string, int> pair in wordCounts)
        {
            double frequency = (double)pair.Value / totalWords;
            relativeFrequencies[pair.Key] = frequency;
        }

        return relativeFrequencies;
    }

    static string GenerateTextualDiagram(Dictionary<string, double> relativeFrequencies)
    {
        var sortedWords = relativeFrequencies.OrderBy(pair => pair.Value);

        int maxWordLength = sortedWords.Max(pair => pair.Key.Length);

        int maxPointsWidth = 10;

        string diagram = "";

        foreach (KeyValuePair<string, double> pair in sortedWords)
        {
            string word = pair.Key;
            double frequency = pair.Value;

            int numPoints = (int)Math.Ceiling(frequency * maxPointsWidth);

            string points = new string('.', numPoints).PadRight(maxPointsWidth, '.');

            string paddedWord = word.PadLeft(maxWordLength, '_');

            diagram += $"{paddedWord} {points}\n";
        }

        return diagram;
    }

    static void Main(string[] args)
    {
        string input = Console.ReadLine();
        string[] words = input.Split();

        if (words.Length > 10000)
        {
            Console.WriteLine("Ошибка: количество слов превышает максимально допустимое значение 10000.");
            return;
        }

        Dictionary<string, double> relativeFrequencies = CalculateRelativeFrequencies(input);

        string diagram = GenerateTextualDiagram(relativeFrequencies);

        Console.WriteLine(diagram);
    }
}
