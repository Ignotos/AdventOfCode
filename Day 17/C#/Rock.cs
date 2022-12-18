namespace Day_17
{
    public abstract class Rock
    {
        public abstract int Length { get; }

        public abstract int Height { get; }

        public (int x, long y) BottomLeftPosition { get; }

        public HashSet<(int x, long y)> Positions { get; set; } = new HashSet<(int, long)>();

        public bool IsStopped { get; set; }

        public IEnumerable<(int x, long y)> GetMovePositionsAlongX(int move) => this.Positions.Select(position => (position.x + move, position.y));

        public IEnumerable<(int x, long y)> GetMovePositionsBelow() => this.Positions.Select(position => (position.x, position.y - 1));

        public void MoveAlongX(int move) => this.Positions = GetMovePositionsAlongX(move).ToHashSet();

        public void MoveDown() => this.Positions = GetMovePositionsBelow().ToHashSet();

        public override string ToString() => $"{this.GetType().Name}";

        public void PrintPositions() => this.Positions.ToList().ForEach(pos => Console.WriteLine($"({pos.x}, {pos.y})"));

        public Rock((int, long) bottomLeftPosition)
        {
            this.BottomLeftPosition = bottomLeftPosition;
        }
    }

    public class HorizontalLineRock : Rock
    {
        public override int Length => 4;

        public override int Height => 1;

        public HorizontalLineRock((int x, long y) bottomLeftPosition) : base(bottomLeftPosition)
        {
            for (var x = 0; x < this.Length; x++)
            {
                this.Positions.Add((bottomLeftPosition.x + x, bottomLeftPosition.y));
            }
        }
    }

    public class VerticalLineRock : Rock
    {
        public override int Length => 1;

        public override int Height => 4;

        public VerticalLineRock((int x, long y) bottomLeftPosition) : base(bottomLeftPosition)
        {
            for (var y = 0; y < this.Height; y++)
            {
                this.Positions.Add((bottomLeftPosition.x, bottomLeftPosition.y + y));
            }
        }
    }

    public class PlusRock : Rock
    {
        public override int Length => 3;

        public override int Height => 3;

        public PlusRock((int x, long y) bottomLeftPosition) : base(bottomLeftPosition)
        {
            for (var x = 0; x < this.Length; x++)
            {
                this.Positions.Add((bottomLeftPosition.x + x, bottomLeftPosition.y + 1));
            }

            for (var y = 0; y < this.Height; y++)
            {
                this.Positions.Add((bottomLeftPosition.x + 1, bottomLeftPosition.y + y));
            }
        }
    }

    public class SquareRock : Rock
    {
        public override int Length => 2;

        public override int Height => 2;

        public SquareRock((int x, long y) bottomLeftPosition) : base(bottomLeftPosition)
        {
            for (var i = 0; i < this.Length; i++)
            {
                this.Positions.Add((bottomLeftPosition.x + i, bottomLeftPosition.y));
                this.Positions.Add((bottomLeftPosition.x + i, bottomLeftPosition.y + 1));
            }
        }
    }

    public class BackwardsLRock : Rock
    {
        public override int Length => 3;

        public override int Height => 3;

        public BackwardsLRock((int x, long y) bottomLeftPosition) : base(bottomLeftPosition)
        {
            for (var x = 0; x < this.Length; x++)
            {
                this.Positions.Add((bottomLeftPosition.x + x, bottomLeftPosition.y));
            }

            for (var y = 0; y < this.Height; y++)
            {
                this.Positions.Add((bottomLeftPosition.x + 2, bottomLeftPosition.y + y));
            }
        }
    }
}
