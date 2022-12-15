namespace Day_14
{
    public class Rock
    {
        public List<(int x, int y)> RockPositions { get; set; } = new List<(int, int)>();

        public static Rock ParseRock(string input)
        {
            var rockCoords = input.Split(" -> ").Select(x => x.Split(',').Select(int.Parse).ToArray()).ToArray();
            var rock = new Rock();

            for(var i = 0; i < rockCoords.Length - 1; i++)
            {
                var (x, y) = (rockCoords[i][0], rockCoords[i][1]);
                var endPos = (rockCoords[i + 1][0], rockCoords[i + 1][1]);
                var rockPos = (x, y);
                rock.RockPositions.Add(rockPos);
                var (stepX, stepY, steps) = GetSteps(rockPos, endPos);

                for (var step = 0; step < steps; step++)
                {
                    rockPos = (rockPos.x + stepX, rockPos.y + stepY);
                    rock.RockPositions.Add(rockPos);
                }
            }

            return rock;
        }

        private static (int, int, int) GetSteps((int x, int y) start, (int x, int y) end)
        {
            var (dirX, dirY) = (end.x - start.x, end.y - start.y);

            return dirX == 0 
                ? (dirX, dirY > 0 ? 1 : -1, Math.Abs(dirY)) 
                : (dirX > 0 ? 1 : -1, dirY, Math.Abs(dirX));
        }
    }
}
