namespace Day_11
{
    public class Monkey
    {
        public Queue<ulong> Items { get; set; } = new Queue<ulong>();

        public Func<ulong, ulong> Operation { get; set; }

        public ulong Divisor { get; set; }

        public int RightMonkey { get; set; }

        public int LeftMonkey { get; set; }

        public ulong ItemsInspected { get; set; }

        public static Monkey[] ParseMonkeys(string filename)
        {
            var fileChunks = File.ReadAllLines(filename).Chunk(7);
            var monkeys = new List<Monkey>();

            foreach (var chunk in fileChunks)
            {
                monkeys.Add(ParseMonkey(chunk));
            }

            return monkeys.ToArray();
        }

        private static Monkey ParseMonkey(string[] input)
        {
            var monkey = new Monkey();

            foreach (var line in input)
            {
                if (line.Contains("Starting items"))
                {
                    line.Split(':').Last().Split(',').Select(ulong.Parse).ToList().ForEach(monkey.Items.Enqueue);
                }
                if (line.Contains("Operation"))
                {
                    monkey.Operation = ParseOperation(line);
                }
                if (line.Contains("Test"))
                {
                    monkey.Divisor = ulong.Parse(line.Split(' ').Last());
                }
                if (line.Contains("If true"))
                {
                    monkey.RightMonkey = int.Parse(line.Split(' ').Last());
                }
                if (line.Contains("If false"))
                {
                    monkey.LeftMonkey = int.Parse(line.Split(' ').Last());
                }
            }

            return monkey;
        }

        private static Func<ulong, ulong> ParseOperation(string input)
        {
            var operationParts = input.Split(':').Last().Split(' ').Skip(4).ToArray();

            if (operationParts[0] == "*")
            {
                if (operationParts[1] == "old")
                {
                    return x => x * x;
                }
                else
                {
                    var value = ulong.Parse(operationParts[1]);
                    return x => x * value;
                }
            }
            else
            {
                var value = ulong.Parse(operationParts[1]);
                return x => x + value;
            }
        }
    }
}
