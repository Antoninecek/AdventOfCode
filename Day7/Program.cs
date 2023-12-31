﻿// See https://aka.ms/new-console-template for more information
using Day7.Part1;

Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input7.txt");
List<HandBidPair> pairs = new();
foreach (var line in lines)
{
    var split = line.Split(' ');
    HandBidPair pair = new(int.Parse(split[1]));
    foreach (char c in split[0]) pair.AddCard(c);
    pair.EvaluateJokers();
    pairs.Add(pair);
}

Console.WriteLine(Part1.Run(pairs));

public class HandBidPair
{
    public List<Card> Cards { get; private set; } = new();
    public int Bid { get; set; }

    public HandValue HandValue { get; private set; } = HandValue.none;

    public HandBidPair(int bid)
    {
        Bid = bid;
    }

    public void EvaluateJokers()
    {
        int jokerCount = Cards.Count(x => x.Value == 1);
        if (jokerCount == 0) return;
        switch (HandValue)
        {
            case HandValue.none:
                HandValue = HandValue.five;
                break;
            case HandValue.highcard:
                HandValue = jokerCount switch
                {
                    1 => HandValue.onepair,
                    2 => HandValue.three,
                    3 => HandValue.four,
                    4 => HandValue.five
                };
                break;
            case HandValue.onepair:
                HandValue = jokerCount switch
                {
                    1 => HandValue.three,
                    2 => HandValue.four,
                    3 => HandValue.five,
                };
                break;
            case HandValue.twopair:
                HandValue = HandValue.fullhouse;
                break;
            case HandValue.three:
                HandValue = jokerCount switch
                {
                    1 => HandValue.four,
                    2 => HandValue.five
                };
                break;
            case HandValue.four:
                if (jokerCount == 1) HandValue = HandValue.five;
                break;

        }
    }

    public void AddCard(char strVal)
    {
        Card card = new(strVal);
        if (card.Value == 1)
        {
            Cards.Add(card);
            return;
        }
        switch (HandValue)
        {
            case HandValue.none:
                HandValue = HandValue.highcard;
                break;
            case HandValue.highcard:
                if (Cards.Count(x => x.Value.Equals(card.Value)) == 1)
                {
                    HandValue = HandValue.onepair;
                }
                break;
            case HandValue.onepair:
                if (Cards.Count(x => x.Value.Equals(card.Value)) == 1)
                {
                    HandValue = HandValue.twopair;
                }
                if (Cards.Count(x => x.Value.Equals(card.Value)) == 2)
                {
                    HandValue = HandValue.three;
                }
                break;
            case HandValue.twopair:
                if (Cards.Count(x => x.Value.Equals(card.Value)) == 2)
                {
                    HandValue = HandValue.fullhouse;
                }
                break;
            case HandValue.three:
                if (Cards.Count(x => x.Value.Equals(card.Value)) == 3)
                {
                    HandValue = HandValue.four;
                }
                if (Cards.Count(x => x.Value.Equals(card.Value)) == 1)
                {
                    HandValue = HandValue.fullhouse;
                }
                break;
            case HandValue.four:
                if (Cards.Count(x => x.Value.Equals(card.Value)) == 4)
                {
                    HandValue = HandValue.five;
                }
                break;
        }
        Cards.Add(card);
    }
}

public enum HandValue { five = 7, four = 6, fullhouse = 5, three = 4, twopair = 3, onepair = 2, highcard = 1, none = 0 }

public class HandBidPairComparer : Comparer<HandBidPair>
{
    public override int Compare(HandBidPair? x, HandBidPair? y)
    {
        if (x.HandValue > y.HandValue) return 1;
        if (x.HandValue < y.HandValue) return -1;

        for (int i = 0; i < x.Cards.Count; i++)
        {
            if (x.Cards[i].Value != y.Cards[i].Value) return x.Cards[i].Value > y.Cards[i].Value ? 1 : -1;
        }

        return 0;
    }
}

public class Card
{
    public int Value { get; private set; }

    public Card(char strVal)
    {
        Value = strVal switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 1,
            'T' => 10,
            _ => int.Parse(strVal.ToString()),
        };
    }
}