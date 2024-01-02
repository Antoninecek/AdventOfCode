// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input14.txt");

Dictionary<int, int> colRocksCount = new();
for(int i = 0; i < lines[0].Length; i++)
{
    int koef = lines.Length;
    colRocksCount.Add(i, 0);
    for(int j = 0; j < lines.Length; j++)
    {
        char tile = lines[j][i];
        if(tile == 'O')
        {
            colRocksCount[i] += koef;
            koef--;
        }
        if (tile == '#') koef = lines.Length - j - 1;
    }
}

Console.WriteLine(colRocksCount.Values.Sum());