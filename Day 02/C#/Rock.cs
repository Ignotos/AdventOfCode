namespace Day_2
{
    class Rock : Play
    {
        public Rock(): base(1) {}

        public override Outcome GetOutcome(Play opponentPlay)
        {
            return opponentPlay switch
            {
                Rock => Outcome.Draw,
                Paper => Outcome.Lose,
                Scissors => Outcome.Win
            };
        }
    }
}
