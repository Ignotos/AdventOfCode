namespace Day_14
{
    public class Sand
    {
        public (int x, int y) Pos { get; set; }

        public bool IsStopped { get; set; }

        public Sand()
        {
            Pos = GetStartPosition();
        }

        public static (int x, int y) GetStartPosition() => (500, 0);

        public bool IsStartPosition => Pos == GetStartPosition();

        public (int x, int y) GetBelowPosition() => (Pos.x, Pos.y + 1);

        public (int x, int y) GetDiagonalLeftPosition() => (Pos.x - 1, Pos.y + 1);

        public (int x, int y) GetDiagonalRightPosition() => (Pos.x + 1, Pos.y + 1);

        public void MoveDown() => Pos = GetBelowPosition();

        public void MoveDiagonallyLeft() => Pos = GetDiagonalLeftPosition();

        public void MoveDiagonallyRight() => Pos = GetDiagonalRightPosition();
    }
}
