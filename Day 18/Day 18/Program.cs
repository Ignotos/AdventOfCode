using Day_18;

internal class AoC17
{
    private static void Main()
    {
        var lavaDroplets = LavaDroplet.ParseLavaDroplets(@"..\..\..\..\input.txt");
        var exposedSides = new Dictionary<(int x, int y, int z), int>();

        foreach (var lavaDroplet in lavaDroplets)
        {
            exposedSides.Add(lavaDroplet.Coordinates, lavaDroplet.ExposedSides);
            var adjacentLavaDroplets = exposedSides.Keys.Where(lavaDroplet.IsPositionAdjacent);

            foreach (var adjacentLavaDroplet in adjacentLavaDroplets)
            {
                exposedSides[adjacentLavaDroplet]--;
                exposedSides[lavaDroplet.Coordinates]--;
            }
        }

        // #1
        Console.WriteLine(exposedSides.Values.Sum());
    }
}