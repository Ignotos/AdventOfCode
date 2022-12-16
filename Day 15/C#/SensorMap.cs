using System;

namespace Day_15
{
    public class SensorMap
    {
        public List<(long x, long y)> SensorPositions { get; set; } = new List<(long, long)>();

        public List<(long x, long y)> BeaconPositions { get; set; } = new List<(long, long)>();

        public List<long> ManhattanDistances { get; set; } = new List<long>();

        public static SensorMap ParseSensorMap(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var sensorMap = new SensorMap();

            foreach (var line in lines)
            {
                var positions = line.Split(':').Select(x => x.Split(',').Select(x => x.Split('=').Last()).Select(long.Parse).ToArray()).ToArray();
                var sensorPosition = (positions[0][0], positions[0][1]);
                var beaconPosition = (positions[1][0], positions[1][1]);
                sensorMap.SensorPositions.Add(sensorPosition);
                sensorMap.BeaconPositions.Add(beaconPosition);
                sensorMap.ManhattanDistances.Add(GetManhattanDistance(sensorPosition, beaconPosition));
            }

            return sensorMap;
        }

        public ulong GetDistressBeaconTuningFrequency(ulong distressBeaconSearchArea)
        {
            var sensorCoordinates = this.SensorPositions.ToArray();
            var manhattanDistances = this.ManhattanDistances.ToArray();

            for (ulong row = 0; row < distressBeaconSearchArea; row++)
            {
                var sensorRangesInTargetRow = new List<(long left, long right)>();

                for (var sensor = 0; sensor < sensorCoordinates.Length; sensor++)
                {
                    var sensorPosition = sensorCoordinates[sensor];
                    var sensorPositionAtTargetRow = (sensorPosition.x, (long)row);
                    var manhattanDistanceToTargetRow = GetManhattanDistance(sensorPosition, sensorPositionAtTargetRow);
                    var sensorRangeAtTargetRow = manhattanDistances[sensor] - manhattanDistanceToTargetRow;

                    if (sensorRangeAtTargetRow > 0)
                    {
                        var leftRange = sensorPosition.x - sensorRangeAtTargetRow;
                        var rightRange = sensorPosition.x + sensorRangeAtTargetRow;

                        sensorRangesInTargetRow.Add((leftRange < 0 ? 0 : leftRange, rightRange > (long)distressBeaconSearchArea ? (long)distressBeaconSearchArea : rightRange));
                    }
                }

                var orderedSensorRanges = sensorRangesInTargetRow.OrderBy(x => x.left).ToArray();
                var sensorRangeMin = sensorRangesInTargetRow.Min(x => x.left);
                var sensorRangeMax = sensorRangesInTargetRow.Max(x => x.right);

                if (sensorRangeMin != 0)
                {
                    return row;
                }

                if (sensorRangeMax != (long)distressBeaconSearchArea)
                {
                    return (distressBeaconSearchArea * distressBeaconSearchArea) + row;
                }

                var rangeCovered = orderedSensorRanges[0].right;

                for (ulong i = 0; i < (ulong)(orderedSensorRanges.Length - 1); i++)
                {
                    rangeCovered = Math.Max(rangeCovered, orderedSensorRanges[i].right);

                    if (orderedSensorRanges[i].right < rangeCovered)
                    {
                        continue;             
                    }
                    
                    if (rangeCovered < orderedSensorRanges[i + 1].left && rangeCovered + 2 == orderedSensorRanges[i + 1].left)
                    {
                        var x = (ulong)(orderedSensorRanges[i].right + 1);
                        return (x * distressBeaconSearchArea) + row;
                    }
                }
            }

            throw new Exception();
        }

        public long GetPositionsNotContainingBeaconInRow(long targetRow)
        {
            var sensorCoordinates = this.SensorPositions.ToArray();
            var manhattanDistances = this.ManhattanDistances.ToArray();
            var beaconPositionsInTargetRow = this.BeaconPositions.Where(position => position.y == targetRow).Select(position => position.x).Distinct();
            var sensorRangesAtTargetRow = new HashSet<long>();

            for (var i = 0; i < sensorCoordinates.Length; i++)
            {
                var sensorPosition = sensorCoordinates[i];
                var sensorPositionAtTargetRow = (sensorPosition.x, targetRow);
                var manhattanDistanceToTargetRow = GetManhattanDistance(sensorPosition, sensorPositionAtTargetRow);
                var sensorRangeAtTargetRow = manhattanDistances[i] - manhattanDistanceToTargetRow;

                if (sensorRangeAtTargetRow > 0)
                {
                    var leftRange = sensorPosition.x - sensorRangeAtTargetRow;
                    var rightRange = sensorPosition.x + sensorRangeAtTargetRow;

                    for (long p = leftRange; p <= rightRange; p++)
                    {
                        if (!beaconPositionsInTargetRow.Where(position => position == p).Any())
                        {
                            sensorRangesAtTargetRow.Add(p);
                        }
                    }
                }
            }

            return sensorRangesAtTargetRow.Count;
        }

        private static long GetManhattanDistance((long x, long y) p1, (long x, long y) p2)
            => Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);
    }
}
