﻿using AdventOfCode2023;

Console.WriteLine("Advent of Code\n$year=2023;");
write_empty();

#if !DEBUG
RunDay(new Day01(),
    "input_day01",
    "Day 1: Trebuchet?!",
    "What is the sum of all of the calibration values?",
    "What is the sum of all of the calibration values?");

RunDay(new Day02(),
    "input_day02",
    "Day 2: Cube Conundrum",
    "What is the sum of the IDs of those games?",
    "What is the sum of the power of these sets?");

RunDay(new Day03(),
    "input_day03",
    "Day 3: Gear Ratios",
    "What is the sum of all of the part numbers in the engine schematic?",
    "What is the sum of all of the gear ratios in your engine schematic?");

RunDay(new Day04(),
    "input_day04",
    "Day 4: Scratchcards",
    "How many points are they worth in total?",
    "how many total scratchcards do you end up with?");

RunDay(new Day05(),
    "input_day05",
    "Day 5: If You Give A Seed A Fertilizer",
    "What is the lowest location number that corresponds to any of the initial seed numbers?",
    "What is the lowest location number that corresponds to any of the initial seed numbers?");

RunDay(new Day06(),
    "input_day06",
    "Day 6: Wait For It",
    "What do you get if you multiply these numbers together?",
    "How many ways can you beat the record in this one much longer race?");

RunDay(new Day07(),
    "input_day07",
    "Day 7: Camel Cards",
    "What are the total winnings?",
    "What are the total winnings?");

RunDay(new Day08(),
    "input_day08",
    "Day 8: Haunted Wasteland",
    "How many steps are required to reach ZZZ?",
    "How many steps does it take before you're only on nodes that end with Z?");
#endif

RunDay(new Day09(),
    "input_day09",
    "Day 9: Mirage Maintenance",
    "What is the sum of these extrapolated values?",
    "What is the sum of these extrapolated values?");

Console.ReadLine();

static void write_sep() => Console.WriteLine(string.Concat(Enumerable.Repeat("#", 50)));
static void write_empty() => Console.WriteLine();

static void RunDay<T>(AdventDay<T> day, string inputPath, string title, string p1Text, string p2Text)
{
    write_empty();
    write_sep();
    Console.WriteLine($"--- {title} ---");
    write_empty();
    T result;
    var watch = System.Diagnostics.Stopwatch.StartNew();
    using (var reader = new StreamReader($@"inputs\{inputPath}.txt"))
        result = day.RunP1(reader);
    watch.Stop();
    Console.WriteLine($"{p1Text}:\n{result}");
    write_empty();
    Console.WriteLine($"Executed in {watch.ElapsedMilliseconds} ms");
    write_empty();
    Console.WriteLine("--- Part Two ---");
    write_empty();
    watch = System.Diagnostics.Stopwatch.StartNew();
    using (var reader = new StreamReader($@"inputs\{inputPath}.txt"))
        result = day.RunP2(reader);
    watch.Stop();
    Console.WriteLine($"{p2Text}:\n{result}");
    write_empty();
    Console.WriteLine($"Executed in {watch.ElapsedMilliseconds} ms");
}
