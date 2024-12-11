// See https://aka.ms/new-console-template for more information

string[] input = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2024\Day4\input.txt");

const string goal = "XMAS";
const string goalPartTwo = "MAS";

PartTwo();

void PartOne()
{
    int count = 0;
    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[0].Length; x++)
        {
            if (input[y][x] == goal[0])
            {
                foreach (var direction in GetDirections())
                {
                    if (FindWord(input, x, y, goal, direction))
                    {
                        count++;
                    }
                }
            }
        }
    }

    Console.WriteLine("total : " + count);
}

void PartTwo()
{
    int count = 0;
    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[0].Length; x++)
        {
            if (input[y][x] == 'A')
            {
                if (CheckXmas(x, y))
                    count++;
            }
        }
    }

    Console.WriteLine("total : " + count);
}


static bool FindWord(string[] grid, int startX, int startY, string word, (int dirX, int dirY) direction)
{
    int x = startX;
    int y = startY;

    for (int i = 0; i < word.Length; i++)
    {
        if (x < 0 || x >= grid[0].Length || y < 0 || y >= grid.Length || grid[y][x] != word[i])
        {
            return false;
        }

        x += direction.dirX;
        y += direction.dirY;
    }

    return true;
}

bool CheckXmas(int x, int y)
{
    HashSet<char> set = new HashSet<char>();

    foreach (var dir in GetRightDiagonal())
    {
        int newX = x + dir.dx;
        int newY = y + dir.dy;

        if (IsInBounds(newX, newY, input))
        {
            set.Add(input[newY][newX]);
        }
    }

    if (!set.Contains('S') || !set.Contains('M'))
        return false;

    set.Clear();

    foreach (var dir in GetLeftDiagonal())
    {
        int newX = x + dir.dx;
        int newY = y + dir.dy;

        if (IsInBounds(newX, newY, input))
        {
            set.Add(input[newY][newX]);
        }
    }

    if (!set.Contains('S') || !set.Contains('M'))
        return false;

    return true;
}

static bool IsInBounds(int x, int y, string[] grid)
{
    return x >= 0 && x < grid[0].Length && y >= 0 && y < grid.Length;
}

static List<(int dx, int dy)> GetDirections()
{
    return new List<(int, int)>
    {
        (-1, -1), (0, -1), (1, -1),
        (-1, 0), (1, 0),
        (-1, 1), (0, 1), (1, 1)
    };
}

static List<(int dx, int dy)> GetLeftDiagonal() =>  new List<(int, int)> { (1, -1), (-1, 1) };
static List<(int dx, int dy)> GetRightDiagonal() => new List<(int, int)> { (-1, -1), (1, 1) };