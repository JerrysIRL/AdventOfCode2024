namespace Day2;

class Program
{
    static string[] input = File.ReadAllLines("C:\\Users\\sergei.maltcev\\Projects\\AdventOfCode2024\\Day2\\input.txt");

    static void Main(string[] args)
    {
        int safeCount = 0;
        foreach (string line in input)
        {
            int[] levels = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            bool isIncreasing = DetermineDirection(levels);

            if (IsSafe(levels, isIncreasing))
            {
                safeCount++;
            }
            else
            {
                bool madeSafeByRemoval = false;
                for (int i = 0; i < levels.Length; i++)
                {
                    int[] modified = RemoveAtIndex(levels, i);
                    bool newDirection = DetermineDirection(modified);

                    if (IsSafe(modified, newDirection))
                    {
                        madeSafeByRemoval = true;
                        break;
                    }
                }

                if (madeSafeByRemoval)
                {
                    safeCount++;
                }
            }
        }

        Console.WriteLine(safeCount);
    }

    private static int[] RemoveAtIndex(int[] arr, int index)
    {
        var list = new List<int>(arr);
        list.RemoveAt(index);
        return list.ToArray();
    }

    private static bool DetermineDirection(int[] levels)
    {
        if (levels[0] < levels[1])
            return true;

        return false;
    }

    private static bool IsSafe(int[] level, bool isIncreasing)
    {
        for (int j = 0; j < level.Length - 1; j++)
        {
            int diff = Math.Abs(level[j] - level[j + 1]);
            if (diff == 0 || diff > 3) return false;

            if (isIncreasing && level[j] >= level[j + 1])
                return false;
            if (!isIncreasing && level[j] <= level[j + 1])
                return false;
        }

        return true;
    }
}