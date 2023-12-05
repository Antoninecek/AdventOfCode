using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day4
{
    internal static class Part2
    {

        public static int Run(string[] lines)
        {


            Dictionary<int, List<int>> winningCombos = new();
            Dictionary<int, int> scretched = new();

            foreach (string line in lines)
            {
                var cardNumber = int.Parse(Regex.Match(line, @"Card[ ]*([0-9]*):").Groups[1].Value);
                string justNumbers = line.Substring(line.IndexOf(":") + 1);
                var winning = justNumbers.Split('|').First().Split(" ").Where(x => x != string.Empty);
                var my = justNumbers.Split('|').Last().Split(" ").Where(x => x != string.Empty);

                var nWinning = my.Intersect(winning).Count();
                winningCombos.Add(cardNumber, Enumerable.Range(cardNumber + 1, nWinning).ToList());
            }

            foreach (var cardNumber in winningCombos.Keys.Order())
            {
                var cardCombo = winningCombos[cardNumber];

                foreach (var card in cardCombo)
                {
                    int n = 1;
                    if (scretched.ContainsKey(cardNumber))
                    {
                        n += scretched[cardNumber];
                    }

                    if (!scretched.TryAdd(card, n))
                    {
                        scretched[card] += n;
                    }
                }
            }
            return scretched.Values.Sum() + lines.Length;
        }
    }
}
