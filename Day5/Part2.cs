namespace Day5.Part2
{
    internal static class Part2
    {
        public static decimal Run(string[] lines)
        {
            List<Range> seeds = new();
            Range? tmpRange = null;
            foreach (decimal splitted in lines[0].Replace("seeds: ", "").Split(' ').Select(decimal.Parse).ToList())
            {
                if (tmpRange == null)
                {
                    tmpRange = new(splitted, -1);
                    continue;
                }
                tmpRange.End = tmpRange.Start + splitted;
                seeds.Add(tmpRange);
                tmpRange = null;
            }

            List<Map> maps = new();

            List<string> names = new();

            string name = "";
            foreach (var line in lines)
            {
                if (line.StartsWith("seeds") || string.IsNullOrWhiteSpace(line)) continue;

                if (line.EndsWith("map:"))
                {
                    name = line.Replace(" map:", "");
                    names.Add(name);
                    continue;
                }
                var pars = line.Split().Select(decimal.Parse).ToList();
                maps.Add(new Map(name, pars[0], pars[1], pars[2]));
            }

            decimal min = decimal.MaxValue;
            foreach (var seed in seeds)
            {
                decimal val = GoOverAllMaps(seed, names, maps);
                if (val < min) min = val;
            }

            return min;
        }

        public static decimal GoOverAllMaps(Range seed, List<string> names, List<Map> maps)
        {
            List<Range> seedRanges = new() { seed };
            List<Range> valuedRanges = new();

            foreach (string name in names)
            {
                IEnumerable<Map> suitableMaps = maps.Where(x => x.Name == name);
                seedRanges.AddRange(valuedRanges);
                valuedRanges.Clear();
                // projdi mapy v setu map
                foreach (var map in suitableMaps)
                {
                    List<Range> toRemove = new();
                    List<Range> toAdd = new();

                    // pro vsechny range ze seedu, ktery nikam nepatri
                    foreach (var seedRange in seedRanges)
                    {
                        Range? inRange = GetSeedRangeInMap(seedRange, map.Range);
                        if (inRange == null) continue;

                        // splitnout na ranges
                        toAdd.AddRange(SplitRanges(seedRange, map.Range));
                        // k odebrani range v mape
                        toRemove.Add(inRange);
                        // valuovat
                        valuedRanges.Add(ValueRange(inRange, map));
                    }
                    // odstranit range, ktery prosly mapou
                    toRemove.ForEach(x => seedRanges.Remove(x));
                    toAdd.ForEach(x => seedRanges.Add(x));
                }
                // range, ktery nejsou v mape
                valuedRanges.AddRange(seedRanges);
            }
            return valuedRanges.Min(x => x.Start);
        }

        // ziskat jen range, ktery jsou v mape, ty pak valuovat a vratit jen ty, co nejsou uvnitr, ty pak poslat do dalsi mapy
        public static List<Range> SplitRanges(Range seed, Range map)
        {
            List<Range> ranges = new();
            if (seed.Start < map.Start) ranges.Add(new Range(seed.Start, map.Start - 1));
            if (seed.End > map.End) ranges.Add(new Range(map.End + 1, seed.End));
            return ranges;
        }

        public static Range ValueRange(Range seed, Map map)
        {
            return new Range(map.GetRightValue(seed.Start), map.GetRightValue(seed.End));
        }

        public static Range? GetSeedRangeInMap(Range seed, Range map)
        {
            // not in
            if (seed.End < map.Start) return null;
            if (seed.Start > map.End) return null;

            // full in
            if (seed.Start >= map.Start && seed.End <= map.End)
            {
                return seed;
            }

            // zleva do konce
            if (seed.End <= map.End) return new Range(map.Start, seed.End);

            // zevnitr doprava
            if (seed.Start <= map.End) return new Range(seed.Start, map.End);

            throw new NotImplementedException();
        }



        public static List<Range>? GetSeedToMapRanges(Range seed, Range map)
        {
            // full in
            if (seed.Start >= map.Start && seed.End <= map.End)
            {
                return new List<Range>() { seed };
            }
            // zleva
            if (seed.Start < map.Start && seed.End >= map.Start)
            {
                // az za konec
                if (seed.End > map.End)
                {
                    return new List<Range> { new Range(seed.Start, map.Start - 1), new Range(map.Start, map.End), new Range(map.End + 1, seed.End) };
                }
                else
                {
                    return new List<Range> { new Range(seed.Start, map.Start - 1), new Range(map.Start, seed.End) };
                }
            }
            // zevnitr doprava
            if (seed.Start >= map.Start && seed.Start <= map.End)
            {
                return new List<Range> { new Range(seed.Start, map.End), new Range(map.End + 1, seed.End) };
            }
            // not inside
            return null;
        }
    }

    public class Map
    {
        public decimal Dest { get; set; }
        public decimal Source { get; set; }
        public decimal Len { get; set; }
        public string Name { get; set; }
        public Range Range { get; set; }

        public Map(string name, decimal dest, decimal source, decimal len)
        {
            Name = name;
            Dest = dest;
            Source = source;
            Len = len;
            Range = new Range(dest, dest + len);
        }

        public bool IsRigthMap(decimal value)
        {
            return value > Source && value < Source + Len;
        }

        public decimal GetRightValue(decimal value)
        {
            decimal diff = value - Source;
            return Dest + diff;
        }
    }

    public class Range
    {
        public decimal Start;
        public decimal End;

        public Range(decimal start, decimal end)
        {
            this.Start = start;
            this.End = end;
        }
    }
}
