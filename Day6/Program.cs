// See https://aka.ms/new-console-template for more information

string[] input = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2024\Day6\input.txt");

var startY = Array.FindIndex(input, row => row.Contains('^'));
var startX = input[startY].IndexOf('^');

Tuple<int, int> startLocation = new Tuple<int, int>(startX, startY);

PartOne();
PartTwo();

void PartOne()
{
    HashSet<Tuple<int, int>> visited = new();
    visited.Add(new Tuple<int, int>(startLocation.Item1, startLocation.Item2));

    var currentDirection = Direction.Up;
    var currentLocation = startLocation;

    while (true)
    {
        var nextLocation = GetNextLocation(currentLocation, currentDirection);

        if (nextLocation.Item1 < 0 || nextLocation.Item2 < 0 || nextLocation.Item1 >= input[0].Length || nextLocation.Item2 >= input.Length)
            break;

        if (input[nextLocation.Item2][nextLocation.Item1] == '#')
        {
            var newDirection = TurnRight(currentDirection);
            currentDirection = newDirection;
        }
        else
        {
            currentLocation = nextLocation;
            visited.Add(currentLocation);
        }
    }

    Console.WriteLine(visited.Count);
}


void PartTwo()
{
    int validObstructionCount = 0;
    var currentDirection = Direction.Up;

    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[0].Length; x++)
        {
            if (input[y][x] == '.' && !(x == startLocation.Item1 && y == startLocation.Item2))
            {
                if (CausesLoop(input, startLocation, currentDirection, new Tuple<int, int>(x, y)))
                {
                    validObstructionCount++;
                }
            }
        }
    }
    Console.WriteLine(validObstructionCount);
}

bool CausesLoop(string[] input, Tuple<int, int> startLocation, Direction startDirection, Tuple<int, int> obstruction)
{
    HashSet<(int X, int Y, Direction Dir)> visitedStates = new();
    var currentLocation = startLocation;
    var currentDirection = startDirection;

    while (true)
    {
        var nextLocation = GetNextLocation(currentLocation, currentDirection);

        if (nextLocation.Item1 < 0 || nextLocation.Item2 < 0 || nextLocation.Item1 >= input[0].Length || nextLocation.Item2 >= input.Length)
            return false;

        if (nextLocation.Equals(obstruction) || input[nextLocation.Item2][nextLocation.Item1] == '#')
        {
            currentDirection = TurnRight(currentDirection);
        }
        else
        {
            currentLocation = nextLocation;

            var state = (currentLocation.Item1, currentLocation.Item2, currentDirection);
            if (!visitedStates.Add(state))
            {
                return true;
            }
        }
    }
}

Tuple<int, int> GetNextLocation(Tuple<int, int> curLocation, Direction direction)
{
    switch (direction)
    {
        case Direction.Up:
            return new Tuple<int, int>(curLocation.Item1, curLocation.Item2 - 1);
        case Direction.Right:
            return new Tuple<int, int>(curLocation.Item1 + 1, curLocation.Item2);
        case Direction.Down:
            return new Tuple<int, int>(curLocation.Item1, curLocation.Item2 + 1);
        case Direction.Left:
            return new Tuple<int, int>(curLocation.Item1 - 1, curLocation.Item2);
        default:
            throw new Exception("Invalid direction");
    }
}

Direction TurnRight(Direction direction)
{
    return direction switch
    {
        Direction.Up => Direction.Right,
        Direction.Right => Direction.Down,
        Direction.Down => Direction.Left,
        Direction.Left => Direction.Up,
        _ => throw new Exception("Invalid direction"),
    };
}

enum Direction
{
    Up,
    Right,
    Down,
    Left
}