
using AdventOfCode2023;

internal class Day06 : AdventDay<long>
{
    public long RunP1(StreamReader reader)
    {
        (int time, int dist)[] races = ParseP1(reader);
        long prod = 1;
        for (int i = 0; i < races.Length; i++)
        {
            var (tb1, tb2) = Solve(races[i].time, races[i].dist);
            prod *= tb2 - tb1 - 1;
        }
        return prod;
    }

    public long RunP2(StreamReader reader)
    {
        var (time, dist) = ParseP2(reader);
        var (tb1, tb2) = Solve(time, dist);
        return tb2 - tb1 - 1;
    }

    private (long, long) Solve(in long time, in long dist) => 
        ((long)Math.Floor((-time + Math.Sqrt(time.Pow(2) - 4 * dist)) / -2),
            (long)Math.Ceiling((-time - Math.Sqrt(time.Pow(2) - 4 * dist)) / -2));

    private (int, int)[] ParseP1(StreamReader reader)
    {
        var time = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse);
        var dist = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse);
        return time.Zip(dist).ToArray();
    }

    private (long, long) ParseP2(StreamReader reader)
    {
        var time = long.Parse(string.Concat(reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1)));
        var dist = long.Parse(string.Concat(reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1)));
        return (time, dist);
    }
}