string[] lines = File.ReadAllLines(@"..\..\..\..\input.txt");
var elfCalories = new List<int>();
var currentTotal = 0;

foreach (string line in lines)
{
    if (!string.IsNullOrEmpty(line))
    {
        currentTotal += int.Parse(line);
    }
    else
    {
        elfCalories.Add(currentTotal);
        currentTotal = 0;
    }
}

// #1
Console.WriteLine(elfCalories.Max());

// #2
Console.WriteLine(elfCalories.OrderByDescending(x => x).Take(3).Sum());