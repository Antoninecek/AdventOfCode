using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7.Part1
{
    internal static class Part1
    {
        public static int Run(List<HandBidPair> pairs)
        {
            int result = 0;
            pairs.Sort(new HandBidPairComparer());
            for (int i = 0; i < pairs.Count; i++)
            {
                result += pairs[i].Bid * (i + 1);
            }
            return result;
        }
    }
}
