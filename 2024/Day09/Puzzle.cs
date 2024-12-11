namespace _2024.Day09;

public class Puzzle
{
    public static void Part1()
    {
        var map = ReadMap();
        Console.WriteLine(map.Checksum());
    }
    
    public static void Part2()
    {
        var map = ReadMap();
        Console.WriteLine(map.Checksum(map.DefragmentedMapSoft));
    }

    private static DiskMap ReadMap()
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
        return map;
    }
}

public record DiskMap
{
    public List<int?> FragmentedMap { get; set; } = new List<int?>();
    public List<int?> DefragmentedMap => DefragmentMap([..FragmentedMap]);
    public List<int?> DefragmentedMapSoft => DefrgamentMapSoft([..FragmentedMap]);

    public long Checksum()
    {
        return GetCheckSum([..DefragmentedMap]);
    } 

    public long Checksum(List<int?> map)
    {
        return GetCheckSum([..map]);
    }

    public List<int?> DefragmentMap(List<int?> map)
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
    
    public List<int?> DefrgamentMapSoft(List<int?> map)
    {
        for (int i = map.Count - 1; i >= 0;)
        {
            var current = map[i];
            var fileLength = 1;
            for (int j = i-1; j >= 1; j--)
            {
                if (map[j] == current)
                    fileLength++;
                else
                    break;
            } 
            i -= fileLength;
            if (current==null)
                continue;
            
            var freeLength = 0;
            var freeIndex = 0;
            for (var j = 0; j < i+1 && freeLength!=fileLength; j++)
            {
                if (map[j] == null)
                    freeLength++;
                else
                {
                    freeIndex = j+1;
                    freeLength = 0;
                }
            }

            if (freeLength != fileLength)
                continue;
            
            map.RemoveRange(freeIndex, fileLength);
            map.InsertRange(freeIndex, new int?[fileLength].ToList().Select(x => current));
            map.RemoveRange(i+1, fileLength);
            map.InsertRange(i+1, new int?[fileLength].ToList().Select(x => x = null));
        }
        return map;
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