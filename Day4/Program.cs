// See https://aka.ms/new-console-template for more information
using Day4;

Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input4.txt");
//lines = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
//Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
//Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
//Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
//Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
//Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11".Split("\r\n");

int sum = 0;

foreach (string line in lines)
{
    string justNumbers = line.Substring(line.IndexOf(":") + 1);
    var winning = justNumbers.Split('|').First().Split(" ").Where(x => x != string.Empty);
    var my = justNumbers.Split('|').Last().Split(" ").Where(x => x != string.Empty);

    sum += (int)Math.Pow(2, winning.Intersect(my).Count()-1);
}

Console.WriteLine(sum);
Console.WriteLine(Part2.Run(lines));