// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input9.txt");

int sum = 0;
foreach (var line in lines)
{
    List<List<int>> histories = new();

    histories.Add(line.Split(' ').Select(int.Parse).ToList());
    while (!histories.Last().All(x => x == 0))
    {
        histories.Add(GetHistoryLine(histories.Last()));
    }

    histories.Last().Add(0);
    for (int i = histories.Count - 2; i >= 0; i--)
    {
        histories[i].Add(histories[i + 1].Last() + histories[i].Last());
    }
    sum += histories.First().Last();
}

Console.WriteLine(sum);

static List<int> GetHistoryLine(List<int> line)
{
    List<int> res = new();
    for (int i = 0; i < line.Count - 1; i++)
    {
        res.Add(line[i + 1] - line[i]);
    }
    return res;
}