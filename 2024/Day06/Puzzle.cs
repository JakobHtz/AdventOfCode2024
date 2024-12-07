namespace _2024.Day06;

public class Puzzle
{
    public static void Part1()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day06/input.txt");

        var tmpTiles = new List<MapTile>();
        var guardPos = -1;
        var mapSize = lines.First().Length;
        foreach (var line in lines)
        {
            foreach (var tile in line.ToArray())
            {
                switch (tile)
                {
                    case '.':
                        tmpTiles.Add(new MapTile()
                        {
                            Walkable = true
                        });
                        break;
                    case '#':
                        tmpTiles.Add(new MapTile()
                        {
                            Walkable = false
                        });
                        break;
                    case '^':
                        tmpTiles.Add(new MapTile()
                        {
                            Walkable = true
                        });
                        guardPos = tmpTiles.Count - 1;
                        break;
                }
            }
        }

        var map = new GuardMap()
        {
            GuardPos = guardPos,
            MapLength = mapSize,
            Map = tmpTiles.ToArray()
        };

        while (map.NextStep())
        {
            //map.PrintMap();
            //Thread.Sleep(100);
        }

        Console.WriteLine(map.GetTilesVisited());
    }
    
    public static void Part2()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day06/input.txt");
        
        var tiles = new List<MapTile>();
        var guardPos = -1;
        var mapSize = lines.First().Length;
        foreach (var line in lines)
        {
            foreach (var tile in line.ToArray())
            {
                switch (tile)
                {
                    case '.':
                        tiles.Add(new MapTile()
                        {
                            Walkable = true
                        });
                        break;
                    case '#':
                        tiles.Add(new MapTile()
                        {
                            Walkable = false
                        });
                        break;
                    case '^':
                        tiles.Add(new MapTile()
                        {
                            Walkable = true
                        });
                        guardPos = tiles.Count - 1;
                        break;
                }
            }
        }

        var tmpTiles =  new List<MapTile>(tiles.Select(e => new MapTile(){Walkable = e.Walkable}));
        var map = new GuardMap()
        {
            GuardPos = guardPos,
            MapLength = mapSize,
            Map = tmpTiles.ToArray()
        };
        
        List<int> possibleTiles = new List<int>();
        while (map.NextStep())
        {
            possibleTiles.Add(map.GuardPos);
        }

        possibleTiles = possibleTiles.Distinct().ToList();

        var sum = 0;
        foreach (var pTile in possibleTiles)
        {
            tmpTiles = new List<MapTile>(tiles.Select(e => new MapTile(){Walkable = e.Walkable}));
            tmpTiles[pTile].Walkable = false;
            var tmpMap = new GuardMap()
            {
                GuardPos = guardPos,
                MapLength = mapSize,
                Map = tmpTiles.ToArray()
            };
            
            var count = 0;
            while (tmpMap.NextStep())
            {
                count++;
                if (tmpMap.Direction == tmpMap.Map[tmpMap.GuardPos].LastDirection && tmpMap.Map[tmpMap.GuardPos].Visits > 0)
                {
                    sum++;
                    break;
                }
            }
        }
        Console.WriteLine(sum);
    }
}

public class MapTile
{
    public bool Walkable { get; set; }
    public int Visits { get; private set; } = 0;
    public Directions? LastDirection { get; set; } = null;
    public void Visited(Directions direction)
    {
        LastDirection = direction;
        Visits = Visits +1;
    }
}

public class GuardMap
{
    public int GuardPos { get; set; }
    public Directions Direction { get; set; } = Directions.NORTH;
    public MapTile[] Map { get; init; }
    public int MapLength { get; init; }

    public bool NextStep()
    {
        var nextPos = -1;
        switch (Direction)
        { 
            case Directions.NORTH:
                nextPos = GuardPos - MapLength;
                if (nextPos < 0)
                {
                    return false;
                }
                
                if (Map[nextPos].Walkable)
                {
                    Map[GuardPos].Visited(Directions.NORTH);
                    GuardPos = nextPos;
                }
                else
                {
                    Direction = Directions.EAST;
                }
                break;
            case Directions.EAST:
                nextPos = GuardPos+1;
                if ((int)GuardPos / MapLength != (int)nextPos / MapLength)
                {
                    return false;
                }

                if (Map[nextPos].Walkable)
                {
                    Map[GuardPos].Visited(Directions.EAST);
                    GuardPos = nextPos;
                }
                else
                {
                    Direction = Directions.SOUTH;
                }
                break;
            case Directions.WEST:
                nextPos = GuardPos-1;
                if ((int)GuardPos / MapLength != (int)nextPos / MapLength)
                {
                    return false;
                }

                if (Map[nextPos].Walkable)
                {
                    Map[GuardPos].Visited(Directions.WEST);
                    GuardPos = nextPos;
                }
                else
                {
                    Direction = Directions.NORTH;
                }
                break;
            case Directions.SOUTH:
                nextPos = GuardPos + MapLength;
                if (nextPos >= Map.Length)
                {
                    return false;
                }

                if (Map[nextPos].Walkable)
                {
                    Map[GuardPos].Visited(Directions.SOUTH);
                    GuardPos = nextPos;
                }
                else
                {
                    Direction = Directions.WEST;
                }
                break;
        }
        return true;
    }

    public void PrintMap()
    {
        Console.Clear();
        for (var i = 0; i < Map.Length; i++)
        {
            if (i%MapLength == 0)
                Console.Write("\n");
            if (i!=GuardPos)
                Console.Write(Map[i].Walkable ? '.' : "#");
            else
                Console.Write("+");
        }
    }

    public int GetTilesVisited()
    {
        return Map.Count(x => x.Visits > 0);
    }
}

public enum Directions
{
    NORTH = 0,
    EAST = 1,
    SOUTH = 2,
    WEST = 3
}
