namespace AdventOfCode2023;
using Position = Helper.Position2DLong;
internal class Day11 : AdventDay<long>
{
    public long RunP1(StreamReader reader)
    {
        var galaxies = Parse(reader);

        var x = 0;
        while (!galaxies.All(g => g.X <= x))
        {
            if (!galaxies.Any(g => g.X == x))
            {
                for (int i = 0; i < galaxies.Length; i++)
                    if (galaxies[i].X > x) galaxies[i] = new(galaxies[i].X + 1, galaxies[i].Y);
                x += 2;
            }
            else x++;
        }
        var y = 0;
        while (!galaxies.All(g => g.Y <= y))
        {
            if (!galaxies.Any(g => g.Y == y))
            {
                for (int i = 0; i < galaxies.Length; i++)
                    if (galaxies[i].Y > y) galaxies[i] = new(galaxies[i].X, galaxies[i].Y + 1);
                y += 2;
            }
            else y++;
        }
        var sum = 0L;
        for (int i = 0; i < galaxies.Length; i++)
        {
            for (int j = i + 1; j < galaxies.Length; j++)
            {
                sum += galaxies[i].MDist(galaxies[j]);
            }
        }

        return sum;
    }

    public long RunP2(StreamReader reader)
    {
        var galaxies = Parse(reader);

        var x = 0L;
        while (!galaxies.All(g => g.X <= x))
        {
            if (!galaxies.Any(g => g.X == x))
            {
                for (int i = 0; i < galaxies.Length; i++)
                    if (galaxies[i].X > x) galaxies[i] = new(galaxies[i].X + 999999, galaxies[i].Y);
                x += 1000000;
            }
            else x++;
        }
        var y = 0L;
        while (!galaxies.All(g => g.Y <= y))
        {
            if (!galaxies.Any(g => g.Y == y))
            {
                for (int i = 0; i < galaxies.Length; i++)
                    if (galaxies[i].Y > y) galaxies[i] = new(galaxies[i].X, galaxies[i].Y + 999999);
                y += 1000000;
            }
            else y++;
        }
        var sum = 0L;
        for (int i = 0; i < galaxies.Length; i++)
        {
            for (int j = i + 1; j < galaxies.Length; j++)
            {
                sum += galaxies[i].MDist(galaxies[j]);
            }
        }

        return sum;
    }

    private Position[] Parse(StreamReader reader)
    {
        List<Position> galaxies = new();
        int y = 0;
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == '#') galaxies.Add(new(x, y));
            }
            y++;
        }
        return galaxies.ToArray();
    }
}