// See https://aka.ms/new-console-template for more information
using Day6;

Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input6.txt");
List<decimal> times = new List<decimal>() { 53, 71, 78, 80 };
List<decimal> distances = new List<decimal>() { 275, 1181, 1215, 1524 };

List<TimeDistance> pairs = new();

for (int i = 0; i < times.Count; i++)
{
    pairs.Add(new() { Distance = distances[i], Time = times[i] });
}

Console.WriteLine(Part1.Run(pairs));

pairs = new() { new() { Distance = 275118112151524, Time = 53717880 } };
Console.WriteLine(Part1.Run(pairs));

public class TimeDistance
{
    public decimal Distance { get; set; }
    public decimal Time { get; set; }
}