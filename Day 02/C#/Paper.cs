namespace Day_2
{
    class Paper : Play
    {
        public Paper() : base(2) {}

        public override Outcome GetOutcome(Play opponentPlay)
        {
            return opponentPlay switch
            {
                Rock => Outcome.Win,
                Paper => Outcome.Draw,
                Scissors => Outcome.Lose
            };
        }
    }
}
