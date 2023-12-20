namespace AdventOfCode2023;
using Vec = Helper.Vector2DInt;
internal class Day17 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        var map = Parse(reader);
        var max = map.GetLength(0);
        var target = new Vec(max - 1, max - 1);

        Dictionary<(Vec, Direction, int), int> dist = new();
        HashSet<Vertex> q = [new(0, Direction.Start, 0, new(0,0))];

        while (q.Count != 0)
        {
            var v = q.MinBy(x => x.Cost);
            q.Remove(v);
            foreach (var ndir in NextDirs(v.Dir, v.Streak))
            {
                var npos = v.Pos + DirVec(ndir);
                if (CheckBounds(max, npos))
                {
                    var streak = v.Dir == ndir ? v.Streak + 1 : 1;
                    var ncost = v.Cost + map[npos.X, npos.Y];
                    if (!dist.ContainsKey((npos, ndir, streak)) || dist[(npos, ndir, streak)] > ncost)
                    {
                        dist[(npos, ndir, streak)] = ncost;
                        q.Add(new(ncost, ndir, streak, npos));
                    }
                }
            }
        }

        var min = int.MaxValue;
        for (int i = 1; i <= 4; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                var key = (target, (Direction)i, j);
                if (dist.ContainsKey(key) && dist[key] < min) min = dist[key];
            }
        }
        return min;
    }

    public int RunP2(StreamReader reader)
    {
        var map = Parse(reader);
        var max = map.GetLength(0);
        var target = new Vec(max - 1, max - 1);

        Dictionary<(Vec, Direction, int), int> dist = new();
        HashSet<Vertex> q = [new(0, Direction.Start, 0, new(0, 0))];

        while (q.Count != 0)
        {
            var v = q.MinBy(x => x.Cost);
            q.Remove(v);
            foreach (var ndir in NextDirs2(v.Dir, v.Streak))
            {
                if (ndir != v.Dir && !CheckBounds(max, v.Pos + 4 * DirVec(ndir))) continue;
                var npos = v.Pos + DirVec(ndir);
                if (npos.X >= 0 && npos.Y >= 0 && npos.X < max && npos.Y < max)
                {
                    var streak = v.Dir == ndir ? v.Streak + 1 : 1;
                    var ncost = v.Cost + map[npos.X, npos.Y];
                    if (!dist.ContainsKey((npos, ndir, streak)) || dist[(npos, ndir, streak)] > ncost)
                    {
                        dist[(npos, ndir, streak)] = ncost;
                        q.Add(new(ncost, ndir, streak, npos));
                    }
                }
            }
        }

        var min = int.MaxValue;
        for (int i = 1; i <= 4; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                var key = (target, (Direction)i, j);
                if (dist.ContainsKey(key) && dist[key] < min) min = dist[key];
            }
        }
        return min;
    }

    private readonly record struct Vertex(
        int Cost,
        Direction Dir,
        int Streak,
        Vec Pos);

    private static bool CheckBounds(int max, Vec pos)
    {
        return pos.X >= 0 && pos.Y >= 0 && pos.X < max && pos.Y < max;
    }

    private Vec DirVec(Direction dir) => dir switch
    {
        Direction.Up => new(0, -1),
        Direction.Down => new(0, 1),
        Direction.Left => new(-1, 0),
        Direction.Right => new(1, 0),
        _ => throw new ArgumentException(
            "Argument " + nameof(Direction) + '.' + dir + " not supported for " + nameof(DirVec))
    };

    private IEnumerable<Direction> NextDirs(Direction dir, int streak) => dir switch
    {
        Direction.Start => [Direction.Down, Direction.Right],
        Direction.Up => streak == 3 
        ? [Direction.Left, Direction.Right] : [Direction.Up, Direction.Left, Direction.Right],
        Direction.Down => streak == 3
        ? [Direction.Left, Direction.Right] : [Direction.Down, Direction.Left, Direction.Right],
        Direction.Left => streak == 3
        ? [Direction.Up, Direction.Down] : [Direction.Up, Direction.Down, Direction.Left],
        Direction.Right => streak == 3
        ? [Direction.Up, Direction.Down] : [Direction.Up, Direction.Down, Direction.Right],
        _ => throw new ArgumentException(
            "Argument " + nameof(Direction) + '.' + dir + " not supported for " + nameof(NextDirs))
    };

    private IEnumerable<Direction> NextDirs2(Direction dir, int streak) => dir switch
    {
        Direction.Start => [Direction.Down, Direction.Right],
        Direction.Up => streak < 4 ? [Direction.Up] :
        streak < 10 ? [Direction.Up, Direction.Left, Direction.Right] : [Direction.Left, Direction.Right],
        Direction.Down => streak < 4 ? [Direction.Down] :
        streak < 10 ? [Direction.Down, Direction.Left, Direction.Right] : [Direction.Left, Direction.Right],
        Direction.Left => streak < 4 ? [Direction.Left] :
        streak < 10 ? [Direction.Up, Direction.Down, Direction.Left] : [Direction.Up, Direction.Down],
        Direction.Right => streak < 4 ? [Direction.Right] :
        streak < 10 ? [Direction.Up, Direction.Down, Direction.Right] : [Direction.Up, Direction.Down],
        _ => throw new ArgumentException(
            "Argument " + nameof(Direction) + '.' + dir + " not supported for " + nameof(NextDirs))
    };

    private static byte[,] Parse(StreamReader reader)
    {
        string? line = reader.ReadLine() ?? "";
        byte[,] map = new byte[line.Length, line.Length];
        int y = 0;
        do
        {
            for (int x = 0; x < line.Length; x++)
            {
                map[x, y] = (byte)(line[x] - '0');
            }
            y++;
        } while ((line = reader.ReadLine()) != null);

        return map;
    }

    private enum Direction
    {
        Start,
        Up,
        Down,
        Left,
        Right
    }
}