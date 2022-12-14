using Day_12;

internal class AoC12
{
    private static void Main()
    {
        var heightmap = Heightmap.ParseHeightmap(@"..\..\..\..\input.txt");

        // #1
        Console.WriteLine(GetShortestPathSteps(heightmap, heightmap.StartPoint));

        // #2
        Console.WriteLine(heightmap.StartPoints.Select(x => GetShortestPathSteps(heightmap, x)).Where(x => x != -1).Min());
    }

    private static int GetShortestPathSteps(Heightmap heightmap, HeightmapPoint startPoint)
    {
        var queue = new Queue<(HeightmapPoint Point, int Steps)>();
        queue.Enqueue((startPoint, 0));

        var isVisited = new bool[heightmap.RowCount, heightmap.ColumnCount];
        isVisited[startPoint.Row, startPoint.Col] = true;

        while (queue.Any())
        {
            var point = queue.Dequeue();

            if (point.Point.IsEndPoint)
            {
                return point.Steps;
            }

            heightmap
                .GetAdjacentPoints(point.Point)
                .Where(adjacentPoint => !isVisited[adjacentPoint.Row, adjacentPoint.Col])
                .ToList()
                .ForEach(adjacentPoint =>
                {
                    if (!isVisited[adjacentPoint.Row, adjacentPoint.Col])
                    {
                        isVisited[adjacentPoint.Row, adjacentPoint.Col] = true;
                        queue.Enqueue((adjacentPoint, point.Steps + 1));
                    }
                });
        }

        return -1;
    }
}
