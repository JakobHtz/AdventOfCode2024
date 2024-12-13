using System.Diagnostics.CodeAnalysis;

namespace _2024.Day10;

public class Puzzle
{

    public static void Part1()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day10/input.txt");
        var map = new List<int>();
        var mapSize = lines.Length;
        foreach (var line in lines)
        {
            foreach (var x in line)
            {
                map.Add(int.Parse(x.ToString()));
            }
        }

        var sum = 0;
        for (var y = 0; y < map.Count(); y++)
        {
            if (map[y] == 0)
            {
                var score = FindTrail(y,0);
                Console.WriteLine(score.Distinct().Count());
                score.Distinct().ToList().ForEach(Console.WriteLine);
                sum += score.Distinct().Count();
            }
        }
        Console.WriteLine(sum);

        List<Point> FindTrail(int index, int pre)
        {
            var score = new List<Point>();

            if (pre == 8 && map[index] == 9)
            {
                return new List<Point>()
                { 
                    new Point()
                    {
                        X = index%mapSize,
                        Y = index/mapSize
                    }
                };
            }

            var indexRight = index + 1;
            if ((index / mapSize == indexRight / mapSize) &&
                indexRight < map.Count &&
                map[indexRight] == map[index] + 1)
            {
                score.AddRange(FindTrail(indexRight, map[index]));
            }
            var indexLeft = index - 1;
            if ((index / mapSize == indexLeft / mapSize) && 
                indexLeft >= 0 &&
                map[indexLeft] == map[index] + 1)
            {
                score.AddRange(FindTrail(indexLeft, map[index]));
            }
            var indexDown = index + mapSize;
            if ((indexDown < map.Count) && map[indexDown] == map[index] + 1)
            {
                score.AddRange(FindTrail(indexDown, map[index]));
            }
            var indexUp = index - mapSize;
            if ((indexUp >= 0) && map[indexUp] == map[index] + 1)
            {
                score.AddRange(FindTrail(indexUp, map[index]));
            }
            return score;
        }
    }
    
    public static void Part2()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day10/input.txt");
        var map = new List<int>();
        var mapSize = lines.Length;
        foreach (var line in lines)
        {
            foreach (var x in line)
            {
                map.Add(int.Parse(x.ToString()));
            }
        }

        var sum = 0;
        for (var y = 0; y < map.Count(); y++)
        {
            if (map[y] == 0)
            {
                var score = FindTrail(y,0);
                Console.WriteLine(score);
                sum += score;
            }
        }
        Console.WriteLine(sum);

        int FindTrail(int index, int pre)
        {
            var score = 0;

            if (pre == 8 && map[index] == 9)
            {
                return 1;
            }

            var indexRight = index + 1;
            if ((index / mapSize == indexRight / mapSize) &&
                indexRight < map.Count &&
                map[indexRight] == map[index] + 1)
            {
                score += FindTrail(indexRight, map[index]);
            }
            var indexLeft = index - 1;
            if ((index / mapSize == indexLeft / mapSize) && 
                indexLeft >= 0 &&
                map[indexLeft] == map[index] + 1)
            {
                score += FindTrail(indexLeft, map[index]);
            }
            var indexDown = index + mapSize;
            if ((indexDown < map.Count) && map[indexDown] == map[index] + 1)
            {
                score += FindTrail(indexDown, map[index]);
            }
            var indexUp = index - mapSize;
            if ((indexUp >= 0) && map[indexUp] == map[index] + 1)
            {
                score += FindTrail(indexUp, map[index]);
            }
            return score;
        }
    }
}

public record Point
{
    public int X { get; init; }
    public int Y { get; init; }
}