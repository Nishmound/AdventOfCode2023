namespace AdventOfCode2023;

using System.Collections.Generic;
using Position = Helper.Position2D;
internal class Day03 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        var (nums, syms, bp) = Parse(reader);
        int sum = 0;
        foreach (var num in nums)
        {
            bool isPart = false;
            foreach (var sym in syms)
            {
                if (num.adj.Contains(sym.pos))
                {
                    isPart = true;
                    break;
                }
            }
            if (isPart) sum += num.num;
        }

        return sum;
    }

    public int RunP2(StreamReader reader)
    {
        var (nums, syms, bp) = Parse(reader);
        int sum = 0;
        foreach (var sym in syms.Where(x => x.sym == '*'))
        {
            List<int> parts = new();
            foreach (var num in nums)
            {
                if (num.adj.Contains(sym.pos))
                {
                    parts.Add(num.num);
                }
            }
            if (parts.Count == 2) sum += parts[0] * parts[1];
        }

        return sum;
    }

    private ((int num, Position pos, int length, Position[] adj)[],
        (char sym, Position pos, Position[] adj)[], char[,]) Parse(StreamReader reader)
    {
        List<char[]> bpR = new();
        string? line;
        while ((line = reader.ReadLine()) != null)
            bpR.Add(line.ToCharArray());
        reader.Close();
        char[,] bp = new char[bpR[0].Length, bpR.Count];
        for (int i = 0; i < bpR.Count; i++)
        {
            for (int j = 0; j < bpR[0].Length; j++)
            {
                bp[j, i] = bpR[i][j];
            }
        }

        List<(int, Position, int, Position[])> nums = new();
        List<(char, Position, Position[])> syms = new();

        for (int y = 0; y < bp.GetLength(1); y++)
        {
            bool readNum = false;
            string rawNum = "";
            Position currStart = new();
            int currLen = 0;
            for (int x = 0; x < bp.GetLength(0); x++)
            {
                if (readNum)
                {
                    if (char.IsDigit(bp[x, y]))
                    {
                        rawNum += bp[x, y];
                        currLen++;
                    }
                    else
                    {
                        readNum = false;
                        nums.Add((int.Parse(rawNum), currStart, currLen, Adjacents(currStart, currLen)));
                        rawNum = "";
                        currLen = 0;
                    }
                }
                else
                {
                    if (char.IsDigit(bp[x, y]))
                    {
                        readNum = true;
                        rawNum += bp[x, y];
                        currLen++;
                        currStart = (x, y);
                    }
                }
                if (bp[x, y] != '.' && !char.IsDigit(bp[x, y]))
                {
                    syms.Add((bp[x, y], (x, y), Adjacents((x, y))));
                }
            }
            if (readNum)
            {
                nums.Add((int.Parse(rawNum), currStart, currLen, Adjacents(currStart, currLen)));
            }
        }
        return (nums.ToArray(), syms.ToArray(), bp);
    }

    private Position[] Adjacents(Position start, int length = 1)
    {
        List<Position> adj = new();
        for (int i = 0; i < length; i++)
        {
            adj.Add((start.X + i, start.Y - 1));
            adj.Add((start.X + i, start.Y + 1));
            if (i == 0)
            {
                adj.Add((start.X - 1, start.Y));
                adj.Add((start.X - 1, start.Y + 1));
                adj.Add((start.X - 1, start.Y - 1));
            }
            if (i == length - 1)
            {
                adj.Add((start.X + length, start.Y));
                adj.Add((start.X + length, start.Y + 1));
                adj.Add((start.X + length, start.Y - 1));
            }
        }
        return adj.ToArray();
    }
}