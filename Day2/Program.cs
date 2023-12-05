// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input2.txt");
int i = 0;
int sum = 0;
foreach (var line in lines)
{
    i++;

    int a = Regex.Matches(line, @"([0-9]*) blue").Max(x => int.Parse(Regex.Match(x.Value, @"[0-9]*").Value));
    int b = Regex.Matches(line, @"([0-9]*) red").Max(x => int.Parse(Regex.Match(x.Value, @"[0-9]*").Value));
    int c = Regex.Matches(line, @"([0-9]*) green").Max(x => int.Parse(Regex.Match(x.Value, @"[0-9]*").Value));

    sum += a * b * c;
}

Console.WriteLine(sum);