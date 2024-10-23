using ProjectEulerCSharp.EulerMath;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEulerCSharp.EulerProblems._0050s
{
    [Euler(
            title: "Problem 54: Poker Hands",
            description: @"In the card game poker, a hand consists of five cards and are ranked, from lowest to highest, in the following way:

    * High Card: Highest value card.
    * One Pair: Two cards of the same value.
    * Two Pairs: Two different pairs.
    * Three of a Kind: Three cards of the same value.
    * Straight: All cards are consecutive values.
    * Flush: All cards of the same suit.
    * Full House: Three of a kind and a pair.
    * Four of a Kind: Four cards of the same value.
    * Straight Flush: All cards are consecutive values of same suit.
    * Royal Flush: Ten, Jack, Queen, King, Ace, in same suit.
The cards are valued in the order:
2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King, Ace.

If two players have the same ranked hands then the rank made up of the highest value wins; for example, a pair of eights beats a pair of fives (see example 1 below). But if two ranks tie, for example, both players have a pair of queens, then highest cards in each hand are compared (see example 4 below) if the highest cards tie then the next highest cards are compared, and so on.

Consider the following five hands dealt to two players:

Hand    Player 1            Player 2            Winner
1       5H 5C 6S 7S KD      2C 3S 8S 8D TD      Player 2
        Pair of Fives       Pair of Eights

2       5D 8C 9S JS AC      2C 5C 7D 8S QH
        Highest card Ace    Highest card Queen  Player 1

3       2D 9C AS AH AC      3D 6D 7D TD QD
        Three Aces      Flush with Diamonds     Player 2
    
4       4D 6S 9H QH QC      3D 6D 7H QD QS      Player 1
        Pair of Queens      Pair of Queens
        Highest card Nine   Highest card Seven

5       2H 2D 4C 4D 4S      3C 3D 3S 9S 9D      Player 1
        Full House          Full House
        With Three Fours    with Three Threes

The file, poker.txt, contains one-thousand random hands dealt to two players. Each line of the file contains ten cards (separated by a single space): the first five are Player 1s cards and the last five are Player 2s cards. You can assume that all hands are valid (no invalid characters or repeated cards), each players hand is in no specific order, and in each hand there is a clear winner.

How many hands does Player 1 win?"
            )
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0054 : ISolution<int>
    {
        private readonly string[] _hands = EulerData.Get.Resource(fileName: "p054_poker.txt", fileStr => fileStr.SplitOnNewLines());

        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            // This solution is super brute force. This problem is very odd for Euler, it seems like it's purely a programming
            // problem with a bunch of rules and cases but no obvious clever math to make it more terse or performant.
            var handOnes = new List<PokerScore>();
            var handOTwos = new List<PokerScore>();
            foreach (var hs in _hands)
            {
                var hand1 = Card.PokerHandFromString(hs.Substring(0, 14));
                var hand2 = Card.PokerHandFromString(hs.Substring(15, 14));
                handOnes.Add(PokerScore.BuildPokerScore(hand1));
                handOTwos.Add(PokerScore.BuildPokerScore(hand2));
            }

            var cnt = 0;
            var comparer = new PokerScore.PokerScoreComparer();
            for (var i = 0; i < handOnes.Count; i++)
            {
                if (comparer.Compare(handOnes[i], handOTwos[i]) > 0)
                {
                    // this should be the count of all handOne wins
                    cnt++;
                }
            }

            return cnt;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 376;
        }

        private record Card(string Suit, int Value)
        {
            private static readonly Dictionary<char, int> ValueLookup =
            new()
            {
                {'2', 2},
                {'3', 3},
                {'4', 4},
                {'5', 5},
                {'6', 6},
                {'7', 7},
                {'8', 8},
                {'9', 9},
                {'T', 10},
                {'J', 11},
                {'Q', 12},
                {'K', 13},
                {'A', 14}
            };

            private static Card FromString(string str)
            {
                var val = ValueLookup[str[0]];
                var suit = str[1].ToString();
                return new Card(suit, val);
            }

            public static Card[] PokerHandFromString(string encodedCards)
            {
                var cardStrings = encodedCards.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var hand = cardStrings.Select(Card.FromString).OrderBy(c => c.Value).ThenBy(c => c.Suit).ToArray();
                return hand;
            }
        }

        private record PokerScore(int ScoreImportance)
        {
            private record GotNothing(int HighValue) : PokerScore(1);
            private record Pair(int PairValue, int HighValue) : PokerScore(2);
            private record TwoPairs(int LowPairValue, int HighPairValue, int HighValue) : PokerScore(3);
            private record ThreeOfKind(int ThreeKindValue, int HighValue) : PokerScore(4);
            private record Straight(int HighValue) : PokerScore(5);
            private record Flush(int HighValue) : PokerScore(6);
            private record FullHouse(int ThreeKindValue, int TwoKindValue, int HighValue) : PokerScore(7);
            private record FourOfKind(int KindValue, int HighValue) : PokerScore(8);
            private record StraightFlush(int HighValue) : PokerScore(9);
            private record RoyalFlush() : PokerScore(10);

            internal static PokerScore BuildPokerScore(Card[] cards)
            {
                var highValue = cards[4].Value;
                var isFlush = cards.Select(c => c.Suit).Distinct().Count() == 1;
                var isStraight = cards[0].Value + 1 == cards[1].Value
                              && cards[1].Value + 1 == cards[2].Value
                              && cards[2].Value + 1 == cards[3].Value
                              && cards[3].Value + 1 == cards[4].Value;
                if (isFlush && isStraight)
                {
                    if (highValue == 14)
                    {
                        return new RoyalFlush();
                    }
                    return new StraightFlush(highValue);
                }
                if (cards[0].Value == cards[1].Value && cards[1].Value == cards[2].Value && cards[2].Value == cards[3].Value)
                {
                    return new FourOfKind(cards[0].Value, highValue);
                }
                if (cards[1].Value == cards[2].Value && cards[2].Value == cards[3].Value && cards[3].Value == cards[4].Value)
                {
                    return new FourOfKind(cards[1].Value, highValue);
                }
                if (cards[0].Value == cards[1].Value && cards[1].Value == cards[2].Value && cards[3].Value == cards[4].Value)
                {
                    return new FullHouse(ThreeKindValue: cards[0].Value, TwoKindValue: cards[3].Value, highValue);
                }
                if (cards[0].Value == cards[1].Value && cards[2].Value == cards[3].Value && cards[3].Value == cards[4].Value)
                {
                    return new FullHouse(ThreeKindValue: cards[3].Value, TwoKindValue: cards[0].Value, highValue);
                }
                if (isFlush)
                {
                    return new Flush(highValue);
                }
                if (isStraight)
                {
                    return new Straight(highValue);
                }
                for (var tki = 0; tki < 3; tki++)
                {
	                if (cards[tki].Value == cards[tki + 1].Value && cards[tki + 1].Value == cards[tki + 2].Value)
	                {
		                return new ThreeOfKind(ThreeKindValue: cards[tki].Value, highValue);
					}
                }
                if (cards[0].Value == cards[1].Value && cards[2].Value == cards[3].Value)
                {
                    var low = Math.Min(cards[0].Value, cards[2].Value);
                    var max = Math.Max(cards[0].Value, cards[2].Value);
                    return new TwoPairs(low, max, highValue);
                }
                if (cards[0].Value == cards[1].Value && cards[3].Value == cards[4].Value)
                {
	                var low = Math.Min(cards[0].Value, cards[3].Value);
	                var max = Math.Max(cards[0].Value, cards[3].Value);
	                return new TwoPairs(low, max, highValue);
                }
				if (cards[1].Value == cards[2].Value && cards[3].Value == cards[4].Value)
                {
                    var low = Math.Min(cards[1].Value, cards[3].Value);
                    var max = Math.Max(cards[1].Value, cards[3].Value);
                    return new TwoPairs(low, max, highValue);
                }
                for (var i1 = 0; i1<5; i1++)
                {
	                for (var i2 = i1+1; i2<5; i2++)
	                {
		                if (cards[i1].Value == cards[i2].Value)
		                {
			                return new Pair(cards[i1].Value, highValue);
		                }
	                }
                }
                return new GotNothing(highValue);
            }

            internal class PokerScoreComparer : IComparer<PokerScore>
            {
                public int Compare(PokerScore hand1, PokerScore hand2)
                {
                    if (hand1.ScoreImportance == hand2.ScoreImportance)
                    {
						// if the ScoreImportance is the same then both hands have the same type of PokerScore subclass
						switch (hand1)
                        {
                            case GotNothing gn1 when hand2 is GotNothing gn2:
                                return gn1.HighValue.CompareTo(gn2.HighValue);
                            case Pair p1 when hand2 is Pair p2:
                                {
                                    if (p1.PairValue == p2.PairValue)
                                    {
                                        return p1.HighValue.CompareTo(p2.HighValue);
                                    }
                                    return p1.PairValue.CompareTo(p2.PairValue);
                                }
                            case TwoPairs tp1 when hand2 is TwoPairs tp2:
                                {
                                    if (tp1.HighPairValue == tp2.HighPairValue)
                                    {
                                        if (tp1.LowPairValue == tp2.LowPairValue)
                                        {
                                            return tp1.HighPairValue.CompareTo(tp2.HighValue);
                                        }
                                        return tp1.LowPairValue.CompareTo(tp2.LowPairValue);
                                    }
                                    return tp1.HighPairValue.CompareTo(tp2.HighPairValue);
                                }
                            case ThreeOfKind tk1 when hand2 is ThreeOfKind tk2:
                                {
                                    if (tk1.ThreeKindValue == tk2.ThreeKindValue)
                                    {
                                        return tk1.HighValue.CompareTo(tk2.HighValue);
                                    }
                                    return tk1.ThreeKindValue.CompareTo(tk2.ThreeKindValue);
                                }
                            case Straight s1 when hand2 is Straight s2:
                                {
                                    return s1.HighValue.CompareTo(s2.HighValue);
                                }
                            case Flush f1 when hand2 is Flush f2:
                                {
                                    return f1.HighValue.CompareTo(f2.HighValue);
                                }
                            case FullHouse fh1 when hand2 is FullHouse fh2:
                                {
                                    if (fh1.ThreeKindValue == fh2.ThreeKindValue)
                                    {
                                        if (fh1.TwoKindValue == fh2.TwoKindValue)
                                        {
                                            return fh1.HighValue.CompareTo(fh2.HighValue);
                                        }
                                        return fh1.TwoKindValue.CompareTo(fh2.TwoKindValue);
                                    }
                                    return fh1.ThreeKindValue.CompareTo(fh2.ThreeKindValue);
                                }
                            case FourOfKind fk1 when hand2 is FourOfKind fk2:
                                {
                                    if (fk1.KindValue == fk2.KindValue)
                                    {
                                        return fk1.HighValue.CompareTo(fk2.HighValue);
                                    }
                                    return fk1.KindValue.CompareTo(fk2.KindValue);
                                }
                            case StraightFlush sf1 when hand2 is StraightFlush sf2:
                                {
                                    return sf1.HighValue.CompareTo(sf2.HighValue);
                                }
                            case RoyalFlush: return 0;
                        }
                    }

					return hand1.ScoreImportance.CompareTo(hand2.ScoreImportance);
                }
            }
        }
    }
}
