namespace Lottery_Simulation
{
    class MegaMillions : Ticket
    {
        private const int TOTAL_NUMS = 6;

        public MegaMillions() : base(TOTAL_NUMS, "Mega Millions")
        {
            cost = 2;
        }

        public override void DrawRandomNumbers()
        {
            // First 5 are randomly generated from 1-70
            // The "mega" is a number from 1-25
            for (int i = 0; i < 5; i++)
            {
                numbers[i] = random.Next(1, 70);
            }

            numbers[TOTAL_NUMS - 1] = random.Next(1, 25);
        }
    }
}
