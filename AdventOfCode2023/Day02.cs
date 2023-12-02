
internal class Day02 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        int sum = 0;
        int count = 1;
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            bool valid = true;
            string[] parts = line.Split(':')[1].Split([';', ',']);
            foreach (var item in parts)
            {
                var num = int.Parse(item.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]);
                if ((item.Contains("red") && num > 12)
                    || (item.Contains("green") && num > 13)
                    || (item.Contains("blue") && num > 14))
                {
                    valid = false; break;
                }
            }
            if (valid)
            {
                sum += count;
            }
            count++;
        }
        return sum;
    }

    public int RunP2(StreamReader reader)
    {
        int sum = 0;
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            int maxRed = 0, maxGreen = 0, maxBlue = 0;
            string[] parts = line.Split(':')[1].Split([';', ',']);
            foreach (var item in parts)
            {
                var num = int.Parse(item.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]);
                if (item.Contains("red") && (num > maxRed)) maxRed = num;
                else if (item.Contains("green") && (num > maxGreen)) maxGreen = num;
                else if (item.Contains("blue") && (num > maxBlue)) maxBlue = num;
            }
            sum += maxRed * maxGreen * maxBlue;
        }
        return sum;
    }

}