// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input.txt");
int sum = 0;
Dictionary<string, int> groups = new() { { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 }, { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 } };
foreach (var line in lines)
{
    var matchesL = Regex.Matches(line, @"([0-9]|" + string.Join("|", groups.Keys) + ")");
    var matchesR = Regex.Matches(line, @"([0-9]|" + string.Join("|", groups.Keys) + ")", RegexOptions.RightToLeft);
    string res = "";
    if (int.TryParse(matchesL.First().Value, out int res1))
    {
        res += res1;
    }
    else
    {
        res += groups[matchesL.First().Value];
    }
    if (int.TryParse(matchesR.First().Value, out int res2))
    {
        res += res2;
    }
    else
    {
        res += groups[matchesR.First().Value];
    }
    sum += int.Parse(res);
}
Console.WriteLine(sum);