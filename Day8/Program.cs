// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input8.txt");

Dictionary<string, string[]> maps = new();

string path = lines[0];

for (int i = 2; i < lines.Length; i++)
{
    maps.Add(lines[i].Substring(0, 3), new string[2] { lines[i].Substring(7, 3), lines[i].Substring(12, 3) });
}

List<string> starts = maps.Keys.Where(x => x.EndsWith('A')).ToList();

foreach (var start in starts)
{
    char direction;
    string currentKey = start;

    int nSteps = 0;

    while (!currentKey.EndsWith('Z'))
    {
        foreach (char p in path)
        {
            direction = p;
            nSteps++;
            maps.TryGetValue(currentKey, out string[] paths);
            if (direction == 'R')
            {
                currentKey = paths[1];
            }
            if (direction == 'L')
            {
                currentKey = paths[0];
            }
            if (currentKey.EndsWith('Z'))
            {
                Console.WriteLine(nSteps);
                break;
            }
        }
    }
}

// najdi least common multiple ze vsech
// https://www.calculatorsoup.com/calculators/math/lcm.php?input=13939+11309+20777+15517+17621+18673&data=none&action=solve
