using Day_2;

internal class Day2
{
    private static void Main()
    {
        var lines = File.ReadAllLines(@"..\..\..\..\input.txt");
        int points1 = 0, points2 = 0;

        foreach (string line in lines)
        {
            var plays = Play.ParsePlays(line);
            var opponentPlay = plays.First();
            var playerPlay = plays.Last();

            points1 += playerPlay.Value + (int) playerPlay.GetOutcome(opponentPlay);

            var desiredOutcome = Play.GetDesiredOutcome(playerPlay);
            points2 += Play.GetDesiredPlay(opponentPlay, desiredOutcome).Value + (int) desiredOutcome;
        }

        // #1
        Console.WriteLine($"{points1}");

        // #2
        Console.WriteLine($"{points2}");
    }
}