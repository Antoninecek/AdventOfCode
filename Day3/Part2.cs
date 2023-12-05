namespace Day3
{
    internal static class Part2
    {
        public static int Run(string[] lines)
        {
//            lines = @"467..114..
//...*......
//..35..633.
//......#...
//617*......
//.....+.58.
//..592.....
//......755.
//...$.*....
//.664.598..".Split("\r\n");
            int sum = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    sum += IsAsterisk(lines, i, j);
                }
            }
            return sum;
        }

        public static int GetPowerAdjacentNumbers(string[] lines, int i, int j)
        {
            List<string> result = new();

            bool isNahore = false;
            // nahore
            if (i != 0)
            {
                string a = GetWholeNumber(lines[i - 1], j);
                if (!string.IsNullOrWhiteSpace(a))
                {
                    isNahore = true;
                }
                result.Add(a);
            }
            // nahore vlevo
            if (i != 0 && j != 0 && !isNahore)
            {
                result.Add(GetWholeNumber(lines[i - 1], j - 1));
            }
            // nahore vpravo
            if (i != 0 && j != lines.Length - 1 && !isNahore)
            {
                result.Add(GetWholeNumber(lines[i - 1], j + 1));
            }

            // vlevo
            if (j != 0)
            {
                result.Add(GetWholeNumber(lines[i], j - 1));
            }
            // vpravo
            if (j != lines.Length - 1)
            {
                result.Add(GetWholeNumber(lines[i], j + 1));
            }

            bool isDole = false;
            // dole
            if (i != lines.Length - 1)
            {
                string a = GetWholeNumber(lines[i + 1], j);
                if (!string.IsNullOrWhiteSpace(a))
                {
                    isDole = true;
                }
                result.Add(a);
            }
            // dole vlevo
            if (j != 0 && i != lines.Length - 1 && !isDole)
            {
                result.Add(GetWholeNumber(lines[i + 1], j - 1));
            }
            // dole vpravo
            if (i != lines.Length - 1 && j != lines.Length - 1 && !isDole)
            {
                result.Add(GetWholeNumber(lines[i + 1], j + 1));
            }

            var r = result.Where(x => x != string.Empty);
            if (r.Count() == 2)
            {
                return int.Parse(r.First()) * int.Parse(r.Last());
            }
            return 0;
        }

        public static string GetWholeNumber(string line, int j)
        {
            string number = string.Empty;
            if (int.TryParse(line[j].ToString(), out int n))
            {
                number = n.ToString();
            }
            else
            {
                return string.Empty;
            }

            for (int k = j + 1; k < line.Length; k++)
            {
                if (int.TryParse(line[k].ToString(), out int r))
                {
                    number += line[k];
                }
                else
                {
                    break;
                }
            }
            for (int k = j - 1; k >= 0; k--)
            {
                if (int.TryParse(line[k].ToString(), out int r))
                {
                    number = line[k] + number;
                }
                else
                {
                    break;
                }
            }
            return number;
        }

        public static int IsAsterisk(string[] lines, int i, int j)
        {
            if (lines[i][j] == '*')
                return GetPowerAdjacentNumbers(lines, i, j);
            return 0;
        }
    }
}
