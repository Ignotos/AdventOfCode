using Day_15;

internal class Day15
{
    private static void Main()
    {
        var sensorMap = SensorMap.ParseSensorMap(@"..\..\..\..\input.txt");
        Console.WriteLine(sensorMap.GetPositionsNotContainingBeaconInRow(2000000));

        sensorMap = SensorMap.ParseSensorMap(@"..\..\..\..\input.txt");
        Console.WriteLine(sensorMap.GetDistressBeaconTuningFrequency(4000000));
    }
}