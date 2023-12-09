
namespace AdventOfCode2023;

internal class Day09 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        return Parse(reader).Select(Extrapolate).Sum();
    }

    public int RunP2(StreamReader reader)
    {
        return Parse(reader).Select(x => x.Reverse().ToArray()).Select(Extrapolate).Sum();
    }

    private IEnumerable<int[]> Parse(StreamReader reader)
    {
        List<int[]> data = new();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            data.Add(line.Split(' ').Select(int.Parse).ToArray());
        }

        return data;
    }

    private int Extrapolate(int[] data) =>
        data.All(n => n == 0) 
        ? data[0] 
        : Extrapolate(Difference(data)) + data[^1];

    private int[] Difference(int[] ints) => ints[..^1].Select((n, i) => ints[i + 1] - n).ToArray();
    
}