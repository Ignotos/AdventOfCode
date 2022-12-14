namespace Day_12
{
    public class Heightmap
    {
        private HeightmapPoint[,] Points { get; }

        public List<HeightmapPoint> StartPoints { get; }

        public int RowCount { get; }

        public int ColumnCount { get; }

        public HeightmapPoint StartPoint { get; }

        public Heightmap(HeightmapPoint[,] points, int rowCount, int colCount, List<HeightmapPoint> startingPoints)
        {
            this.Points = points;
            this.StartPoint = startingPoints.Find(point => point.IsStartPoint);
            this.RowCount = rowCount;
            this.ColumnCount = colCount;
            this.StartPoints = startingPoints;
        }

        public IEnumerable<HeightmapPoint> GetAdjacentPoints(HeightmapPoint point)
            => new List<HeightmapPoint>
            {
                this.GetPoint(point.Row, point.Col - 1),
                this.GetPoint(point.Row, point.Col + 1),
                this.GetPoint(point.Row - 1, point.Col),
                this.GetPoint(point.Row + 1, point.Col)
            }
            .Where(x => x != null && point.CanMakeMove(x));

        public static Heightmap ParseHeightmap(string filename)
        {
            var heightmap = File.ReadAllLines(filename).Select(x => x.ToCharArray()).ToArray();
            var rowCount = heightmap.Length;
            var colCount = heightmap[0].Length;
            var points = new HeightmapPoint[rowCount, colCount];
            var startingPoints = new List<HeightmapPoint>();

            for (var row = 0; row < rowCount; row++)
            {
                for (var col = 0; col < colCount; col++)
                {
                    var label = heightmap[row][col];
                    var point = new HeightmapPoint(label, row, col);
                    points[row, col] = point;

                    if (point.IsStartingPoint)
                    {
                        startingPoints.Add(point);
                    }
                }
            }

            return new Heightmap(points, rowCount, colCount, startingPoints);
        }

        private HeightmapPoint? GetPoint(int row, int col) => this.IsValidIndex(row, col) ? this.Points[row, col] : null;

        private bool IsValidIndex(int row, int col) => row >= 0 && col >= 0 && row < this.RowCount && col < this.ColumnCount;
    }
}
