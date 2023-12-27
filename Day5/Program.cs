// See https://aka.ms/new-console-template for more information
using Day5;
using Day5.Part2;

Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input5.txt");
//var lines = File.ReadAllLines("input5test.txt");

Console.WriteLine(Part1.Run(lines));
Console.WriteLine(Part1_5.Run(lines));
//Console.WriteLine(Part2.Run(lines));

