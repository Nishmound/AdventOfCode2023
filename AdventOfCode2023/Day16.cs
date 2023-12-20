namespace AdventOfCode2023;
using Position = Helper.Vector2DInt;
internal class Day16 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        char[,] map = Parse(reader);
        return Energize(new(0,0), Origin.LEFT, map);
    }

    public int RunP2(StreamReader reader)
    {
        var map = Parse(reader);
        int max = 0;
        var xM = map.GetLength(0);
        var yM = map.GetLength(1);

        for (int x = 0; x < xM; x++)
        {
            var top = Energize(new(x, 0), Origin.TOP, map);
            if (top > max) max = top;
            var bot = Energize(new(x, yM - 1), Origin.BOTTOM, map);
            if (bot > max) max = bot;
        }
        for (int y = 0; y < yM; y++)
        {
            var left = Energize(new(0, y), Origin.LEFT, map);
            if (left > max) max = left;
            var right = Energize(new(xM - 1, y), Origin.RIGHT, map);
            if (right > max) max = right;
        }
        return max;
    }

    private static char[,] Parse(StreamReader reader)
    {
        List<string> lines = new();
        string? line;
        while ((line = reader.ReadLine()) != null) lines.Add(line);
        reader.Close();

        char[,] map = new char[lines[0].Length, lines.Count];
        for (int y = 0; y < map.GetLength(1); y++)
            for (int x = 0; x < map.GetLength(0); x++)
                map[x, y] = lines[y][x];

        return map;
    }

    private enum Origin
    {
        TOP = 1,
        BOTTOM = 2,
        LEFT = 4,
        RIGHT = 8
    }

    private int Energize (in Position startPos, in Origin startOrg, in char[,] map)
    {
        bool[,] eng = new bool[map.GetLength(0), map.GetLength(1)];
        byte[,] vis = new byte[map.GetLength(0), map.GetLength(1)];
        Stack<(Position, Origin)> stack = new();
        stack.Push((startPos, startOrg));

        while (stack.Count != 0)
        {
            var (pos, org) = stack.Pop();
            if ((vis[pos.X, pos.Y] & (int)org) > 0) continue;
            else vis[pos.X, pos.Y] += (byte)org;
            eng[pos.X, pos.Y] = true;

            switch (org)
            {
                case Origin.TOP:
                    switch (map[pos.X, pos.Y])
                    {
                        case '/':
                            if (pos.X > 0) stack.Push((new(pos.X - 1, pos.Y), Origin.RIGHT));
                            break;
                        case '\\':
                            if (pos.X < map.GetLength(0) - 1) stack.Push((new(pos.X + 1, pos.Y), Origin.LEFT));
                            break;
                        case '-':
                            if (pos.X > 0) stack.Push((new(pos.X - 1, pos.Y), Origin.RIGHT));
                            if (pos.X < map.GetLength(0) - 1) stack.Push((new(pos.X + 1, pos.Y), Origin.LEFT));
                            vis[pos.X, pos.Y] += (byte)Origin.BOTTOM;
                            break;
                        default:
                            if (pos.Y < map.GetLength(1) - 1) stack.Push((new(pos.X, pos.Y + 1), org));
                            break;
                    }
                    break;
                case Origin.BOTTOM:
                    switch (map[pos.X, pos.Y])
                    {
                        case '/':
                            if (pos.X < map.GetLength(0) - 1) stack.Push((new(pos.X + 1, pos.Y), Origin.LEFT));
                            break;
                        case '\\':
                            if (pos.X > 0) stack.Push((new(pos.X - 1, pos.Y), Origin.RIGHT));
                            break;
                        case '-':
                            if (pos.X > 0) stack.Push((new(pos.X - 1, pos.Y), Origin.RIGHT));
                            if (pos.X < map.GetLength(0) - 1) stack.Push((new(pos.X + 1, pos.Y), Origin.LEFT));
                            vis[pos.X, pos.Y] += (byte)Origin.TOP;
                            break;
                        default:
                            if (pos.Y > 0) stack.Push((new(pos.X, pos.Y - 1), org));
                            break;
                    }
                    break;
                case Origin.LEFT:
                    switch (map[pos.X, pos.Y])
                    {
                        case '/':
                            if (pos.Y > 0) stack.Push((new(pos.X, pos.Y - 1), Origin.BOTTOM));
                            break;
                        case '\\':
                            if (pos.Y < map.GetLength(1) - 1) stack.Push((new(pos.X, pos.Y + 1), Origin.TOP));
                            break;
                        case '|':
                            if (pos.Y > 0) stack.Push((new(pos.X, pos.Y - 1), Origin.BOTTOM));
                            if (pos.Y < map.GetLength(1) - 1) stack.Push((new(pos.X, pos.Y + 1), Origin.TOP));
                            vis[pos.X, pos.Y] += (byte)Origin.RIGHT;
                            break;
                        default:
                            if (pos.X < map.GetLength(0) - 1) stack.Push((new(pos.X + 1, pos.Y), org));
                            break;
                    }
                    break;
                case Origin.RIGHT:
                    switch (map[pos.X, pos.Y])
                    {
                        case '/':
                            if (pos.Y < map.GetLength(1) - 1) stack.Push((new(pos.X, pos.Y + 1), Origin.TOP));
                            break;
                        case '\\':
                            if (pos.Y > 0) stack.Push((new(pos.X, pos.Y - 1), Origin.BOTTOM));
                            break;
                        case '|':
                            if (pos.Y > 0) stack.Push((new(pos.X, pos.Y - 1), Origin.BOTTOM));
                            if (pos.Y < map.GetLength(1) - 1) stack.Push((new(pos.X, pos.Y + 1), Origin.TOP));
                            vis[pos.X, pos.Y] += (byte)Origin.LEFT;
                            break;
                        default:
                            if (pos.X > 0) stack.Push((new(pos.X - 1, pos.Y), org));
                            break;
                    }
                    break;
            }
        }

        var sum = 0;
        foreach (var e in eng) if (e) sum++;

        return sum;
    }
}