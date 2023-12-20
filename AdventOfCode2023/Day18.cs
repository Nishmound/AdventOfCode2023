namespace AdventOfCode2023;

using System.Globalization;
using Vec = Helper.Vector2DLong;
internal class Day18 : AdventDay<long>
{
    public long RunP1(StreamReader reader)
    {
        List<Vec> verts = [new(0, 0)];
        var prev = Direction.Up;
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split(' ');
            var dir = Dir(parts[0][0]);
            var dist = int.Parse(parts[1]);

            if (IsRight(dir, prev))
            {
                verts[^1] = verts[^1] - DirVec(prev);
                verts.Add(verts[^1] + DirVec(dir) * dist);
                prev = dir;
            }
            else
            {
                verts.Add(verts[^1] + DirVec(dir) * (dist + 1));
                prev = dir;
            }
        }

        var sum = 0L;
        for (int i = 0; i < verts.Count() - 1; i++)
        {
            sum += verts[i].X * verts[i + 1].Y - verts[i + 1].X * verts[i].Y;
        }

        return sum/2;
    }

    public long RunP2(StreamReader reader)
    {
        List<Vec> verts = [new(0, 0)];
        var prev = Direction.Up;
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split(' ');
            var dir = (Direction)(parts[2][^2] - '0');
            var dist = int.Parse(parts[2][2..^2], NumberStyles.HexNumber);

            if (IsRight(dir, prev))
            {
                verts[^1] = verts[^1] - DirVec(prev);
                verts.Add(verts[^1] + DirVec(dir) * dist);
                prev = dir;
            }
            else
            {
                verts.Add(verts[^1] + DirVec(dir) * (dist + 1));
                prev = dir;
            }
        }

        var sum = 0L;
        for (int i = 0; i < verts.Count() - 1; i++)
        {
            checked
            {
                sum += verts[i].X * verts[i + 1].Y - verts[i + 1].X * verts[i].Y;
            }
        }

        return sum / 2;
    }

    private enum Direction
    {
        Right,
        Down,
        Left,
        Up
    }

    private Direction Dir(char dir) => dir switch
    {
        'R' => Direction.Right,
        'L' => Direction.Left,
        'U' => Direction.Up,
        'D' => Direction.Down,
        _ => throw new ArgumentException()
    };

    private bool IsRight(Direction curr, Direction prev) => curr switch
    {
        Direction.Up => prev == Direction.Right,
        Direction.Down => prev == Direction.Left,
        Direction.Right => prev == Direction.Down,
        Direction.Left => prev == Direction.Up,
        _ => throw new ArgumentException()
    };

    private Vec DirVec(Direction dir) => dir switch
    {
        Direction.Up => Vec.Down,
        Direction.Down => Vec.Up,
        Direction.Right => Vec.Right,
        Direction.Left => Vec.Left,
        _ => throw new ArgumentException()
    };
}