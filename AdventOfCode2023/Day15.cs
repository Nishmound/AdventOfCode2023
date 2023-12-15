
internal class Day15 : AdventDay<long>
{
    public long RunP1(StreamReader reader)
    {
        var sum = 0;
        var hash = 0;
        int c;
        while ((c = reader.Read()) >= 0)
        {
            if (c == '\n' || c == '\r') continue;
            else if (c == ',')
            {
                sum += hash;
                hash = 0;
            }
            else
            {
                hash += c;
                hash *= 17;
                hash %= 256;
            }
        }
        sum += hash;
        return sum;
    }

    public long RunP2(StreamReader reader)
    {
        HashMap map = new();
        List<char> buf = new();
        int c = reader.Read();
        while (true)
        {
            if (c == '\n' || c == '\r') { c = reader.Read(); continue; }
            if (c == ',' || c == -1)
            {
                if (char.IsAsciiDigit(buf[^1]))
                {
                    map.Insert(new(string.Concat(buf[..^2]), buf[^1] - '0'));
                }
                else map.Remove(string.Concat(buf[..^1]));
                if (c == -1) break;
                buf.Clear();
            }
            else buf.Add((char)c);
            c = reader.Read();
        }

        return map.Sum();
    }

    private class HashMap
    {
        private List<KeyValuePair<string, int>>[] map;
        
        public List<KeyValuePair<string, int>> this[int i]
        {
            get { return map[i]; }
        }

        public HashMap()
        {
            map = new List<KeyValuePair<string, int>>[256];
            for (int i = 0; i < 256; i++) map[i] = new();
        }

        public void Insert(KeyValuePair<string, int> kvpair)
        {
            var hash = Hash(kvpair.Key);
            var len = map[hash].Count();
            for (int i = 0; i < len; i++)
            {
                if (map[hash][i].Key == kvpair.Key)
                {
                    map[hash][i] = kvpair;
                    return;
                }
            }
            map[hash].Add(kvpair);
        }

        public void Show()
        {
            for (int i = 0; i < 256; i++)
            {
                var len = map[i].Count();
                if (len == 0) continue;
                Console.Write("Box " + i + ":");
                for (int j = 0; j < len; j++)
                {
                    Console.Write(" [" + map[i][j].Key + ' ' + map[i][j].Value + ']');
                }
                Console.WriteLine();
            }
        }

        public void Remove(string key)
        {
            var hash = Hash(key);
            var len = map[hash].Count();
            for (int i = 0; i < len; i++)
            {
                if (map[hash][i].Key == key)
                {
                    map[hash].RemoveAt(i);
                    return;
                }
            }
        }

        public long Sum()
        {
            var sum = 0L;
            for (int i = 0; i < 256; i++)
            {
                var len = map[i].Count();
                for (int j =  0; j < len; j++)
                {
                    sum += (i + 1) * (j + 1) * map[i][j].Value;
                }
            }
            return sum;
        }

        private int Hash(string line)
        {
            var val = 0;
            for (int i = 0; i < line.Length; i++)
            {
                val += line[i];
                val *= 17;
                val %= 256;
            }
            return val;
        }
    }
}