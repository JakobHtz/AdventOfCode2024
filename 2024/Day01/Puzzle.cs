namespace _2024.Day01;

public class Puzzle
{
    public static void Part1()
    {
        var input = File.ReadAllText("/home/jakob/source/repos/AdventOfCode/2024/Day01/input.txt");
        var lines = input.Split('\n');

        var list0 = new List<int>();
        var list1 = new List<int>();
        foreach (var line in lines)
        {
            var splitList = line.Split("   ");
            if (!string.IsNullOrWhiteSpace(splitList[0]) && 
                !string.IsNullOrWhiteSpace(splitList[1]))
            {
                list0.Add(int.Parse(splitList[0]));
                list1.Add(int.Parse(splitList[1]));
            }
        }
        list0.Sort();
        list1.Sort();
        var dist = 0;
        for (var i = 0; i < list0.Count; i++)
        {
            dist += Math.Abs(list0[i] - list1[i]);
        }
        System.Console.WriteLine(dist);
    }
    
    public static void Part2()
    {
        var input = File.ReadAllText("/home/jakob/source/repos/AdventOfCode/2024/Day01/input.txt");
        var lines = input.Split('\n');

        var list0 = new List<int>();
        var list1 = new List<int>();
        foreach (var line in lines)
        {
            var splitList = line.Split("   ");
            if (!string.IsNullOrWhiteSpace(splitList[0]) && 
                !string.IsNullOrWhiteSpace(splitList[1]))
            {
                list0.Add(int.Parse(splitList[0]));
                list1.Add(int.Parse(splitList[1]));
            }
        }
        var simScore = 0;
        for (var i = 0; i < list0.Count; i++)
        {
            var num = list0[i];
            var oc = list1.FindAll(c => c.Equals(num));
            simScore += oc.Count*num;
        }
        System.Console.WriteLine(simScore);
    }
}