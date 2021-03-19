namespace Lottery_Simulation
{
    class Powerball : Ticket
    {
        private const int TOTAL_NUMS = 6;

        public Powerball() : base(TOTAL_NUMS, "Powerball")
        {
            cost = 2;
        }

        public override void DrawRandomNumbers()
        {
            // First 5 is generated from 1-69
            // Last number "power" is generated from 1-26
            for (int i = 0; i < TOTAL_NUMS; i++)
            {
                numbers[i] = random.Next(1, 69);
            }

            numbers[TOTAL_NUMS - 1] = random.Next(1, 26);
        }
    }
}
