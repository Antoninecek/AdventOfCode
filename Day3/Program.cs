// See https://aka.ms/new-console-template for more information
using Day3;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input3.txt");

int sum = 0;

for (int i = 0; i < lines.Length; i++)
{
    string line = lines[i];
    char[] chars = line.ToCharArray();
    string theNumber = string.Empty;
    bool goodNumber = false;

    for (int j = 0; j < line.Length; j++)
    {
        if (int.TryParse(chars[j].ToString(), out int res))
        {
            theNumber += res;
            if (!goodNumber && IsSymbolAdjacent(lines, i, j))
            {
                goodNumber = true;
            }
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(theNumber) && goodNumber)
            {
                sum += int.Parse(theNumber);
            }
            goodNumber = false;
            theNumber = string.Empty;
        }
    }
    if (!string.IsNullOrWhiteSpace(theNumber) && goodNumber)
    {
        sum += int.Parse(theNumber);
    }
}
Console.WriteLine("part 1 " + sum);
Console.WriteLine("part 2 " + Part2.Run(lines));



static bool IsSymbolAdjacent(string[] lines, int i, int j)
{
    // nahore vlevo
    if (i != 0 && j != 0 && !IsNumberOrDot(lines[i - 1][j - 1])) return true;
    // nahore
    if (i != 0 && !IsNumberOrDot(lines[i - 1][j])) return true;
    // nahore vpravo
    if (i != 0 && j != lines.Length - 1 && !IsNumberOrDot(lines[i - 1][j + 1])) return true;

    // vlevo
    if (j != 0 && !IsNumberOrDot(lines[i][j - 1])) return true;
    // vpravo
    if (j != lines.Length - 1 && !IsNumberOrDot(lines[i][j + 1])) return true;

    // dole vlevo
    if (j != 0 && i != lines.Length - 1 && !IsNumberOrDot(lines[i + 1][j - 1])) return true;
    // dole
    if (i != lines.Length - 1 && !IsNumberOrDot(lines[i + 1][j])) return true;
    // dole vpravo
    if (i != lines.Length - 1 && j != lines.Length - 1 && !IsNumberOrDot(lines[i + 1][j + 1])) return true;

    return false;
}


static bool IsNumberOrDot(char c)
{
    return c == '.' || Regex.Match(c.ToString(), @"[0-9]").Success;
}