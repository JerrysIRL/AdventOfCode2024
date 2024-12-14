string[] testsStr = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2024\Day7\input.txt");

List<Operation> operations = new List<Operation> { Operation.Add, Operation.Multiply, Operation.ConCat };
ulong totalCalibrationResult = 0;

//146111650210682

for (int i = 0; i < testsStr.Length; i++)
{
    var test = testsStr[i].Split(':');
    ulong testResult = ulong.Parse(test[0].Trim());
    List<ulong> testValues = test[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToList();

    if (CalculateAllOptions(testValues, testResult, 1, testValues[0]))
    {
        totalCalibrationResult += testResult;
    }
}

Console.WriteLine($"Total Result: {totalCalibrationResult}");


bool CalculateAllOptions(List<ulong> numbers, ulong testResult, int index, ulong currentResult)
{
    if (index == numbers.Count)
    {
        return currentResult == testResult;
    }

    ulong nextNumber = numbers[index];

    foreach (var op in operations)
    {
        try
        {
            ulong nextResult = currentResult;

            switch (op)
            {
                case Operation.Add:
                    nextResult += nextNumber;
                    break;
                case Operation.Multiply:
                    nextResult *= nextNumber;
                    break;
                case Operation.ConCat:
                    nextResult = ulong.Parse(string.Concat(nextResult.ToString(), nextNumber));
                    break;
            }

            if (CalculateAllOptions(numbers, testResult, index + 1, nextResult))
            {
                return true;
            }
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"OverflowException: {currentResult} {op} {nextNumber} - {ex.Message}");
        }
    }

    return false;
}

enum Operation
{
    Add,
    Multiply,
    ConCat
}