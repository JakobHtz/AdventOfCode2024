namespace _2024.Day08;

public class Puzzle
{

    public static void Part1()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day08/input.txt");

        var antannas = new List<Antanna>();
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                var chr = lines[i][j];
                if (chr != '.')
                {
                    antannas.Add(new Antanna()
                    {
                        Type = chr,
                        Position = new Point()
                        {
                            X = j,
                            Y = i
                        }
                    });
                }
            }
        }

        var mapSize = new Point()
        {
            Y = lines.Length,
            X = lines.First().Length
        };

        var results = new List<Point>();
        var types = antannas.DistinctBy(e => e.Type).Select(e => e.Type).ToList();
        types.ForEach(e =>
        {
            List<Antanna> tmpAnt = [..antannas];
            tmpAnt = tmpAnt.FindAll(a => a.Type == e);
            var points = getAntinodes(tmpAnt);
            results.AddRange(points);
        });
        
        var dist = results.Distinct().ToList();
        //dist.RemoveAll(e => antannas.Any(o => o.Position == e));
        Console.WriteLine(dist.Count);
        for (int i = 0; i < mapSize.Y*mapSize.X; i++)
        {
            if (i % mapSize.X == 0)
            {
                Console.WriteLine();
            }

            var p = dist.Find(e =>
            {
                if (e.X + e.Y * mapSize.X == i) 
                    return true;
                return false;
            });
            var a = antannas.Find(e =>
            {
                if (e.Position.X + e.Position.Y * mapSize.X == i) 
                    return true;
                return false;
            });
            if (p != null)
            {
                Console.Write("#");
                continue;
            }
            if (a != null)
            {
                Console.Write(a.Type);
                continue;
            }
            Console.Write('.');
        }
        
        List<Point> getAntinodes(List<Antanna> antannas)
        {
            var points = new List<Point>();
            foreach (var first in antannas)
            {
                List<Antanna> tmpAntannas = [..antannas];
                tmpAntannas.Remove(first);

                foreach (var antanna in tmpAntannas)
                {
                    var dist = first.Position.GetDistanceTo(antanna.Position);
                    var bBox = first.Position.GetBBox(antanna.Position);

                    var lowPoint = new Point()
                    {
                        X = (bBox.Item3) ? bBox.Item1.X - dist.X : bBox.Item1.X - dist.X,
                        Y = (bBox.Item3) ? bBox.Item1.Y - dist.Y : bBox.Item2.Y + dist.Y
                    };
                    var highPoint = new Point()
                    {
                        X = (bBox.Item3) ? bBox.Item2.X + dist.X : bBox.Item2.X + dist.X,
                        Y = (bBox.Item3) ? bBox.Item2.Y + dist.Y : bBox.Item1.Y - dist.Y
                    };
                    if (!(lowPoint.X < 0 || lowPoint.Y < 0 || lowPoint.X >= mapSize.X || lowPoint.Y >= mapSize.Y))
                    {
                        points.Add(lowPoint);
                    }
                    if (!(highPoint.X >= mapSize.X || highPoint.Y >= mapSize.Y || highPoint.X < 0 || highPoint.Y < 0))
                    {
                        points.Add(highPoint);
                    }   
                }
            }
            return points.Distinct().ToList();
        }
    }
    
    public static void Part2()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day08/input.txt");

        var antannas = new List<Antanna>();
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                var chr = lines[i][j];
                if (chr != '.')
                {
                    antannas.Add(new Antanna()
                    {
                        Type = chr,
                        Position = new Point()
                        {
                            X = j,
                            Y = i
                        }
                    });
                }
            }
        }

        var mapSize = new Point()
        {
            Y = lines.Length,
            X = lines.First().Length
        };

        var results = new List<Point>();
        var types = antannas.DistinctBy(e => e.Type).Select(e => e.Type).ToList();
        types.ForEach(e =>
        {
            List<Antanna> tmpAnt = [..antannas];
            tmpAnt = tmpAnt.FindAll(a => a.Type == e);
            var points = getAntinodes(tmpAnt);
            results.AddRange(points);
        });
        
        var dist = results.Distinct().ToList();
        //dist.RemoveAll(e => antannas.Any(o => o.Position == e));
        Console.WriteLine(dist.Count);
        for (int i = 0; i < mapSize.Y*mapSize.X; i++)
        {
            if (i % mapSize.X == 0)
            {
                Console.WriteLine();
            }

            var p = dist.Find(e =>
            {
                if (e.X + e.Y * mapSize.X == i) 
                    return true;
                return false;
            });
            var a = antannas.Find(e =>
            {
                if (e.Position.X + e.Position.Y * mapSize.X == i) 
                    return true;
                return false;
            });
            if (p != null)
            {
                Console.Write("#");
                continue;
            }
            if (a != null)
            {
                Console.Write(a.Type);
                continue;
            }
            Console.Write('.');
        }
        
        List<Point> getAntinodes(List<Antanna> antannas)
        {
            var points = new List<Point>();
            foreach (var first in antannas)
            {
                List<Antanna> tmpAntannas = [..antannas];
                points.Add(first.Position);
                tmpAntannas.Remove(first);

                foreach (var antanna in tmpAntannas)
                {
                    var dist = first.Position.GetDistanceTo(antanna.Position);
                    var bBox = first.Position.GetBBox(antanna.Position);

                    Point lowPoint;
                    var lastXPos = (bBox.Item3) ? bBox.Item1.X - dist.X : bBox.Item1.X - dist.X;
                    var lastYPos = (bBox.Item3) ? bBox.Item1.Y - dist.Y : bBox.Item2.Y + dist.Y;
                    do
                    {
                        lowPoint = new Point()
                        {
                            X = lastXPos,
                            Y = lastYPos,
                        };
                        if (!(lowPoint.X < 0 || lowPoint.Y < 0 || lowPoint.X >= mapSize.X || lowPoint.Y >= mapSize.Y))
                        {
                            points.Add(lowPoint);
                        }
                        lastXPos = (bBox.Item3) ? lastXPos - dist.X : lastXPos - dist.X;
                        lastYPos = (bBox.Item3) ? lastYPos - dist.Y : lastYPos + dist.Y;
                    } while (!(lastXPos < 0 || lastYPos < 0 || lastXPos == mapSize.X || lastYPos >= mapSize.Y));
                    
                    Point highPoint;
                    var lastX = (bBox.Item3) ? bBox.Item2.X + dist.X : bBox.Item2.X + dist.X;
                    var lastY = (bBox.Item3) ? bBox.Item2.Y + dist.Y : bBox.Item1.Y - dist.Y;
                    do
                    {
                        highPoint = new Point()
                        {
                            X = lastX,
                            Y = lastY,
                        };
                        if (!(highPoint.X < 0 || highPoint.Y < 0 || highPoint.X >= mapSize.X || highPoint.Y >= mapSize.Y))
                        {
                            points.Add(highPoint);
                        }
                        lastX = (bBox.Item3) ? lastX + dist.X : lastX + dist.X;
                        lastY = (bBox.Item3) ? lastY + dist.Y : lastY - dist.Y;
                    } while (!(lastX < 0 || lastY < 0 || lastX == mapSize.X || lastY >= mapSize.Y));
                    
                    if (!(highPoint.X >= mapSize.X || highPoint.Y >= mapSize.Y || highPoint.X < 0 || highPoint.Y < 0))
                    {
                        points.Add(highPoint);
                    }   
                }
            }
            return points.Distinct().ToList();
        }
    }
}

public record Antanna : IComparable<Antanna> 
{
    public char Type { get; set; }
    public Point Position { get; set; }

    public int CompareTo(Antanna? other)
    {
        return Position.CompareTo(other.Position);
    }
}

public record Point : IComparable<Point>
{
    public int X { get; set; }
    public int Y { get; set; }
    
    public Point GetDistanceTo(Point other)
    {
        return new Point()
        {
            X = Math.Abs(X - other.X),
            Y = Math.Abs(Y - other.Y)
        };
    }

    public (Point, Point, bool) GetBBox(Point point)
    {
        var lower = new Point()
        {
            X = Math.Min(X, point.X),
            Y = Math.Min(Y, point.Y),
        };
        var higher = new Point()
        {
            X = Math.Max(X, point.X),
            Y = Math.Max(Y, point.Y),
        };
        var lowerPoint = this.CompareTo(point)<0? this : point;
        var higherPoint = this.CompareTo(point)>=0? this : point;
        var isSinking = lowerPoint.X < higherPoint.X && lowerPoint.Y < higherPoint.Y;
        
        return (lower, higher, isSinking);
    }

    public int CompareTo(Point? other)
    {
        var p1 = X + Y * 1000;
        var p2 = other.X + other.Y * 1000;
        return p1 - p2;
    }
}