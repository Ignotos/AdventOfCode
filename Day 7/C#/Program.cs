using Day_7;

internal class AoC7
{
    private static void Main()
    {
        var directorySizes = DirectoryTree.ParseDirectoryTree(@"..\..\..\..\input.txt").GetDirectorySizes();

        // #1
        Console.WriteLine(directorySizes.Where(size => size <= 100000).Sum());

        var availableSpace = 70000000;
        var targetUnusedSpace = 30000000;
        var usedSpace = directorySizes.First();
        var unusedSpace = availableSpace - usedSpace;
        var targetSize = targetUnusedSpace - unusedSpace;

        // #2
        Console.WriteLine(directorySizes.Where(size => size >= targetSize).Min());
    }
}