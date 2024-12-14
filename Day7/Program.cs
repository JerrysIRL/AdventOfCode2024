// See https://aka.ms/new-console-template for more information

string[] testsStr = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2024\Day7\input.txt");

List<Operation> operations = new List<Operation> { Operation.Add, Operation.Multiply };
int sum = 0;
HashSet<long> results = new HashSet<long>();

for (int i = 0; i < testsStr.Length; i++)
{
    var test = testsStr[i].Split(':');
    var testResult = long.Parse(test[0]);
    List<long> testValues = test[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

    CalculateAllOptions(testValues, testResult, 1, testValues[0]);
}

Console.WriteLine(results.Sum());

void CalculateAllOptions(List<long> numbers, long testResult, int index, long currentResult)
{
    if (index == numbers.Count)
    {
        if (currentResult == testResult)
        {
            results.Add(currentResult);
        }

        return;
    }

    foreach (var op in operations)
    {
        long nextResult = currentResult;

        if (op == Operation.Add)
        {
            nextResult += numbers[index];
        }
        else if (op == Operation.Multiply)
        {
            nextResult *= numbers[index];
        }



        CalculateAllOptions(numbers, testResult, index + 1, nextResult);
    }
}

enum Operation
{
    Add,
    Multiply,
    ConCat
}