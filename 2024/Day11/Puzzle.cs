namespace _2024.Day11;

public class Puzzle
{

    public static void Part1()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day11/input.txt");
        var stones = lines.First().Split(' ').Select(long.Parse).ToList();
        for (int i = 0; i < 25; i++)
        {
            List<Task<List<long>>> tasks = new List<Task<List<long>>>();
            foreach (var stone in stones)
            {
                tasks.Add(Task.Run(() => Blink(stone)));
            }
            Task.WaitAll(tasks.ToArray());
            var list = new List<long>();
            tasks.ForEach(t => list.AddRange(t.Result));
            stones = list;
        }
        Console.WriteLine(stones.Count);
    }

    public static List<long> Blink(long number)
    {
        var newNumbers = new List<long>();
        if (number == 0)
        {
            newNumbers = newNumbers.Append(1).ToList();
        }
        else if (number.ToString().Length%2 == 0)
        {
            var upper = number / (long)Math.Pow(10, number.ToString().Length/2);
            var lower = number % (long)Math.Pow(10, number.ToString().Length/2);
            newNumbers = newNumbers.Append(upper).ToList();
            newNumbers = newNumbers.Append(lower).ToList();
        }
        else
        {
            newNumbers = newNumbers.Append(number * 2024).ToList();
        }
        return newNumbers;
    }
}
