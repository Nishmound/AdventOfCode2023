namespace AdventOfCode2023;
using System.Text.RegularExpressions;

internal class Day01 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        List<int> values = new();
        string? line;
        string pattern = @"(\d)";
        while ((line = reader.ReadLine()) != null)
        {
            values.Add(int.Parse(
                Regex.Match(line, pattern).Groups[1].Value
                + Regex.Match(string.Concat(line.Reverse()), pattern).Groups[1].Value));
        }
        return values.Sum();
    }

    public int RunP2(StreamReader reader)
    {
        List<int> values = new();
        string pFirst = @"(\d|one|two|three|four|five|six|seven|eight|nine)";
        string pLast = @"(\d|eno|owt|eerht|ruof|evif|xis|neves|thgie|enin)";
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            values.Add(getValue(line, pFirst, pLast));
        }
        return values.Sum();
    }

    private static string ParseNum(in string num) => num switch
    {
        "one" => "1",
        "two" => "2",
        "three" => "3",
        "four" => "4",
        "five" => "5",
        "six" => "6",
        "seven" => "7",
        "eight" => "8",
        "nine" => "9",
        "eno" => "1",
        "owt" => "2",
        "eerht" => "3",
        "ruof" => "4",
        "evif" => "5",
        "xis" => "6",
        "neves" => "7",
        "thgie" => "8",
        "enin" => "9",
        _ => num
    };

    private static int getValue(string line, string pFirst, string pLast)
    {
        string first = ParseNum(Regex.Match(line, pFirst).Groups[1].Value);
        string last = ParseNum(Regex.Match(string.Concat(line.Reverse()), pLast).Groups[1].Value);
        return int.Parse(first + last);
    }
}