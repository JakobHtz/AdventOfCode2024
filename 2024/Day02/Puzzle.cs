using System.IO.Compression;

namespace _2024.Day02;

public class Puzzle
{
    public static void Part1()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day02/input.txt");
        var sum = 0;
        foreach (var line in lines)
        {
            var report = line.Split(' ');
            bool? flow = null;
            var isValid = true;
            for (int i = 0; i < report.Length-1; i++)
            {
                var a = int.Parse(report[i]);
                var b = int.Parse(report[i + 1]);
                flow ??= a - b < 0;
                var dif = Math.Abs(a - b);
                if (dif == 0 || dif > 3 || flow != a - b < 0)
                {
                    isValid = false;
                }
            }
            if (isValid)
                sum++;
        }
        Console.WriteLine(sum);
    }
    
    public static void Part2()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day02/input.txt");
        var sum = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) break;
            var report = line.Split(' ');
            bool flow = int.Parse(report.First()) - int.Parse(report.Last()) < 0;
            var isValid = true;
            var buffer = false;
            for (int i = -1; i < report.Length - 1; i++)
            {
                int a = i < 0 ? (flow?int.Parse(report.First()) -1 : int.Parse(report.First()) +1) : int.Parse(report[i]);
                var b = int.Parse(report[i + 1]);
                int c = i >= report.Length - 2 ? 0 : int.Parse(report[i + 2]);

                var ab = a - b;
                var ac = a - c;

                if (ab == 0 || Math.Abs(ab) > 3 || flow != ab < 0)
                {
                    if (buffer)
                    {
                        isValid = false;
                        break;
                    }
                    buffer = true;
                    if ((Math.Abs(ac) == 0 || Math.Abs(ac) > 3 || flow != ac < 0))
                    {
                        isValid = false;
                        break;
                    }
                    i++;
                }
            }
            if (isValid)
                sum++;
        }
        Console.WriteLine(sum);
    }
}