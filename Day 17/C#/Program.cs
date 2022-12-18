using Day_17;

internal class AoC17
{
    private static void Main()
    {
        var chamber = new Chamber(@"..\..\..\..\input.txt");
        Console.WriteLine(chamber.SimulateRocksFalling(2022));
    }
}