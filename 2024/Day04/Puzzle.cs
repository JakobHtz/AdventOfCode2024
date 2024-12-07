namespace _2024.Day04;

public class Puzzle
{
    public static void Part1()
    {
        
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day04/input.txt");
        var y = lines.ToArray();
        var sum = 0;
        for (int i = 0; i < y.Length; i++)
        {
            var xm3 = i<3 ? null : lines[i - 3].ToCharArray();
            var xm2 = i<2 ? null : lines[i - 2].ToCharArray();
            var xm1 = i<1 ? null : lines[i - 1].ToCharArray();
            var x = lines[i].ToCharArray();
            var xp1 = i>=x.Length-1 ? null : lines[i + 1].ToCharArray();
            var xp2 = i>=x.Length-2 ? null : lines[i + 2].ToCharArray();
            var xp3 = i>=x.Length-3 ? null : lines[i + 3].ToCharArray();

            for (int j = 0; j < y.Length; j++)
            {
                if (j<x.Length-3)
                    if (x[j]=='X')
                        if (x[j+1]=='M')
                            if (x[j+2]=='A')
                                if (x[j+3] == 'S')
                                    sum++;
                if (j<x.Length-3 && i<y.Length-3)
                    if (x[j]=='X')
                        if (xp1[j+1]=='M')
                            if (xp2[j+2]=='A')
                                if (xp3[j+3] == 'S')
                                    sum++;
                if (i<y.Length-3)
                    if (x[j]=='X')
                        if (xp1[j]=='M')
                            if (xp2[j]=='A')
                                if (xp3[j] == 'S')
                                    sum++;
                if (j>=3 && i>=3)
                    if (x[j]=='X')
                        if (xm1[j-1]=='M')
                            if (xm2[j-2]=='A')
                                if (xm3[j-3] == 'S')
                                    sum++;
                if (i>=3)
                    if (x[j]=='X')
                        if (xm1[j]=='M')
                            if (xm2[j]=='A')
                                if (xm3[j] == 'S')
                                    sum++;
                if (j>=3)
                    if (x[j]=='X')
                        if (x[j-1]=='M')
                            if (x[j-2]=='A')
                                if (x[j-3] == 'S')
                                    sum++;
                if (j>=3 && i<y.Length-3)
                    if (x[j]=='X')
                        if (xp1[j-1]=='M')
                            if (xp2[j-2]=='A')
                                if (xp3[j-3] == 'S')
                                    sum++;
                if (j<x.Length-3 && i>=3)
                    if (x[j]=='X')
                        if (xm1[j+1]=='M')
                            if (xm2[j+2]=='A')
                                if (xm3[j+3] == 'S')
                                    sum++;
            }
        }
        Console.WriteLine(sum);
    }
    
    public static void Part2()
    {
        
        var lines = File.ReadAllLines("/home/jakob/source/repos/AdventOfCode/2024/Day04/input.txt");
        var y = lines.ToArray();
        var sum = 0;
        for (int i = 1; i < y.Length-1; i++)
        {
            var xm1 = i<1 ? null : lines[i - 1].ToCharArray();
            var x = lines[i].ToCharArray();
            var xp1 = i>=x.Length-1 ? null : lines[i + 1].ToCharArray();

            for (int j = 1; j < y.Length-1; j++)
            {
                if (x[j] == 'A')
                {
                    var mas = 
                        xp1[j + 1].ToString() +
                        'A'.ToString() +
                        xm1[j - 1].ToString();
                    var sam = 
                        xp1[j - 1].ToString() +
                        'A'.ToString() +
                        xm1[j + 1].ToString();
                    if (mas =="MAS" || mas=="SAM")
                        if (sam =="MAS" || sam=="SAM")
                            sum++;
                        
                }
            }
        }
        Console.WriteLine(sum);
    }
}
