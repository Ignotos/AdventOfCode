namespace Day_12
{
    public class HeightmapPoint
    {
        public char Label { get; }

        public int Value { get; }

        public int Row { get; }

        public int Col { get; }

        public bool IsEndPoint => this.Label == 'E';

        public bool IsStartPoint => this.Label == 'S';

        public bool IsStartingPoint => this.Label == 'a' || this.IsStartPoint;

        public HeightmapPoint(char label, int row, int col)
        {
            this.Label = label;
            this.Row = row;
            this.Col = col;
            this.Value = label switch
            {
                'S' => (int)'a',
                'E' => (int)'z',
                 _  => (int)label,
            };
        }

        public bool CanMakeMove(HeightmapPoint node) => node.Value <= this.Value + 1;
    }
}
