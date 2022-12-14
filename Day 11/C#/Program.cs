using Day_11;

internal class Day11
{
    private static void Main()
    {
        var filename = @"..\..\..\..\input.txt";
        var monkeys = Monkey.ParseMonkeys(filename);

        // #1
        Console.WriteLine(DoRounds(monkeys, 20, x => x / 3));

        // #2
        monkeys = Monkey.ParseMonkeys(filename);
        var commonDivisor = monkeys.Select(x => x.Divisor).Aggregate((acc, x) => acc * x);
        Console.WriteLine(DoRounds(monkeys, 10000, x => x % commonDivisor));
    }

    private static ulong DoRounds(Monkey[] monkeys, int rounds, Func<ulong, ulong> worryLevelTransform)
    {
        var round = 0;

        while (round < rounds)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Any())
                {
                    monkey.ItemsInspected++;
                    var worryLevel = worryLevelTransform(monkey.Operation(monkey.Items.Dequeue()));

                    if (worryLevel % monkey.Divisor == 0)
                    {
                        monkeys[monkey.RightMonkey].Items.Enqueue(worryLevel);
                    }
                    else
                    {
                        monkeys[monkey.LeftMonkey].Items.Enqueue(worryLevel);
                    }
                }
            }

            round++;
        }

        return monkeys.Select(x => x.ItemsInspected).OrderByDescending(x => x).Take(2).Aggregate((acc, x) => acc * x);
    }
}