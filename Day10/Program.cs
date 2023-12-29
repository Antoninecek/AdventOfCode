// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input10.txt");

char[][] map = new char[lines.Length][];

int indexI = -1;
int indexJ = -1;

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        char c = lines[i][j];
        if (indexI == -1 && c == 'S')
        {
            indexI = i;
            indexJ = j;
        }
        map[i][j] = c;
    }
}

int lastIndexI = -1;
int lastIndexJ = -1;

while (true)
{
    char currentTile = lines[indexI][indexJ];

    // ziskej napojeny tiles

    char? topTile, leftTile, rightTile, bottomTile;
    bool isConnected = false;

    // tile ziskat jen pro kontrolu S
    // uchovavat predchozi index, abych vedel, kam jit
    lastIndexI = indexI;
    lastIndexJ = indexJ;

    switch (currentTile)
    {
        case '|':
            indexI = lastIndexI == indexI - 1 ? indexI + 1 : indexI - 1;
            break;
        case '-':
            indexJ = lastIndexJ == indexJ - 1 ? indexJ + 1 : indexJ - 1;
            break;
        case 'L':
            if (lastIndexI == indexI - 1)
            {
                indexJ += 1;
            }
            else
            {
                indexI -= 1;
            }
            break;
        case 'J':
            if (lastIndexI == indexI - 1)
            {
                indexJ -= 1;
            }
            else
            {
                indexI -= 1;
            }
            break;
        case '7':
            if (lastIndexI == indexI + 1)
            {
                indexJ -= 1;
            }
            else
            {
                indexI += 1;
            }
            break;
        case 'F':
            if (lastIndexI == indexI + 1)
            {
                indexJ += 1;
            }
            else
            {
                indexI += 1;
            }
            break;
        case '.':
            throw new ArgumentException();
        case 'S':
            // tady zjistit, ktera je napojena

            topTile = GetTile(indexI - 1, indexJ, map);
            bottomTile = GetTile(indexI + 1, indexJ, map);
            leftTile = GetTile(indexI, indexJ - 1, map);
            rightTile = GetTile(indexI, indexJ + 1, map);
            break;
    }
    // pokud jsem zpatky na startu, jsem na konci
    GetTile(indexI, indexJ, map);
}

static char? GetTile(int i, int j, char[][] map)
{
    try
    {
        return map[i][j];
    }
    catch (Exception)
    {
        return null;
    }
}