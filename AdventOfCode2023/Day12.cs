
using System.Text.Json;

internal class Day12 : AdventDay<long>
{
    public long RunP1(StreamReader reader)
    {
        var sum = 0L;
        Dictionary<string, long> mem = new();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split(' ');
            var nums = parts[1].Split(',').Select(int.Parse).ToArray();
            sum += CountWays(parts[0], nums, mem);
        }
        return sum;
    }

    public long RunP2(StreamReader reader)
    {
        var sum = 0L;
        Dictionary<string, long> mem = new();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split(' ');
            var str = string.Join('?',Enumerable.Repeat(parts[0], 5));
            var nums = Enumerable.Repeat(parts[1].Split(',').Select(int.Parse), 5).SelectMany(x => x).ToArray();
            sum += CountWays(str, nums, mem);
        }
        return sum;
    }

    private long CountWays(in string line, in int[] runs, in Dictionary<string, long> mem)
    {
        var key = line + JsonSerializer.Serialize(runs);
        if (mem.ContainsKey(key)) return mem[key];

        if (line.Length == 0)
        {
            if (runs.Length == 0)
            {
                mem[key] = 1;
                return 1;
            }
            mem[key] = 0;
            return 0;
        }

        if (runs.Length == 0)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '#')
                {
                    mem[key] = 0;
                    return 0;
                }
            }
            mem[key] = 1;
            return 1;
        }

        if (line.Length < runs.Sum() + runs.Length - 1)
        {
            mem[key] = 0;
            return 0;
        }

        if (line[0] == '.')
        {
            var res = CountWays(line[1..], runs, mem);
            mem[key] = res;
            return res;
        }

        if (line[0] == '#')
        {
            for (int i = 0; i < runs[0]; i++)
            {
                if (line[i] == '.')
                {
                    mem[key] = 0;
                    return 0;
                }
            }
            if (line.Length > runs[0] && line[runs[0]] == '#')
            {
                mem[key] = 0;
                return 0;
            }
            long res;
            if (line.Length <= runs[0])
            {
                res = CountWays("", runs[1..], mem);
            }
            else res = CountWays(line[(runs[0] + 1)..], runs[1..], mem);
            mem[key] = res;
            return res;
        }

        var res1 = CountWays('#' + line[1..], runs, mem);
        var res2 = CountWays('.' + line[1..], runs, mem);
        mem[key] = res1 + res2;
        return res1 + res2;
    }
}