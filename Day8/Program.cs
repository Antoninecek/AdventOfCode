// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input8.txt");

Dictionary<string, string[]> maps = new();

string path = lines[0];

for (int i = 2; i < lines.Length; i++)
{
    maps.Add(lines[i].Substring(0, 3), new string[2] { lines[i].Substring(7, 3), lines[i].Substring(12, 3) });
}

char direction;
string currentKey = "AAA";

int nSteps = 0;

while (currentKey != "ZZZ")
{
    foreach(char p in path)
    {
        direction = p;
        nSteps++;
        maps.TryGetValue(currentKey, out string[] paths);
        if(direction == 'R')
        {
            currentKey = paths[1];
        }
        if(direction == 'L')
        {
            currentKey = paths[0];
        }
        if(currentKey == "ZZZ")
        {
            break;
        }
    }
}

Console.WriteLine(nSteps);