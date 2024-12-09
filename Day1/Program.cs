namespace Day1;

class Program
{
    static readonly string[] input = File.ReadAllLines("C:\\Users\\sergei.maltcev\\Projects\\AdventOfCode2024\\Day1\\input.txt");

    static void Main(string[] args)
    {
        int[] leftColumn = new int[input.Length];
        int[] rightColumn = new int[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            var split = input[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            leftColumn[i] = int.Parse(split[0]);
            rightColumn[i] = int.Parse(split[1]);

        }

        PartOne(leftColumn, rightColumn);
        PartTwo(leftColumn, rightColumn);
    }

    private static void PartTwo(int[] leftColumn, int[] rightColumn)
    {
        int sum = leftColumn
            .Select(left => left * rightColumn.Count(right => right == left))
            .Sum();

        Console.WriteLine(sum);
    }

    private static void PartOne( int[] leftColumn, int[] rightColumn)
    {
        Array.Sort(leftColumn);
        Array.Sort(rightColumn);

        var totalDistance = 0;

        for (int i = 0; i < input.Length; i++)
        {
            var diff = Math.Abs(leftColumn[i] - rightColumn[i]);
            Console.WriteLine(diff);
            totalDistance += diff;
        }

        Console.WriteLine(totalDistance);
    }
}