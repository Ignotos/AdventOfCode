using Day_14;

internal class AoC14
{
    private static void Main()
    {
        var filename = @"..\..\..\..\input.txt";
        var rocks = File.ReadAllLines(filename).Select(Rock.ParseRock).ToList();
        var cave = new Cave(rocks);

        // #1
        Console.WriteLine(cave.SimulateSandFallingToAbyss());

        // #2
        cave = new Cave(rocks);
        Console.WriteLine(cave.SimulateSandFallingToFloor());
    }
}
