namespace Day_18
{
    public class LavaDroplet
    {
        public int X { get; }

        public int Y { get; }

        public int Z { get; }

        public int ExposedSides { get; set; }

        public LavaDroplet(int[] coordinates)
        {
            this.X = coordinates[0];
            this.Y = coordinates[1];
            this.Z = coordinates[2];
            this.ExposedSides = 6;
        }

        public (int x, int y, int z) Coordinates => (X, Y, Z);

        public static IEnumerable<LavaDroplet> ParseLavaDroplets(string filename)
            => File.ReadAllLines(filename).Select(coordinate => new LavaDroplet(coordinate.Split(',').Select(int.Parse).ToArray()));

        public bool IsPositionAdjacent((int x, int y, int z) coordinates)
            => (Math.Abs(this.X - coordinates.x) == 1 && this.Y == coordinates.y && this.Z == coordinates.z)
            || (this.X == coordinates.x && Math.Abs(this.Y - coordinates.y) == 1 && this.Z == coordinates.z)
            || (this.X == coordinates.x && this.Y == coordinates.y && Math.Abs(this.Z - coordinates.z) == 1); 
        
    }
}
