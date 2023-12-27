using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    internal class Part1_5
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

            bool range = false;
            decimal seedStart = decimal.MinValue;
            foreach (var seed in seeds)
            {
                Console.WriteLine(seed.ToString());
                if (!range)
                {
                    seedStart = seed;
                    range = true;
                    continue;
                }
                for (decimal i = seedStart; i <= seedStart + seed; i++)
                {
                    decimal val = Part1.GoOverAllMaps(i, names, maps);
                    if (val < min)
                    {
                        min = val;
                        Console.WriteLine($"{i} {min}");
                    }
                }
                range = false;
            }

            return min;
        }
    }
}
