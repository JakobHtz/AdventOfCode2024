using System.Text.RegularExpressions;

namespace _2024.Day03;

public class Puzzle
{
    public void Part1()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day03/input.txt");
        var validMulEx = new Regex($"""(mul\(\d+\,\d+\))|(do\(\))|(don't\(\))""", RegexOptions.Compiled);
        var trimChars = new[] { 'm', 'u', 'l', '(', ')' };
        var instructions = new List<string>();
        foreach (var line in lines)
        {
            var captures = validMulEx.Matches(line);
            foreach (var capture in captures)
            {
                instructions.Add(capture.ToString()!);
            }
        }


        int sum = 0;
        bool multiply = true;
        foreach (var instruction in instructions)
        {
            if (instruction.StartsWith("mul") && multiply)
            {
                var numCsv = instruction.Trim(trimChars);
                string[] split = numCsv.Split(",");
                int x = int.Parse(split[0]);
                int y = int.Parse(split[1]);
                sum += x * y;
            }
        }
        Console.WriteLine(sum);
    }
    
    public void Part2()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day03/input.txt");
        var validMulEx = new Regex($"""(mul\(\d+\,\d+\))|(do\(\))|(don't\(\))""", RegexOptions.Compiled);
        var trimChars = new[] { 'm', 'u', 'l', '(', ')' };
        var instructions = new List<string>();
        foreach (var line in lines)
        {
            var captures = validMulEx.Matches(line);
            foreach (var capture in captures)
            {
                instructions.Add(capture.ToString()!);
            }
        }


        int sum = 0;
        bool multiply = true;
        foreach (var instruction in instructions)
        {
            if (instruction == "do()")
                multiply = true;
            if (instruction == "don't()")
                multiply = false;
            if (instruction.StartsWith("mul") && multiply)
            {
                var numCsv = instruction.Trim(trimChars);
                string[] split = numCsv.Split(",");
                int x = int.Parse(split[0]);
                int y = int.Parse(split[1]);
                sum += x * y;
            }
        }
        Console.WriteLine(sum);
    }
}