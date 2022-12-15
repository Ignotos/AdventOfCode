namespace Day_14
{
    public class Cave
    {
        public List<Rock> Rocks { get; }

        public List<(int x, int y)> SandPositions { get; } = new List<(int, int)>();

        private int AbyssLevel { get; }

        private int FloorLevel { get; }

        public Cave(List<Rock> rocks)
        {
            this.Rocks = rocks;
            this.AbyssLevel = rocks.Max(rock => rock.RockPositions.Max(pos => pos.y));
            this.FloorLevel = this.AbyssLevel + 2;
        }

        public int SimulateSandFallingToFloor()
        {
            var sandCount = 0;
            var hasReachedCapacity = false;

            while (!hasReachedCapacity)
            {
                var sand = new Sand();

                while (!sand.IsStopped)
                {
                    if (!IsSandBlockedWithFloor(sand.GetBelowPosition()))
                    {
                        sand.MoveDown();
                    }
                    else if (!IsSandBlockedWithFloor(sand.GetDiagonalLeftPosition()))
                    {
                        sand.MoveDiagonallyLeft();
                    }
                    else if (!IsSandBlockedWithFloor(sand.GetDiagonalRightPosition()))
                    {
                        sand.MoveDiagonallyRight();
                    }
                    else if (sand.IsStartPosition)
                    {
                        sandCount++;
                        sand.IsStopped = true;
                        hasReachedCapacity = true;
                    }
                    else
                    {
                        sandCount++;
                        sand.IsStopped = true;
                        this.SandPositions.Add(sand.Pos);
                    }                    
                }
            }

            return sandCount;
        }

        public int SimulateSandFallingToAbyss()
        {
            var sandCount = 0;
            var hasReachedAbyss = false;

            while (!hasReachedAbyss)
            {
                var sand = new Sand();

                while (!sand.IsStopped)
                {
                    if (sand.Pos.y == this.AbyssLevel)
                    {
                        hasReachedAbyss = true;
                        sand.IsStopped = true;
                    }
                    else if (!IsSandBlocked(sand.GetBelowPosition()))
                    {
                        sand.MoveDown();
                    }
                    else if (!IsSandBlocked(sand.GetDiagonalLeftPosition()))
                    {
                        sand.MoveDiagonallyLeft();
                    }
                    else if (!IsSandBlocked(sand.GetDiagonalRightPosition()))
                    {
                        sand.MoveDiagonallyRight();
                    }
                    else
                    {
                        sandCount++;
                        sand.IsStopped = true;
                        this.SandPositions.Add(sand.Pos);
                    }
                }
            }

            return sandCount;
        }

        private bool IsSandBlocked((int x, int y) pos)
            => this.Rocks.Any(rock => rock.RockPositions.Contains((pos.x, pos.y)))
                || this.SandPositions.Contains((pos.x, pos.y));

        private bool IsSandBlockedWithFloor((int x, int y) pos)
            => pos.y == this.FloorLevel || this.IsSandBlocked(pos);
    }
}
