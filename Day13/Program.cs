// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input13.txt");

List<string> currLines = new();
int sum = 0;
foreach (var line in lines)
{
    if (!string.IsNullOrWhiteSpace(line))
    {
        currLines.Add(line);
        continue;
    }
    // dojdu k mezere, jdu na to
    sum += Run(currLines);
    currLines.Clear();
}
Console.WriteLine(sum);

static int Run(List<string> lines)
{
    bool[][] mirrorIndex = new bool[lines.Count][];
    int j = 0;
    foreach (var line in lines)
    {
        mirrorIndex[j] = new bool[lines[j].Length];
        for (int i = 0; i < line.Length - 1; i++)
        {
            // kazdej string projedu a ziskam substring, abych nepresahl
            int len = Math.Min(line.Length - (i + 1), i + 1);
            string first = line.Substring(Math.Max(0, (i + 1) - len), len);
            char[] s = line.Substring(i + 1, len).ToCharArray();
            Array.Reverse(s);
            string second = new string(s);
            mirrorIndex[j][i] = first.Equals(second);
        }
        j++;
    }

    // najdu na jakym indexu je ve vsech stringach shoda
    Dictionary<int, int> indexCountPair = new();
    for (int i = 0; i < mirrorIndex.Length; i++)
    {
        for (int k = 0; k < mirrorIndex[i].Length; k++)
        {
            if (mirrorIndex[i][k])
            {
                if (!indexCountPair.TryAdd(k, 1))
                {
                    indexCountPair[k]++;
                }
            }
        }
    }
    foreach (var key in indexCountPair.Keys)
    {
        if (indexCountPair[key] == mirrorIndex.Length - 1)
        {
            return key + 1;
        }
    }

    // pokud neni v horizontu, jdu na vertikal
    return 100 * Run(Transpose(lines));
}

static List<string> Transpose(List<string> lines)
{
    // init
    char[][] ch = new char[lines[0].Length][];
    for (int i = 0; i < lines[0].Length; i++)
    {
        ch[i] = new char[lines.Count];
    }

    // transpose
    for (int i = 0; i < lines.Count; i++)
    {
        for (int j = 0; j < lines[i].Length; j++)
        {
            ch[j][i] = lines[i][j];
        }
    }
    // vratit stringy protoze jsem retard
    List<string> r = new();
    for (int i = 0; i < ch.Length; i++)
    {
        r.Add(new string(ch[i]));
    }
    return r;
}