// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input10.txt");

char[][] map = new char[lines.Length][];

int indexI = -1;
int indexJ = -1;

for (int i = 0; i < lines.Length; i++)
{
    map[i] = new char[lines[i].Length];
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
int nIndex;

bool firstRound = true;
int nTiles = 0;

while (true)
{
    nTiles++;
    char currentTile = lines[indexI][indexJ];
    Console.WriteLine(nTiles + " " + currentTile + " " + indexI + " " + indexJ);

    // ziskej napojeny tiles

    char? topTile, leftTile, rightTile, bottomTile;

    switch (currentTile)
    {
        case '|':
            nIndex = lastIndexI == indexI - 1 ? indexI + 1 : indexI - 1;
            lastIndexI = indexI;
            lastIndexJ = indexJ;
            indexI = nIndex;
            break;
        case '-':
            nIndex = lastIndexJ == indexJ - 1 ? indexJ + 1 : indexJ - 1;
            lastIndexI = indexI;
            lastIndexJ = indexJ;
            indexJ = nIndex;
            break;
        case 'L':
            if (lastIndexI == indexI - 1)
            {
                lastIndexI = indexI;
                lastIndexJ = indexJ;
                indexJ += 1;
            }
            else
            {
                lastIndexI = indexI;
                lastIndexJ = indexJ;
                indexI -= 1;
            }
            break;
        case 'J':
            if (lastIndexI == indexI - 1)
            {
                lastIndexI = indexI;
                lastIndexJ = indexJ;
                indexJ -= 1;
            }
            else
            {
                lastIndexI = indexI;
                lastIndexJ = indexJ;
                indexI -= 1;
            }
            break;
        case '7':
            if (lastIndexI == indexI + 1)
            {
                lastIndexI = indexI;
                lastIndexJ = indexJ;
                indexJ -= 1;
            }
            else
            {
                lastIndexI = indexI;
                lastIndexJ = indexJ;
                indexI += 1;
            }
            break;
        case 'F':
            if (lastIndexI == indexI + 1)
            {
                lastIndexI = indexI;
                lastIndexJ = indexJ;
                indexJ += 1;
            }
            else
            {
                lastIndexI = indexI;
                lastIndexJ = indexJ;
                indexI += 1;
            }
            break;
        case '.':
            throw new ArgumentException();
        case 'S':
            // tady zjistit, ktera je napojena
            firstRound = false;
            topTile = GetTile(indexI - 1, indexJ, map);
            if (topTile == '|' || topTile == '7' || topTile == 'F')
            {
                lastIndexI = indexI;
                lastIndexJ = indexJ;
                indexI = indexI - 1;
                break;
            }
            bottomTile = GetTile(indexI + 1, indexJ, map);
            if (bottomTile == '|' || bottomTile == 'L' || bottomTile == 'J')
            {
                lastIndexI = indexI;
                lastIndexJ = indexJ;
                indexI = indexI + 1;
                break;
            }
            leftTile = GetTile(indexI, indexJ - 1, map);
            if (leftTile == '-' || leftTile == 'L' || leftTile == 'F')
            {
                lastIndexI = indexI;
                lastIndexJ = indexJ;
                indexJ = indexJ - 1;
                break;
            }
            rightTile = GetTile(indexI, indexJ + 1, map);
            if (rightTile == '-' || rightTile == 'J' || rightTile == '7')
            {
                lastIndexI = indexI;
                lastIndexJ = indexJ;
                indexJ = indexJ + 1;
                break;
            }
            break;
    }
    // pokud jsem zpatky na startu, jsem na konci
    if (!firstRound)
    {
        if (GetTile(indexI, indexJ, map) == 'S') break;
    }
}
Console.WriteLine(nTiles);

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
