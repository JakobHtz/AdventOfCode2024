namespace _2024.Day07;

public class Puzzle
{
    public static void Part1()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day07/input.txt");

        var equations = new List<Equation>();
        foreach (var line in lines)
        {
            var equation = line.Split(":");
            var result = long.Parse(equation[0]);
            var operands = equation[1]
                .Trim()
                .Split(" ")
                .Select(e => int.Parse(e))
                .ToList();
            
            equations.Add(new Equation()
            {
                Result = result,
                Operands = operands
            });
        }

        foreach (var equation in equations)
        {
            equation.Solve();
        }

        var solved = equations.FindAll(e => e.IsSolved);
        long sum = 0;
        foreach (var s in solved)
        {
            sum += s.Result;
        }
        Console.WriteLine(sum);
    }
    
    public static void Part2()
    {
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day07/input.txt");

        var equations = new List<Equation>();
        foreach (var line in lines)
        {
            var equation = line.Split(":");
            var result = long.Parse(equation[0]);
            var operands = equation[1]
                .Trim()
                .Split(" ")
                .Select(e => int.Parse(e))
                .ToList();
            
            equations.Add(new Equation()
            {
                Result = result,
                Operands = operands
            });
        }

        foreach (var equation in equations)
        {
            equation.SolveConcat();
        }

        var solved = equations.FindAll(e => e.IsSolved);
        long sum = 0;
        foreach (var s in solved)
        {
            sum += s.Result;
        }
        Console.WriteLine(sum);
    }
}

public class Equation
{
    public long Result { get; set; }
    public bool IsSolved { get; set; } = false;
    public List<int> Operands { get; init;  } = new List<int>();
    public List<string> Operators { get; } = new List<string>();
    
    public List<string> Values { get; } = new List<string>();

    public void Solve()
    {
        List<int> operands = [..Operands];
        operands.RemoveAt(0);
        Solve(Operands.First(), operands);
    }
    private bool Solve(long previousResult, List<int> nextOperands)
    {
        if (nextOperands.Count <= 0)
        {
            if (Result == previousResult)
            {
                IsSolved = true;
                return true;
            }
            return false;
        }
            
        var nextOperand = nextOperands.First();
        nextOperands.RemoveAt(0);

        long nextResultPlus = previousResult + nextOperand;
        long nextResultMultiply = previousResult * nextOperand;
        if (Solve(nextResultPlus, [..nextOperands]))
        {
            Operators.Add("+");
            return true;
        }
        if (Solve(nextResultMultiply, [..nextOperands]))
        {
            Operators.Add("*");
            return true;
        };
        return false;
    }
    
    public void SolveConcat()
    {
        List<int> operands = [..Operands];
        operands.RemoveAt(0);
        SolveConcat(Operands.First(), operands);
    }
    private bool SolveConcat(long previousResult, List<int> nextOperands)
    {
        if (nextOperands.Count <= 0)
        {
            if (Result == previousResult)
            {
                IsSolved = true;
                return true;
            }
            return false;
        }
            
        var nextOperand = nextOperands.First();
        nextOperands.RemoveAt(0);

        long nextResultPlus = previousResult + nextOperand;
        long nextResultMultiply = previousResult * nextOperand;
        long nextResultConcat = long.Parse(
            previousResult.ToString() + 
            nextOperand.ToString());
        if (SolveConcat(nextResultPlus, [..nextOperands]))
        {
            Operators.Add("+");
            return true;
        }
        if (SolveConcat(nextResultMultiply, [..nextOperands]))
        {
            Operators.Add("*");
            return true;
        };
        if (SolveConcat(nextResultConcat, [..nextOperands]))
        {
            Operators.Add("||");
            return true;
        };
        return false;
    }
}
