// See https://aka.ms/new-console-template for more information

var input = File.ReadAllText("E:\\AdventOfCode\\AdventOfCode2024\\Day5\\input.txt")
    .Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

string[] rulesStr = input[0].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
string[] manualsStr = input[1].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

List<Tuple<int, int>> rules = rulesStr.Select(x =>
{
    var parts = x.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
    return new Tuple<int, int>(int.Parse(parts[0]), int.Parse(parts[1]));
}).ToList();

List<List<int>> manuals = manualsStr.Select(x => x.Split(",").Select(int.Parse).ToList()).ToList();

int sum = 0;
int sumPartTwo = 0;

foreach (var page in manuals)
{
    var tempPage = page.ToList();
    bool isValid = true;
    var relevantRules = rules.FindAll(x => page.Contains(x.Item1) && page.Contains(x.Item2));
    if (relevantRules.Count == 0)
    {
        sum += page[page.Count / 2];
        continue;
    }
    foreach (var num in page)
    {
        var numRules = relevantRules.Where(x => num == x.Item1);
        foreach (var rule in numRules)
        {
            var index = tempPage.IndexOf(num);
            var index2 = tempPage.IndexOf(rule.Item2);
            if (index > index2)
            {
                int itemToMove = tempPage[index];
                tempPage.RemoveAt(index);
                tempPage.Insert(index2, itemToMove);

                isValid = false;
            }
        }
    }

    if(isValid)
    {
        sum += page[page.Count / 2];
    }
    else
    {
        sumPartTwo += tempPage[page.Count / 2];
    }
}



Console.WriteLine(sum);
Console.WriteLine(sumPartTwo);