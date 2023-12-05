namespace AdventOfCode2023;

internal class Day05 : AdventDay<long>
{
    public long RunP1(StreamReader reader)
    {
        var seeds = reader.ReadLine()[7..].Split(' ').Select(long.Parse).ToArray();
        reader.ReadLine();
        var SoilMap = ParseMap(reader);
        var FertMap = ParseMap(reader);
        var WaterMap = ParseMap(reader);
        var LightMap = ParseMap(reader);
        var TempMap = ParseMap(reader);
        var HumidMap = ParseMap(reader);
        var LocMap = ParseMap(reader);

        var min = long.MaxValue;

        for (int i = 0; i < seeds.Length; i++)
        {
            long loc = Map(Map(Map(Map(Map(Map(Map(seeds[i], SoilMap), FertMap), WaterMap), LightMap), TempMap), HumidMap), LocMap);
            min = loc < min ? loc : min;
        }

        return min;
    }

    public long RunP2(StreamReader reader)
    {
        var seeds = reader.ReadLine()[7..].Split(' ').Select(long.Parse).ToArray();
        reader.ReadLine();
        var SoilMap = ParseMap(reader);
        var FertMap = ParseMap(reader);
        var WaterMap = ParseMap(reader);
        var LightMap = ParseMap(reader);
        var TempMap = ParseMap(reader);
        var HumidMap = ParseMap(reader);
        var LocMap = ParseMap(reader);

        var min = long.MaxValue;

        for (int i = 0; i < seeds.Length; i+=2)
        {
            Console.WriteLine($"Range {(i+1)/2}/{seeds.Length/2}");
            for (long j = seeds[i]; j < seeds[i] + seeds[i+1]; j++)
            {
                long loc = Map(Map(Map(Map(Map(Map(Map(j, SoilMap), FertMap), WaterMap), LightMap), TempMap), HumidMap), LocMap);
                min = loc < min ? loc : min;
            }
        }
        return min;
    }

    private (long, long, long)[] ParseMap (StreamReader reader)
    {
        reader.ReadLine();
        List<(long, long, long)> map = new();
        string? line;
        while (!string.IsNullOrEmpty(line = reader.ReadLine()))
        {
            var parts = line.Split(' ').Select(long.Parse).ToArray();
            map.Add((parts[0], parts[1], parts[2]));
        }
        return map.ToArray();
    }

    private long Map(in long source, in (long dest, long src, long len)[] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (source < map[i].src || source >= map[i].src + map[i].len) continue;
            return map[i].dest + (source - map[i].src);
        }
        return source;
    }
}