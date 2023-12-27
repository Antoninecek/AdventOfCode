using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    internal static class Part1
    {
        public static int Run(List<TimeDistance> pairs)
        {
            int sum = 1;

            foreach (var pair in pairs)
            {
                int nWays = 0;
                for (decimal hold = 0; hold < pair.Time; hold++)
                {
                    decimal travelTime = pair.Time - hold;
                    decimal distance = travelTime * hold;
                    if (distance > pair.Distance) nWays++;
                }
                if (nWays > 0) sum *= nWays;
            }
            return sum;
        }
    }
}
