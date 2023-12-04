namespace AdventOfCode2023;
internal class Day04 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        var sum = 0;
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var (winNums, playNums) = Parse(line);
            var wins = winNums.Intersect(playNums).Count();
            if (wins > 0) sum += 2.Pow(wins - 1);
        }

        return sum;
    }

    public int RunP2(StreamReader reader)
    {
        List<(int wins, int cards)> cards = new();
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var (winNums, playNums) = Parse(line);
            var wins = winNums.Intersect(playNums).Count();
            cards.Add((wins, 1));
        }

        var c = cards.Count();
        for (int i = 0; i < c; i++)
        {
            for (int j = 1; j <= cards[i].wins && j < c; j++)
            {
                cards[i + j] = (cards[i + j].wins, cards[i + j].cards + cards[i].cards);
            }
        }

        return cards.Select(x => x.cards).Sum();
    }

    private (IEnumerable<int>, IEnumerable<int>) Parse(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var cut = Array.IndexOf(parts, "|");
        var winNums = parts[2..cut].Select(x => int.Parse(x));
        var playNums = parts[(cut + 1)..].Select(x => int.Parse(x));
        return (winNums, playNums);
    }
}