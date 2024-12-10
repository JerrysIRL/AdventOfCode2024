using System.Text.RegularExpressions;


string input = File.ReadAllText("E:\\AdventOfCode\\AdventOfCode2024\\Day3\\input.txt");
MatchCollection matches =  Regex.Matches(input,"mul\\(\\d{1,3},\\d{1,3}\\)");

int total = 0;
foreach (Match match in matches)
{
    var nums = Regex.Match(match.Value, "\\d{1,3},\\d{1,3}");
    var numsArr = nums.Value.Split(",").Select(int.Parse);
    var result = numsArr.Aggregate((a,b) => a * b);
    total += result;
}

Console.WriteLine(total);
