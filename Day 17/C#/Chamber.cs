namespace Day_17
{
    public class Chamber
    {
        public HashSet<(int x, long y)> OccupiedPositions { get; } = new HashSet<(int x, long y)>();

        public long HighestRockPosition
            => this.OccupiedPositions.Any() ? this.OccupiedPositions.Max(position => position.y + 1) : 0;

        public Rock? LastRock { get; set; } = null;

        public string JetPatterns;

        public int JetPatternIndex { get; set; }

        public int JetPatternCount { get; set; }

        public Chamber(string filename)
        {
            this.JetPatterns = File.ReadAllText(filename);
            this.JetPatternCount = this.JetPatterns.Length;
        }

        public char GetNextJetPattern()
        {
            if (this.JetPatternIndex == this.JetPatternCount)
            {
                this.JetPatternIndex = 0;
            }

            return this.JetPatterns[this.JetPatternIndex++];
        }

        public Rock GetNextRock()
        {
            var nextBottomLeftPosition = (2, this.HighestRockPosition + 3);

            return this.LastRock switch
            {
                HorizontalLineRock => new PlusRock(nextBottomLeftPosition),
                PlusRock           => new BackwardsLRock(nextBottomLeftPosition),
                BackwardsLRock     => new VerticalLineRock(nextBottomLeftPosition),
                VerticalLineRock   => new SquareRock(nextBottomLeftPosition),
                _                  => new HorizontalLineRock(nextBottomLeftPosition)
            };
        }

        public long SimulateRocksFalling(int rocksCount)
        {
            for (var i = 1; i <= rocksCount; i++)
            {
                this.SimulateRockFalling();
            }

            return this.HighestRockPosition;
        }

        public void SimulateRockFalling()
        {
            var rock = this.GetNextRock();

            while (!rock.IsStopped) 
            {
                var move = this.GetNextJetPattern() == '>' ? 1 : -1;

                if (this.CanMakeMoveAlongX(rock, move))
                {
                    rock.MoveAlongX(move);
                }

                if (this.CanMoveDown(rock))
                {
                    rock.MoveDown();
                }
                else
                {
                    rock.IsStopped = true;
                }
            }

            this.OccupiedPositions.UnionWith(rock.Positions);
            this.LastRock = rock;
        }

        public bool CanMoveDown(Rock rock)
            => !rock.GetMovePositionsBelow().Any(IsPositionOccupied);

        public bool CanMakeMoveAlongX(Rock rock, int move)
            => !rock.GetMovePositionsAlongX(move).Any(IsPositionOccupied);

        public bool IsPositionOccupied((int x, long y) position) 
            => position.y < 0 || position.x < 0 || position.x > 6 || this.OccupiedPositions.Contains(position);
    }
}
