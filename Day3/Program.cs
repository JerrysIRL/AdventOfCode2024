using System.Text.RegularExpressions;

string input = File.ReadAllText("E:\\AdventOfCode\\AdventOfCode2024\\Day3\\input.txt");


PartOne();
PartTwo();

//part 1
void PartOne()
{
    MatchCollection matchCollection = Regex.Matches(input, @"mul\(\d{1,3},\d{1,3}\)");
    int totalPartOne = 0;
    foreach (Match match in matchCollection)
    {
        var nums = Regex.Match(match.Value, @"\d{1,3},\d{1,3}");
        var numsArr = nums.Value.Split(",").Select(int.Parse);
        var result = numsArr.Aggregate((a, b) => a * b);

        totalPartOne += result;
    }

    Console.WriteLine($"Total: {totalPartOne}");
}




//part 2
void PartTwo()
{
    string pattern = @"(don't\(\)|do\(\))";

    var lines = Regex.Split(input, pattern);
    int totalPartTwo = 0;
    bool isDo = true;
    foreach (string line in lines)
    {
        if (line == "do()")
        {
            isDo = true;
        }
        else if (line == "don't()")
        {
            isDo = false;
        }

        if (isDo)
        {
            MatchCollection matches = Regex.Matches(line, @"mul\(\d{1,3},\d{1,3}\)");
            foreach (Match match in matches)
            {
                var nums = Regex.Match(match.Value, @"\d{1,3},\d{1,3}");
                var numsArr = nums.Value.Split(",").Select(int.Parse);
                var result = numsArr.Aggregate((a, b) => a * b);

                totalPartTwo += result;
            }
        }
    }

    Console.WriteLine($"Total: {totalPartTwo}");
}

