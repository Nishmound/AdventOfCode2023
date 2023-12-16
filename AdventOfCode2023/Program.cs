using AdventOfCode2023;

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

RunDay(new Day09(),
    "input_day09",
    "Day 9: Mirage Maintenance",
    "What is the sum of these extrapolated values?",
    "What is the sum of these extrapolated values?");

RunDay(new Day10(),
    "input_day10",
    "Day 10: Pipe Maze",
    "How many steps along the loop does it take to get from the starting position\n" +
    "to the point farthest from the starting position?",
    "How many tiles are enclosed by the loop?");

RunDay(new Day11(),
    "input_day11",
    "Day 11: Cosmic Expansion",
    "What is the sum of these lengths?",
    "What is the sum of these lengths?");

RunDay(new Day12(),
    "input_day12",
    "Day 12: Hot Springs",
    "What is the sum of those counts?",
    "What is the sum of those counts?");

RunDay(new Day13(),
    "input_day13",
    "Day 13: Point of Incidence",
    "What number do you get after summarizing all of your notes?",
    "What number do you get after summarizing all of your notes?");

RunDay(new Day14(),
    "input_day14",
    "Day 14: Parabolic Reflector Dish",
    "What is the total load on the north support beams?",
    "What is the total load on the north support beams?");

RunDay(new Day15(),
    "input_day15",
    "Day 15: Lens Library",
    "What is the sum of the results?",
    "What is the sum of the results?");

#endif

RunDay(new Day16(),
    "input_day16",
    "Day 16: The Floor Will Be Lava",
    "How many tiles end up being energized?",
    "How many tiles end up being energized?");

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
