namespace AdventOfCode2023;
using Position = Helper.Vector2DInt;
internal class Day10 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        var (map, start) = Parse(reader);

        return FindLoop(map, start).Count() / 2;
    }

    public int RunP2(StreamReader reader)
    {
        var (map, start) = Parse(reader);
        var loop = FindLoop(map, start);

        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                if (!loop.Contains((x, y))) map[x, y] = 0;
            }
        }

        MarkInnerOuter(ref map, start, !isClockwise(loop));

        var enclosed = 0;
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                if (map[x, y] == 0) Fill((x, y), ref map);
                if ((map[x, y] & INNER) > 0) enclosed++;
            }
        }

        return enclosed;
    }

    private const byte UP = 1;
    private const byte DOWN = 2;
    private const byte RIGHT = 4;
    private const byte LEFT = 8;
    private const byte START = 16;
    private const byte INNER = 32;
    private const byte OUTER = 64;
    private const byte END = 128;
    private const byte DIR = UP | DOWN | LEFT | RIGHT;

    private void ShowMap(in byte[,] map)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                Console.Write(map[x, y] switch
                {
                    0 => '.',
                    UP | DOWN => '|',
                    LEFT | RIGHT => '-',
                    _ => throw new NotImplementedException()
                });
            }
        }
    }

    private Position[] FindLoop(byte[,] map, Position start)
    {
        var currPos = start;
        var currDir = map[start.X, start.Y];
        List<Position> loop = [start];

        if ((currDir & UP) > 0) { currDir = UP; currPos = (currPos.X, currPos.Y - 1); }
        else if ((currDir & DOWN) > 0) { currDir = DOWN; currPos = (currPos.X, currPos.Y + 1); }
        else if ((currDir & LEFT) > 0) { currDir = LEFT; currPos = (currPos.X - 1, currPos.Y); }
        else if ((currDir & RIGHT) > 0) { currDir = RIGHT; currPos = (currPos.X + 1, currPos.Y); }

        while ((map[currPos.X, currPos.Y] & START) == 0)
        {
            loop.Add(currPos);
            currDir = (byte)(InvDir(currDir) ^ (map[currPos.X, currPos.Y]));
            switch (currDir)
            {
                case UP: currPos = (currPos.X, currPos.Y - 1); break;
                case DOWN: currPos = (currPos.X, currPos.Y + 1); break;
                case LEFT: currPos = (currPos.X - 1, currPos.Y); break;
                case RIGHT: currPos = (currPos.X + 1, currPos.Y); break;
            }
        }
        return loop.ToArray();
    }

    private void MarkInnerOuter(ref byte[,] map, Position start, bool reverse)
    {
        var currPos = start;
        var currDir = map[start.X, start.Y];
        map[start.X, start.Y] = (byte)(map[start.X, start.Y] ^ START);

        if (!reverse)
        {
            if ((currDir & RIGHT) > 0) { currDir = RIGHT; currPos = (currPos.X + 1, currPos.Y); }
            else if ((currDir & LEFT) > 0) { currDir = LEFT; currPos = (currPos.X - 1, currPos.Y); }
            else if ((currDir & DOWN) > 0) { currDir = DOWN; currPos = (currPos.X, currPos.Y + 1); }
            else if ((currDir & UP) > 0) { currDir = UP; currPos = (currPos.X, currPos.Y - 1); }
        }
        else
        {
            if ((currDir & UP) > 0) { currDir = UP; currPos = (currPos.X, currPos.Y - 1); }
            else if ((currDir & DOWN) > 0) { currDir = DOWN; currPos = (currPos.X, currPos.Y + 1); }
            else if ((currDir & LEFT) > 0) { currDir = LEFT; currPos = (currPos.X - 1, currPos.Y); }
            else if ((currDir & RIGHT) > 0) { currDir = RIGHT; currPos = (currPos.X + 1, currPos.Y); }
        }

        var nextPos = currPos;
        var maxX = map.GetLength(0) - 1;
        var maxY = map.GetLength(1) - 1;

        do
        {
            currDir = (byte)(InvDir(currDir) ^ (map[currPos.X, currPos.Y]));

            switch (currDir)
            {
                case UP:
                    switch (map[currPos.X, currPos.Y] ^ currDir)
                    {
                        case DOWN:
                            if (currPos.X < maxX && (map[currPos.X + 1, currPos.Y] & DIR) == 0) map[currPos.X + 1, currPos.Y] = INNER;
                            if (currPos.X > 0 && (map[currPos.X - 1, currPos.Y] & DIR) == 0) map[currPos.X - 1, currPos.Y] = OUTER;
                            break;
                        case LEFT:
                            if (currPos.X < maxX && (map[currPos.X + 1, currPos.Y] & DIR) == 0) map[currPos.X + 1, currPos.Y] = INNER;
                            if (currPos.Y < maxY && (map[currPos.X, currPos.Y + 1] & DIR) == 0) map[currPos.X, currPos.Y + 1] = INNER;
                            break;
                        case RIGHT:
                            if (currPos.Y < maxY && (map[currPos.X, currPos.Y + 1] & DIR) == 0) map[currPos.X, currPos.Y + 1] = OUTER;
                            if (currPos.X > 0 && (map[currPos.X - 1, currPos.Y] & DIR) == 0) map[currPos.X - 1, currPos.Y] = OUTER;
                            break;
                    }
                    currPos = (currPos.X, currPos.Y - 1);
                    break;
                case DOWN:
                    switch (map[currPos.X, currPos.Y] ^ currDir)
                    {
                        case UP:
                            if (currPos.X > 0 && (map[currPos.X - 1, currPos.Y] & DIR) == 0) map[currPos.X - 1, currPos.Y] = INNER;
                            if (currPos.X < maxX && (map[currPos.X + 1, currPos.Y] & DIR) == 0) map[currPos.X + 1, currPos.Y] = OUTER;
                            break;
                        case LEFT:
                            if (currPos.Y > 0 && (map[currPos.X, currPos.Y - 1] & DIR) == 0) map[currPos.X, currPos.Y - 1] = OUTER;
                            if (currPos.X < maxX && (map[currPos.X + 1, currPos.Y] & DIR) == 0) map[currPos.X + 1, currPos.Y] = OUTER;
                            break;
                        case RIGHT:
                            if (currPos.X > 0 && (map[currPos.X - 1, currPos.Y] & DIR) == 0) map[currPos.X - 1, currPos.Y] = INNER;
                            if (currPos.Y > 0 && (map[currPos.X, currPos.Y - 1] & DIR) == 0) map[currPos.X, currPos.Y - 1] = INNER;
                            break;
                    }
                    currPos = (currPos.X, currPos.Y + 1);
                    break;
                case LEFT:
                    switch (map[currPos.X, currPos.Y] ^ currDir)
                    {
                        case UP:
                            if (currPos.X < maxX && (map[currPos.X + 1, currPos.Y] & DIR) == 0) map[currPos.X + 1, currPos.Y] = OUTER;
                            if (currPos.Y < maxY && (map[currPos.X, currPos.Y + 1] & DIR) == 0) map[currPos.X, currPos.Y + 1] = OUTER;
                            break;
                        case DOWN:
                            if (currPos.Y > 0 && (map[currPos.X, currPos.Y - 1] & DIR) == 0) map[currPos.X, currPos.Y - 1] = INNER;
                            if (currPos.X < maxX && (map[currPos.X + 1, currPos.Y] & DIR) == 0) map[currPos.X + 1, currPos.Y] = INNER;
                            break;
                        case RIGHT:
                            if (currPos.Y > 0 && (map[currPos.X, currPos.Y - 1] & DIR) == 0) map[currPos.X, currPos.Y - 1] = INNER;
                            if (currPos.Y < maxY && (map[currPos.X, currPos.Y + 1] & DIR) == 0) map[currPos.X, currPos.Y + 1] = OUTER;
                            break;
                    }
                    currPos = (currPos.X - 1, currPos.Y);
                    break;
                case RIGHT:
                    switch (map[currPos.X, currPos.Y] ^ currDir)
                    {
                        case UP:
                            if (currPos.Y < maxY && (map[currPos.X, currPos.Y + 1] & DIR) == 0) map[currPos.X, currPos.Y + 1] = INNER;
                            if (currPos.X > 0 && (map[currPos.X - 1, currPos.Y] & DIR) == 0) map[currPos.X - 1, currPos.Y] = INNER;
                            break;
                        case DOWN:
                            if (currPos.Y > 0 && (map[currPos.X, currPos.Y - 1] & DIR) == 0) map[currPos.X, currPos.Y - 1] = OUTER;
                            if (currPos.X > 0 && (map[currPos.X - 1, currPos.Y] & DIR) == 0) map[currPos.X - 1, currPos.Y] = OUTER;
                            break;
                        case LEFT:
                            if (currPos.Y > 0 && (map[currPos.X, currPos.Y - 1] & DIR) == 0) map[currPos.X, currPos.Y - 1] = OUTER;
                            if (currPos.Y < maxY && (map[currPos.X, currPos.Y + 1] & DIR) == 0) map[currPos.X, currPos.Y + 1] = INNER;
                            break;
                    }
                    currPos = (currPos.X + 1, currPos.Y);
                    break;
            }
        } while (currPos != nextPos);
    }

    private byte Fill(Position curr, ref byte[,] map)
    {
        byte fill = 0;
        map[curr.X, curr.Y] = END;
        if (curr.Y > 0 && map[curr.X, curr.Y - 1] != END)
        {
            if (map[curr.X, curr.Y - 1] == 0) fill = Fill((curr.X, curr.Y - 1), ref map);
            else fill = map[curr.X, curr.Y - 1];
            if (curr.Y < map.GetLength(1) - 1) map[curr.X, curr.Y + 1] = fill;
            if (curr.X > 0) map[curr.X - 1, curr.Y] = fill;
            if (curr.X < map.GetLength(0) - 1) map[curr.X + 1, curr.Y] = fill;
            map[curr.X, curr.Y] = fill;
            return fill;
        }
        if (curr.Y < map.GetLength(1) - 1 && map[curr.X, curr.Y + 1] != END)
        {
            if (map[curr.X, curr.Y + 1] == 0) fill = Fill((curr.X, curr.Y + 1), ref map);
            else fill = map[curr.X, curr.Y + 1];
            if (curr.Y > 0) map[curr.X, curr.Y - 1] = fill;
            if (curr.X > 0) map[curr.X - 1, curr.Y] = fill;
            if (curr.X < map.GetLength(0) - 1) map[curr.X + 1, curr.Y] = fill;
            map[curr.X, curr.Y] = fill;
            return fill;
        }
        if (curr.X > 0 && map[curr.X - 1, curr.Y] != END)
        {
            if (map[curr.X - 1, curr.Y] == 0) fill = Fill((curr.X - 1, curr.Y), ref map);
            else fill = map[curr.X - 1, curr.Y];
            if (curr.Y > 0) map[curr.X, curr.Y - 1] = fill;
            if (curr.Y < map.GetLength(1) - 1) map[curr.X, curr.Y + 1] = fill;
            if (curr.X < map.GetLength(0) - 1) map[curr.X + 1, curr.Y] = fill;
            map[curr.X, curr.Y] = fill;
            return fill;
        }
        if (curr.X < map.GetLength(0) - 1 && map[curr.X + 1, curr.Y] != END)
        {
            if (map[curr.X + 1, curr.Y] == 0) fill = Fill((curr.X + 1, curr.Y), ref map);
            else fill = map[curr.X + 1, curr.Y];
            if (curr.Y > 0) map[curr.X, curr.Y - 1] = fill;
            if (curr.Y < map.GetLength(1) - 1) map[curr.X, curr.Y + 1] = fill;
            if (curr.X > 0) map[curr.X - 1, curr.Y] = fill;
            map[curr.X, curr.Y] = fill;
            return fill;
        }
        return map[curr.X, curr.Y];
    }

    private bool isClockwise(Position[] curve)
    {
        var convPos = curve.Min();
        var convPosIdx = Array.IndexOf(curve, convPos);
        var convPosPrev = convPosIdx == 0 ? curve[^1] : curve[convPosIdx - 1];
        var convPosNext = convPosIdx == curve.Length - 1 ? curve[0] : curve[convPosIdx + 1];
        return (convPos.X * convPosNext.Y + convPosPrev.X * convPos.Y + convPosNext.X * convPosPrev.Y
            - (convPos.X * convPosPrev.Y + convPosNext.X * convPos.Y + convPosPrev.X * convPosNext.Y)) < 0;
    }

    private byte InvDir(byte dir) => dir switch
    {
        RIGHT => LEFT,
        LEFT => RIGHT,
        UP => DOWN,
        DOWN => UP,
        _ => dir
    };

    private (byte[,], Position) Parse(StreamReader reader)
    {
        List<byte[]> mapRaw = new();
        Position start = new();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            byte[] row = new byte[line.Length];
            for (int i = 0; i < row.Length; i++)
            {
                if (line[i] == 'S') start = (i, mapRaw.Count());
                row[i] = line[i] switch
                {
                    '|' => UP | DOWN,
                    '-' => LEFT | RIGHT,
                    'L' => UP | RIGHT,
                    'J' => UP | LEFT,
                    '7' => DOWN | LEFT,
                    'F' => DOWN | RIGHT,
                    'S' => START,
                    _ => 0
                };
            }
            mapRaw.Add(row);
        }
        reader.Close();

        byte[,] map = new byte[mapRaw[0].Length, mapRaw.Count()];
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                map[x, y] = mapRaw[y][x];
            }
        }

        if ((start.X < map.GetLength(0) - 1) && (map[start.X + 1, start.Y] & LEFT) > 0) map[start.X, start.Y] += RIGHT;
        if ((start.X > 0) && (map[start.X - 1, start.Y] & RIGHT) > 0) map[start.X, start.Y] += LEFT;
        if ((start.Y < map.GetLength(1) - 1) && (map[start.X, start.Y + 1] & UP) > 0) map[start.X, start.Y] += DOWN;
        if ((start.Y > 0) && (map[start.X, start.Y - 1] & DOWN) > 0) map[start.X, start.Y] += UP;

        return (map, start);
    }
}