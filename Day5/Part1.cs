using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    internal static class Part1
    {
        public static decimal Run(string[] lines)
        {
            List<decimal> seeds = lines[0].Replace("seeds: ", "").Split(' ').Select(decimal.Parse).ToList();

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

        public static decimal GoOverAllMaps(decimal seed, List<string> names, List<Map> maps)
        {
            decimal seedValue = seed;

            foreach (string name in names)
            {
                IEnumerable<Map> suitableMaps = maps.Where(x => x.Name == name);
                Map? rightMap = suitableMaps.SingleOrDefault(x => x.IsRigthMap(seedValue));
                if (rightMap != null) seedValue = rightMap.GetRightValue(seedValue);
            }
            return seedValue;
        }
    }

    class Map
    {
        public decimal Dest { get; set; }
        public decimal Source { get; set; }
        public decimal Len { get; set; }
        public string Name { get; set; }

        public Map(string name, decimal dest, decimal source, decimal len)
        {
            Name = name;
            Dest = dest;
            Source = source;
            Len = len;
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
}
