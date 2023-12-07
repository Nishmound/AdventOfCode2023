namespace AdventOfCode2023;

internal class Day05 : AdventDay<long>
{
    public long RunP1(StreamReader reader)
    {
#pragma warning disable CS8602 // Dereferenzierung eines möglichen Nullverweises.
        var seeds = reader.ReadLine()[7..].Split(' ').Select(long.Parse).ToArray();
#pragma warning restore CS8602 // Dereferenzierung eines möglichen Nullverweises.
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

        List<(long, long)> seedRanges = new();
        for (int i = 0; i < seeds.Length; i += 2)
        {
            seedRanges.Add((seeds[i], seeds[i + 1]));
        }

        return seedRanges
            .Select(x => MapRange(x.Item1, x.Item2, SoilMap)).SelectMany(x => x)
            .Select(x => MapRange(x.Item1, x.Item2, FertMap)).SelectMany(x => x)
            .Select(x => MapRange(x.Item1, x.Item2, WaterMap)).SelectMany(x => x)
            .Select(x => MapRange(x.Item1, x.Item2, LightMap)).SelectMany(x => x)
            .Select(x => MapRange(x.Item1, x.Item2, TempMap)).SelectMany(x => x)
            .Select(x => MapRange(x.Item1, x.Item2, HumidMap)).SelectMany(x => x)
            .Select(x => MapRange(x.Item1, x.Item2, LocMap)).SelectMany(x => x)
            .MinBy(x => x.Item1).Item1;

        /*
        for (long i = 0; i < long.MaxValue; i++)
        {
            long seed = MapRev(MapRev(MapRev(MapRev(MapRev(MapRev(MapRev(i, LocMap), HumidMap), TempMap), LightMap), WaterMap), FertMap), SoilMap);
            for (int j = 0; j < seeds.Length; j += 2)
            {
                if (seed > seeds[j] && seed < seeds[j] + seeds[j + 1]) return i;
            }
        }
        
        return long.MaxValue;
        */
    }

    private (long, long, long)[] ParseMap(StreamReader reader)
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

    private long MapRev(in long dest, in (long dest, long src, long len)[] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (dest < map[i].dest || dest >= map[i].dest + map[i].len) continue;
            return map[i].src + (dest - map[i].dest);
        }
        return dest;
    }

    private IEnumerable<(long, long)> MapRange(in long sourceStart, in long sourceOffset, in (long dest, long src, long len)[] map)
    {
        List<(long, long)> ranges = new();
        var currStart = sourceStart;
        var currOffset = sourceOffset;
        bool consumed = false;
        while (!consumed)
        {
            for (int i = 0; i < map.Length; i++)
            {
                if (currStart >= map[i].src && currStart < map[i].src + map[i].len)
                {
                    if (currStart + currOffset <= map[i].src + map[i].len)
                    {
                        ranges.Add((map[i].dest + (currStart - map[i].src), currOffset));
                        consumed = true;
                        goto skip;
                    }
                    var newOffset = map[i].src + map[i].len - currStart;
                    ranges.Add((map[i].dest + (currStart - map[i].src), newOffset));
                    currStart = map[i].src + map[i].len;
                    currOffset = currOffset - newOffset;
                    goto skip;
                }
            }
            int nextRange = -1;
            long nextDist = long.MaxValue;
            for (int i = 0; i < map.Length; i++)
            {
                if (map[i].src >= currStart && map[i].src - currStart + currOffset < 0)
                {
                    if (map[i].src - currStart < nextDist)
                    {
                        nextDist = map[i].src - currStart;
                        nextRange = i;
                    }
                }
            }
            if (nextRange >= 0)
            {
                var newOffset = map[nextRange].src - currStart;
                ranges.Add((currStart, newOffset));
                currStart = map[nextRange].src;
                currOffset = currOffset - newOffset;
                continue;
            }
            ranges.Add((currStart, currOffset));
            consumed = true;
        skip:;
        }

        return ranges;
    }

}