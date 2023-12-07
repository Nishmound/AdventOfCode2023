
internal class Day07 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        var hands = Parse1(reader).OrderBy(x => x.Item1).ToArray();
        int sum = 0;
        for (int i = 0; i < hands.Length; i++)
        {
            sum += hands[i].Item2 * (i + 1);
        }
        return sum;
    }

    public int RunP2(StreamReader reader)
    {
        var hands = Parse2(reader).OrderBy(x => x.Item1).ToArray();
        int sum = 0;
        for (int i = 0; i < hands.Length; i++)
        {
            sum += hands[i].Item2 * (i + 1);
        }
        return sum;
    }

    private readonly record struct Hand1(string Cards) : IComparable<Hand1>
    {
        public int CompareTo(Hand1 other)
        {
            var kinds1 = Cards.Distinct().Count();
            var kinds2 = other.Cards.Distinct().Count();

            if (kinds1 < kinds2) return 1;
            if (kinds1 > kinds2) return -1;

            if (kinds1 == 1 || kinds1 == 4 || kinds1 == 5) return CompareCards(other);
            if (kinds1 == 2)
            {
                var card1 = Cards[0];
                var card2 = other.Cards[0];
                var occ1 = Cards.Count(x => x == card1);
                var occ2 = other.Cards.Count(x => x == card2);
                if (occ1 == 1 || occ1 == 4)
                {
                    if (occ2 == 1 || occ2 == 4) return CompareCards(other);
                    else return 1;
                }
                else
                {
                    if (occ2 == 1 || occ2 == 4) return -1;
                    else return CompareCards(other);
                }
            }
            var cards1 = Cards.Distinct();
            var cards2 = other.Cards.Distinct();
            bool isTK1 = true;
            bool isTK2 = true;
            foreach (var card in cards1)
            {
                if (Cards.Count(x => x == card) == 2)
                {
                    isTK1 = false;
                    break;
                }
            }
            foreach (var card in cards2)
            {
                if (other.Cards.Count(x => x == card) == 2)
                {
                    isTK2 = false;
                    break;
                }
            }

            if (isTK1)
            {
                if (isTK2) return CompareCards(other);
                else return 1;
            }
            if (isTK2) return -1;
            return CompareCards(other);
        }

        private int CompareCards(Hand1 other)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Cards[i] == other.Cards[i]) continue;
                if (Cards[i] == 'A') return 1;
                if (Cards[i] == 'K')
                {
                    if (other.Cards[i] == 'A') return -1;
                    else return 1;
                }
                if (Cards[i] == 'Q')
                {
                    if (other.Cards[i] == 'A' || other.Cards[i] == 'K') return -1;
                    else return 1;
                }
                if (Cards[i] == 'J')
                {
                    if (other.Cards[i] == 'A' || other.Cards[i] == 'K' || other.Cards[i] == 'Q') return -1;
                    else return 1;
                }
                if (Cards[i] == 'T')
                {
                    if (other.Cards[i] == 'A' || other.Cards[i] == 'K' || other.Cards[i] == 'Q' || other.Cards[i] == 'J') return -1;
                    else return 1;
                }
                if (!char.IsDigit(other.Cards[i])) return -1;
                var card1 = Cards[i] - '0';
                var card2 = other.Cards[i] - '0';
                if (card1 > card2) return 1;
                if (card1 < card2) return -1;
            }
            return 0;
        }
    }

    private readonly record struct Hand2(string Cards) : IComparable<Hand2>
    {
        public int CompareTo(Hand2 other)
        {
            var hand1 = ReplaceJokers(Cards);
            var hand2 = ReplaceJokers(other.Cards);
            
            var kinds1 = hand1.Distinct().Count();
            var kinds2 = hand2.Distinct().Count();

            if (kinds1 < kinds2) return 1;
            if (kinds1 > kinds2) return -1;

            if (kinds1 == 1 || kinds1 == 4 || kinds1 == 5) return CompareCards(other);
            if (kinds1 == 2)
            {
                var card1 = hand1[0];
                var card2 = hand2[0];
                var occ1 = hand1.Count(x => x == card1);
                var occ2 = hand2.Count(x => x == card2);
                if (occ1 == 1 || occ1 == 4)
                {
                    if (occ2 == 1 || occ2 == 4) return CompareCards(other);
                    else return 1;
                }
                else
                {
                    if (occ2 == 1 || occ2 == 4) return -1;
                    else return CompareCards(other);
                }
            }
            var cards1 = hand1.Distinct();
            var cards2 = hand2.Distinct();
            bool isTK1 = true;
            bool isTK2 = true;
            foreach (var card in cards1)
            {
                if (hand1.Count(x => x == card) == 2)
                {
                    isTK1 = false;
                    break;
                }
            }
            foreach (var card in cards2)
            {
                if (hand2.Count(x => x == card) == 2)
                {
                    isTK2 = false;
                    break;
                }
            }

            if (isTK1)
            {
                if (isTK2) return CompareCards(other);
                else return 1;
            }
            if (isTK2) return -1;
            return CompareCards(other);
        }

        private int CompareCards(Hand2 other)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Cards[i] == other.Cards[i]) continue;
                if (Cards[i] == 'J') return -1;
                if (other.Cards[i] == 'J') return 1;
                if (Cards[i] == 'A') return 1;
                if (Cards[i] == 'K')
                {
                    if (other.Cards[i] == 'A') return -1;
                    else return 1;
                }
                if (Cards[i] == 'Q')
                {
                    if (other.Cards[i] == 'A' || other.Cards[i] == 'K') return -1;
                    else return 1;
                }
                if (Cards[i] == 'T')
                {
                    if (other.Cards[i] == 'A' || other.Cards[i] == 'K' || other.Cards[i] == 'Q') return -1;
                    else return 1;
                }
                if (!char.IsDigit(other.Cards[i])) return -1;
                var card1 = Cards[i] - '0';
                var card2 = other.Cards[i] - '0';
                if (card1 > card2) return 1;
                if (card1 < card2) return -1;
            }
            return 0;
        }

        private string ReplaceJokers(string hand)
        {
            var cards = hand.Distinct();
            if (cards.Count() == 1) return hand;
            var maxChar = cards.First() == 'J' ? cards.Skip(1).First() : cards.First();
            var maxCount = hand.Count(x => x == maxChar);
            foreach (var card in cards.Skip(1))
            {
                if (card == 'J') continue;
                var count = hand.Count(x => x == card);
                if (count > maxCount)
                {
                    maxChar = card;
                    maxCount = count;
                }
            }
            return hand.Replace('J', maxChar);
        }
    }

    private List<(Hand1, int)> Parse1(StreamReader reader)
    {
        List<(Hand1, int)> hands = new();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split(' ');
            hands.Add((new(parts[0]), int.Parse(parts[1])));
        }
        return hands;
    }

    private List<(Hand2, int)> Parse2(StreamReader reader)
    {
        List<(Hand2, int)> hands = new();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split(' ');
            hands.Add((new(parts[0]), int.Parse(parts[1])));
        }
        return hands;
    }
}