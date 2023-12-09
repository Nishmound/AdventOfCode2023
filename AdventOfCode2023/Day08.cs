namespace AdventOfCode2023;

internal class Day08 : AdventDay<long>
{
    public long RunP1(StreamReader reader)
    {
        var (cycle, map) = Parse(reader);

        var curr = "AAA";
        var count = 0;
        while (curr != "ZZZ")
        {
            if (cycle[count % cycle.Length] == 'L') curr = map[curr].Item1;
            else curr = map[curr].Item2;
            count++;
        }
        return count;
    }

    public long RunP2(StreamReader reader)
    {
        var (cycle, map) = Parse(reader);

        var starts = map.Select(x => x.Key).Where(x => x[^1] == 'A').ToArray();
        var loops = new long[starts.Count()];

        for (int i = 0; i < loops.Length; i++)
        {
            var curr = starts[i];
            var count = 0;
            while (curr[^1] != 'Z')
            {
                if (cycle[count % cycle.Length] == 'L') curr = map[curr].Item1;
                else curr = map[curr].Item2;
                count++;
            }
            loops[i] = count;
        }
        return loops.LeastCommonMultiple();
    }

    private (string, Dictionary<string, (string, string)>) Parse(StreamReader reader)
    {
        string cycle = reader.ReadLine() ?? "";
        reader.ReadLine();

        Dictionary<string, (string, string)> map = new();
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split(" ,()=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            map[parts[0]] = (parts[1], parts[2]);
        }

        return (cycle, map);
    }
}