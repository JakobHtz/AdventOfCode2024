namespace _2024.Day05;

public class Puzzle
{
    public static void Part1()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day05/input.txt");

        var orderRules = new List<OrderRule>();
        var updates = new List<List<int>>();
        
        foreach (var line in lines)
        {
            if (line.Contains("|"))
            {
                var parts = line.Split("|");
                orderRules.Add(new OrderRule()
                {
                    X = int.Parse(parts[0]),
                    Y = int.Parse(parts[1]),
                });
            }

            if (line.Contains(","))
            {
                updates.Add(new List<int>(line.Split(',').Select(s => int.Parse(s))));
            }
        }

        var sum = 0;
        foreach (var update in updates)
        {
            var isValid = true;
            for (int i = 0; i < update.Count; i++)
            {
                var needsToComeBefore = orderRules.FindAll(e => e.Y == update[i]).Select(e => e.X).ToList();
                var needsToComeAfter = orderRules.FindAll(e => e.X == update[i]).Select(e => e.Y).ToList();

                if (update.Take(Range.StartAt(i)).ToList().Any(e => needsToComeBefore.Contains(e))) 
                {
                    isValid = false;
                    break;
                }
                if (update.Take(Range.EndAt(i)).ToList().Any(e => needsToComeAfter.Contains(e)))
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                sum += update[update.Count/2];
            }
        }
        Console.WriteLine(sum);
    }
    
    public static void Part2()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day05/input.txt");
        var orderRules = new List<OrderRule>();
        var updates = new List<List<int>>();
        var orderedUpdates = new List<List<int>>();

        foreach (var line in lines)
        {
            if (line.Contains("|"))
            {
                var parts = line.Split("|");
                orderRules.Add(new OrderRule()
                {
                    X = int.Parse(parts[0]),
                    Y = int.Parse(parts[1]),
                });
            }

            if (line.Contains(","))
            {
                updates.Add(new List<int>(line.Split(',').Select(S => int.Parse(S))));    
            }
        }

        var unorderedUpdates = new List<List<int>>();
        foreach (var update in updates)
        {
            var isValid = true;
            for (int i = 0; i < update.Count; i++)
            {
                if (update[i] != update[update.Count - 1] && isValid)
                {
                    var needsToComeBefore = orderRules.FindAll(e => e.Y == update[i]).Select(e => e.X).ToList();
                    var needsToComeAfter = orderRules.FindAll(e => e.X == update[i]).Select(e => e.Y).ToList();

                    if (update.Take(Range.StartAt(i)).ToList().Any(e => needsToComeBefore.Contains(e))) 
                    {
                        isValid = false;
                        break;
                    }
                    if (update.Take(Range.EndAt(i)).ToList().Any(e => needsToComeAfter.Contains(e)))
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            if (!isValid)
            {
                unorderedUpdates.Add(new List<int>(update));
            }
        }

        foreach (var update in unorderedUpdates)
        {
            var orderedList = new List<int>();
            foreach (var value in update)
            {
                var needsToComeBefore = orderRules.FindAll(e => e.Y == value).Select(e => e.X).ToList();
                var needsToComeAfter = orderRules.FindAll(e => e.X == value).Select(e => e.Y).ToList();

                var index = 0;
                for (int i = 0; i < orderedList.Count; i++)
                {
                    if (orderedList.Take(Range.EndAt(i)).ToList().Any(e => needsToComeBefore.Contains(e))) 
                    {
                        index = -1;
                        continue;
                    }
                    if (orderedList.Take(Range.StartAt(i)).ToList().Any(e => needsToComeAfter.Contains(e)))
                    {
                        index = -1;
                        continue;
                    }

                    index = i;
                    break;
                }
                if (index >= 0)
                    orderedList.Insert(index, value);
                else if (orderedList.Any(e => needsToComeAfter.Contains(e)))
                    orderedList.Add(value);
            }
            orderedUpdates.Add(orderedList);
        }

        var sum = 0;
        foreach (var orderedUpdate in orderedUpdates)
        {
            sum += orderedUpdate[orderedUpdate.Count/2];
        }
        Console.WriteLine(sum);
        
        void InsertIntoListByRuleSet(int insert, List<int> list)
        {
            
        }
    }
}

public class OrderRule()
{
    public required int X { get; init; }
    public required int Y { get; init; }
}