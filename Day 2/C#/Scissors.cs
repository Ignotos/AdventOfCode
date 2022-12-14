namespace Day_2
{
    class Scissors : Play
    {
        public Scissors() : base(3) {}

        public override Outcome GetOutcome(Play opponentPlay)
        {
            return opponentPlay switch
            {
                Rock     => Outcome.Lose,
                Paper    => Outcome.Win,
                Scissors => Outcome.Draw
            };
        }
    }

}
