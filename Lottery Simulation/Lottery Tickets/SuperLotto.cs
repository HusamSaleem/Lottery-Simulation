namespace Lottery_Simulation
{
    class SuperLotto : Ticket
    {
        private const int TOTAL_NUMS = 6;

        public SuperLotto() : base(TOTAL_NUMS, "SuperLotto")
        {
            cost = 1;
        }

        public override void DrawRandomNumbers()
        {
            // First 5 is generated from 1-47
            // Last number "super" is generated from 1-27
            for (int i = 0; i < TOTAL_NUMS; i++)
            {
                numbers[i] = random.Next(1, 47);
            }

            numbers[TOTAL_NUMS - 1] = random.Next(1, 27);
        }
    }
}
