using System;

namespace Lottery_Simulation
{
    abstract class Ticket
    {
        public int[] numbers { get; set; }
        protected string name;
        protected Random random;
        public int cost { get; set; }

        public Ticket(int size, string name)
        {
            this.numbers = new int[size];
            this.name = name;
            random = new Random();
        }

        public abstract void DrawRandomNumbers();

        public void PrintNumbers()
        {
            Console.Write($"{name}: ");
            foreach (int num in numbers)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();
        }

        // Return 1 if equal, 0 if not
        // Order doesn't matter for first 5 numbers
        public bool Equal(int[] winningNums)
        {
            bool[] numUsed = new bool[winningNums.Length];

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                int containsIndex = Contains(winningNums, numbers[i]);
                if (containsIndex == -1 || numUsed[containsIndex])
                {
                    return false;
                }
                numUsed[containsIndex] = true;
            }

            // Check the last number since order matters for that one
            if (winningNums[winningNums.Length - 1] != numbers[numbers.Length - 1])
            {
                return false;
            }
            return true;
        }

        // Order doesn't matter for first 5 numbers
        public int GetMatchingNumbersCount(int[] winningNums)
        {
            int matchingNums = 0;
            bool[] numUsed = new bool[winningNums.Length];

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                int containsIndex = Contains(winningNums, numbers[i]);
                if (containsIndex != -1 && !numUsed[containsIndex])
                {
                    matchingNums++;
                    numUsed[containsIndex] = true;
                }
            }

            if (winningNums[winningNums.Length - 1] == numbers[numbers.Length - 1])
            {
                matchingNums++;
            }

            return matchingNums;
        }

        // Utility Function
        // Will check the first 5 numbers only
        // Returns the index
        private int Contains(int[] winningNums, int target)
        {
            for (int i = 0; i < winningNums.Length - 1; i++)
            {
                if (winningNums[i] == target)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
