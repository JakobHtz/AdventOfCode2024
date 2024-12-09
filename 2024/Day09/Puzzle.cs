namespace _2024.Day09;

public class Puzzle
{
    public static void Part1()
    {
        var line = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day09/input.txt").First();
        var map = new DiskMap();
        var id = 0;
        for (var i = 0; i< line.Length; i++)
        {
            bool isEven = i%2 == 0;
            
            for (int j = 0; j < int.Parse(line[i].ToString()); j++)
            {
                map.FragmentedMap.Add(isEven ? id : null);
            }
            id += isEven ? 1 : 0;
        }
        //map.FragmentedMap.Select(e => e == null ? "." : e.ToString()).ToList().ForEach(e => Console.Write(e));
        //Console.WriteLine();
        //map.DefragmentedMap.Select(e => e == null ? "." : e.ToString()).ToList().ForEach(e => Console.Write(e));
        //Console.WriteLine();
        Console.WriteLine(map.Checksum);
    }
}

public record DiskMap
{
    public List<int?> FragmentedMap { get; set; } = new List<int?>();
    public List<int?> DefragmentedMap => DeframentMap([..FragmentedMap]);
    public List<int?> DefragmentedMapSoft => DeframentMapSoft([..FragmentedMap]);
    public long Checksum => GetCheckSum([..DefragmentedMap]);

    public List<int?> DeframentMap(List<int?> map)
    {
        var last = 0;
        for (int i = map.Count-1; i >= 0; i--)
        {
            var current = map[i];
            for (int j = last; j < map.Count; j++)
            {
                if (map[j] == null)
                {
                    if (j >= i) return map; 
                    map[j] = current;
                    last = j;
                    break;
                }
            }
            map[i] = null;
        }
        return map;
    }
    
    public List<int?> DeframentMapSoft(List<int?> map)
    {
        throw new NotImplementedException();
    }

    public long GetCheckSum(List<int?> map)
    {
        long sum = 0;
        for (int i = 0; i < map.Count; i++)
        {
            sum += 1L*(map[i]??0) * i;
        }
        return sum;
    }
}