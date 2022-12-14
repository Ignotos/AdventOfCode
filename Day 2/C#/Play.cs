namespace Day_2
{
    abstract class Play
    {
        public int Value;

        public Play(int value)
        { 
            this.Value = value; 
        }

        public enum Outcome
        {
            Win = 6,
            Lose = 0,
            Draw = 3
        }

        public static IEnumerable<Play> ParsePlays(string plays) => plays.Split(' ').Select(char.Parse).Select(ParsePlay);

        public static Play GetDesiredPlay(Play opponentPlay, Outcome desiredOutcome)
        {
            return (opponentPlay, desiredOutcome) switch
            {
                (Rock, Outcome.Win) => new Paper(),
                (Rock, Outcome.Lose) => new Scissors(),
                (Rock, Outcome.Draw) => new Rock(),
                (Paper, Outcome.Win) => new Scissors(),
                (Paper, Outcome.Lose) => new Rock(),
                (Paper, Outcome.Draw) => new Paper(),
                (Scissors, Outcome.Win) => new Rock(),
                (Scissors, Outcome.Lose) => new Paper(),
                (Scissors, Outcome.Draw) => new Scissors()
            };
        }

        public static Outcome GetDesiredOutcome(Play outcome)
        {
            return outcome switch
            {
                Rock     => Outcome.Lose,
                Paper    => Outcome.Draw,
                Scissors => Outcome.Win
            };
        }

        public abstract Outcome GetOutcome(Play opponentPlay);

        private static Play ParsePlay(char play)
        {
            return play switch
            {
                'X' or 'A' => new Rock(),
                'Y' or 'B' => new Paper(),
                'Z' or 'C' => new Scissors()
            };
        }
    }
}
