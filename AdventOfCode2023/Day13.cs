
internal class Day13 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        int sum = 0;
        while (reader.Peek() != -1)
        {
            var (Hlines, Vlines) = ParsePattern(reader);
            var refPos = FindRef(Hlines);
            if (refPos >= 0)
            {
                sum += 100 * (refPos + 1);
                continue;
            }
            refPos = FindRef(Vlines);
            if (refPos >= 0)
            {
                sum += refPos + 1;
            }
            else throw new ArgumentException("Pattern not valid");
        }
        
        return sum;
    }

    public int RunP2(StreamReader reader)
    {
        int sum = 0;
        while (reader.Peek() != -1)
        {
            var (Hlines, Vlines) = ParsePattern(reader);
            var refPos = FindRef2(Hlines);
            if (refPos >= 0)
            {
                sum += 100 * (refPos + 1);
                continue;
            }
            refPos = FindRef2(Vlines);
            if (refPos >= 0)
            {
                sum += refPos + 1;
            }
            else throw new ArgumentException("Pattern not valid");
        }

        return sum;
    }

    private (string[], string[]) ParsePattern(StreamReader reader)
    {
        string? line = reader.ReadLine() ?? "";
        string[] Vlines = new string[line.Length];
        List<string> Hlines = new();

        do
        {
            Hlines.Add(line);
            for (int i = 0; i < line.Length; i++)
            {
                Vlines[i] += line[i];
            }
        } while (!string.IsNullOrWhiteSpace(line = reader.ReadLine()));

        return (Hlines.ToArray(), Vlines);
    }

    private int FindRef(string[] lines)
    {
        var refPos = -1;

        for (int i = 0; i < lines.Length - 1; i++)
        {
            if (lines[i] == lines[i + 1])
            {
                refPos = i;
                var up = i;
                var down = i + 1;
                while (up > 0 && down < lines.Length - 1)
                {
                    up--;
                    down++;
                    if (lines[up] != lines[down])
                    {
                        refPos = -1;
                        break;
                    }
                }
                if (refPos >= 0) break;
            }
        }

        return refPos;
    }

    private int FindRef2(string[] lines)
    {
        var refPos = -1;

        for (int i = 0; i < lines.Length - 1; i++)
        {
            var diffs = Diff(lines[i], lines[i + 1]);
            if (diffs <= 1)
            {
                refPos = i;
                var up = i;
                var down = i + 1;
                while (up > 0 && down < lines.Length - 1)
                {
                    up--;
                    down++;
                    diffs += Diff(lines[up], lines[down]);
                    if (diffs > 1)
                    {
                        refPos = -1;
                        break;
                    }
                }
                if (diffs == 0) refPos = -1;
                if (refPos >= 0) break;
            }
        }

        return refPos;
    }

    private int Diff(string s1, string s2) => s1.Zip(s2).Count(c => c.First != c.Second);
}