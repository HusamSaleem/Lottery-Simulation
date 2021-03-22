using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Lottery_Simulation
{
    class Simulation
    {
        private Excel excel;
        public string currentLotteryName { get; set; }
        private string excelDirPath { get; set; }

        // The winning numbers.. You can change these
        // *Make sure to follow the rules of each lottery ticket*
        // For example, The mega millions has 5 random numbers 1-70 and 1 random "mega" from 1-25
        private static int[] WINNING_POWER = { 5, 11, 51, 56, 61, 2 };
        private static int[] WINNING_MEGA = { 10, 40, 46, 52, 69, 8 };
        private static int[] WINNING_SUPER = { 3, 7, 8, 13, 21, 19 };

        BackgroundWorker worker;

        public Simulation(string excelDirPath)
        {
            this.excelDirPath = excelDirPath;
            excel = new Excel();
        }
        public void Start()
        {
            NewSimulationMenu();
        }

        // Tries to see how many draws it takes to get the winning numbers
        public void SimulateMega(int[] winningMega, int numOfDraws, bool printInfo)
        {
            int[] excelRows = new int[6];
            if (numOfDraws != -1)
            {
                excel.UseExcel(true);
                excel.SetPath(excelDirPath);
                excel.CreateNewExcelFile("Mega Millions");

                excel.WriteToExcel(2, 8, ArrayToString(winningMega)); // Save the winning numbers at the last Column
                for (int i = 0; i < excelRows.Length; i++)
                {
                    excelRows[i] = 2;
                }
            }
            else
            {
                excel.UseExcel(false);
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            int totalDrawings = 0;
            Ticket mega = new MegaMillions();
            mega.DrawRandomNumbers();

            if (printInfo)
            {
                mega.PrintNumbers();
                Console.Write($"Count of matching Numbers: {mega.GetMatchingNumbersCount(winningMega)}\n");
            }

            totalDrawings++;
            bool saved = false;
            while (!mega.Equal(winningMega) && (totalDrawings < numOfDraws || numOfDraws == -1))
            {
                if (NeedToCancelSimulation())
                {
                    break;
                }

                SaveIfNeeded(numOfDraws);

                if (NeedToPauseSimulation(saved, numOfDraws))
                {
                    if (SimulationVarManager.printCurrentSimInfo)
                    {
                        PrintCurrentSimulationInfo(totalDrawings, totalDrawings * 2, numOfDraws);
                        SimulationVarManager.printCurrentSimInfo = false;
                    }
                    saved = true;
                    continue;
                }
                else
                {
                    saved = false;
                }

                mega.DrawRandomNumbers();

                int matchingNums = mega.GetMatchingNumbersCount(winningMega);

                // Infinitly drawing will not write to excel
                if (numOfDraws != -1)
                {
                    excel.WriteToExcel(excelRows[matchingNums]++, matchingNums + 1, ArrayToString(mega.numbers));

                    if (excelRows[matchingNums] == Excel.MAX_ROWS - 24000)
                    {
                        excel.Save();
                        Console.WriteLine("\nReached maximum excel row size");
                        break;
                    }
                }
                if (printInfo)
                {
                    mega.PrintNumbers();
                    Console.Write($"Count of matching Numbers: {matchingNums}\n");
                }
                totalDrawings++;
            }

            Console.WriteLine($"\nTotal number of drawings: {totalDrawings}. Total Cost: ${totalDrawings * mega.cost}");

            stopWatch.Stop();
            long duration = stopWatch.ElapsedMilliseconds;
            Console.WriteLine($"This simulation took {duration} Miliseconds");

            if (numOfDraws != -1)
            {
                excel.Save();
            }
            SimulationFinished();
        }

        public void SimulatePower(int[] winningPower, int numOfDraws, bool printInfo)
        {
            int[] excelRows = new int[6];
            if (numOfDraws != -1)
            {
                excel.UseExcel(true);
                excel.SetPath(excelDirPath);
                excel.CreateNewExcelFile("Powerball");

                excel.WriteToExcel(2, 8, ArrayToString(winningPower)); // Save the winning numbers at the last Column
                for (int i = 0; i < excelRows.Length; i++)
                {
                    excelRows[i] = 2;
                }
            }
            else
            {
                excel.UseExcel(false);
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            int totalDrawings = 0;
            Ticket power = new Powerball();
            power.DrawRandomNumbers();
            if (printInfo)
            {
                power.PrintNumbers();
                Console.Write($"Count of matching Numbers: {power.GetMatchingNumbersCount(winningPower)}\n");
            }
            totalDrawings++;

            bool saved = false;
            while (!power.Equal(winningPower) && (totalDrawings < numOfDraws || numOfDraws == -1))
            {
                if (NeedToCancelSimulation())
                {
                    break;
                }

                SaveIfNeeded(numOfDraws);

                if (NeedToPauseSimulation(saved, numOfDraws))
                {
                    if (SimulationVarManager.printCurrentSimInfo)
                    {
                        PrintCurrentSimulationInfo(totalDrawings, totalDrawings * 2, numOfDraws);
                        SimulationVarManager.printCurrentSimInfo = false;
                    }

                    saved = true;
                    continue;
                }
                else
                {
                    saved = false;
                }

                power.DrawRandomNumbers();

                int matchingNums = power.GetMatchingNumbersCount(winningPower);

                // Infinitly drawing will not write to excel
                if (numOfDraws != -1)
                {
                    excel.WriteToExcel(excelRows[matchingNums]++, matchingNums + 1, ArrayToString(power.numbers));

                    if (excelRows[matchingNums] == Excel.MAX_ROWS - 24000)
                    {
                        excel.Save();
                        Console.WriteLine("\nReached maximum excel row size");
                        break;
                    }
                }

                if (printInfo)
                {
                    power.PrintNumbers();
                    Console.Write($"Count of matching Numbers: {power.GetMatchingNumbersCount(winningPower)}\n");
                }
                totalDrawings++;
            }

            Console.WriteLine($"\nTotal number of drawings: {totalDrawings}. Total Cost: ${totalDrawings * power.cost}");

            stopWatch.Stop();
            long duration = stopWatch.ElapsedMilliseconds;
            Console.WriteLine($"This simulation took {duration} Miliseconds");

            if (numOfDraws != -1)
            {
                excel.Save();
            }
            SimulationFinished();
        }

        public void SimulateSuper(int[] winningSuper, int numOfDraws, bool printInfo)
        {
            int[] excelRows = new int[6];
            if (numOfDraws != -1)
            {
                excel.UseExcel(true);
                excel.SetPath(excelDirPath);
                excel.CreateNewExcelFile("SuperLotto");

                excel.WriteToExcel(2, 8, ArrayToString(winningSuper)); // Save the winning numbers at the last Column
                for (int i = 0; i < excelRows.Length; i++)
                {
                    excelRows[i] = 2;
                }
            }
            else
            {
                excel.UseExcel(false);
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            int totalDrawings = 0;
            Ticket super = new SuperLotto();
            super.DrawRandomNumbers();

            if (printInfo)
            {
                super.PrintNumbers();
                Console.Write($"Count of matching Numbers: {super.GetMatchingNumbersCount(winningSuper)}\n");
            }

            totalDrawings++;
            bool saved = false; // Save only once when needed.
            while (!super.Equal(winningSuper) && (totalDrawings < numOfDraws || numOfDraws == -1))
            {
                if (NeedToCancelSimulation())
                {
                    break;
                }

                SaveIfNeeded(numOfDraws);

                if (NeedToPauseSimulation(saved, numOfDraws))
                {
                    if (SimulationVarManager.printCurrentSimInfo)
                    {
                        PrintCurrentSimulationInfo(totalDrawings, totalDrawings, numOfDraws);
                        SimulationVarManager.printCurrentSimInfo = false;
                    }
                    saved = true;
                    continue;
                }
                else
                {
                    saved = false;
                }

                super.DrawRandomNumbers();

                int matchingNums = super.GetMatchingNumbersCount(winningSuper);

                // Infinitly drawing will not write to excel
                if (numOfDraws != -1)
                {
                    excel.WriteToExcel(excelRows[matchingNums]++, matchingNums + 1, ArrayToString(super.numbers));

                    if (excelRows[matchingNums] == Excel.MAX_ROWS - 24000)
                    {
                        excel.Save();
                        Console.WriteLine("\nReached maximum excel row size");
                        break;
                    }
                }

                if (printInfo)
                {
                    super.PrintNumbers();
                    Console.Write($"Count of matching Numbers: {super.GetMatchingNumbersCount(winningSuper)}\n");
                }
                totalDrawings++;
            }

            Console.WriteLine($"\nTotal number of drawings: {totalDrawings}. Total Cost: ${totalDrawings * super.cost}");

            stopWatch.Stop();
            long duration = stopWatch.ElapsedMilliseconds;
            Console.WriteLine($"This simulation took {duration} Miliseconds");

            if (numOfDraws != -1)
            {
                excel.Save();
            }
            SimulationFinished();
        }

        // Saves the data if the simulation pauses
        private bool NeedToPauseSimulation(bool saved, int numOfDraws)
        {
            if (!SimulationVarManager.runSimulation)
            {
                if (!saved && numOfDraws != -1)
                {
                    excel.Save();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /** Utility Methods **/
        private bool NeedToCancelSimulation()
        {
            return !SimulationVarManager.simulationInProgress;
        }

        private void SimulationFinished()
        {
            SimulationVarManager.simulationInProgress = false;
            Console.WriteLine("Would you like to run a new simulation?");
            NewSimulationMenu();
        }

        private void SaveIfNeeded(int numOfDraws)
        {
            if (SimulationVarManager.saveSimulation && numOfDraws != -1)
            {
                excel.Save();
                SimulationVarManager.saveSimulation = false;
            }
        }

        private string ArrayToString(int[] arr)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (int num in arr)
            {
                stringBuilder.Append($"{num} ");
            }

            return stringBuilder.ToString();
        }

        private void PrintCurrentSimulationInfo(int totalDrawings, int cost, int numOfDraws)
        {
            Console.WriteLine($"\nTotal Number of draws so far: {totalDrawings}\nCost so far: ${cost}\nNumber of draws remaining {numOfDraws}");
        }

        /** CONSOLE METHODS BELOW **/
        public void NewSimulationMenu()
        {
            Console.WriteLine("------------------------MENU------------------------");
            Console.WriteLine("0: Displays Menu");
            Console.WriteLine("1: Simulate Mega Millions drawings");
            Console.WriteLine("2: Simulate Powerball drawings");
            Console.WriteLine("3: Simulate SuperLotto drawings");
            Console.WriteLine("9: Exit");
            Console.WriteLine("----------------------------------------------------");

            var KP = Console.ReadKey();

            if (KP.Key == ConsoleKey.NumPad0)
            {
                NewSimulationMenu();
            }
            else if (KP.Key == ConsoleKey.NumPad1)
            {
                int numOfDrawings = GetNumberOfDrawingsNeeded();
                bool printExtraInfo = UserWantsExtraInfo();

                this.currentLotteryName = "Mega Millions";
                SimulationVarManager.runSimulation = true;
                SimulationVarManager.simulationInProgress = true;
                StartListeningForCommands();
                SimulateMega(WINNING_MEGA, numOfDrawings, printExtraInfo);
            }
            else if (KP.Key == ConsoleKey.NumPad2)
            {
                int numOfDrawings = GetNumberOfDrawingsNeeded();
                bool printExtraInfo = UserWantsExtraInfo();

                this.currentLotteryName = "Powerball";
                SimulationVarManager.runSimulation = true;
                SimulationVarManager.simulationInProgress = true;
                StartListeningForCommands();
                SimulatePower(WINNING_POWER, numOfDrawings, printExtraInfo);
            }
            else if (KP.Key == ConsoleKey.NumPad3)
            {
                int numOfDrawings = GetNumberOfDrawingsNeeded();
                bool printExtraInfo = UserWantsExtraInfo();

                this.currentLotteryName = "SuperLotto";
                SimulationVarManager.runSimulation = true;
                SimulationVarManager.simulationInProgress = true;
                StartListeningForCommands();
                SimulateSuper(WINNING_SUPER, numOfDrawings, printExtraInfo);
            }
            else if (KP.Key == ConsoleKey.NumPad9)
            {
                System.Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid option. Please rethink your life before choosing again");
                NewSimulationMenu();
            }
        }

        private int GetNumberOfDrawingsNeeded()
        {
            Console.WriteLine("Choose how many drawings you would like from the range of (1 - 1,000,000)");
            Console.WriteLine("Enter -1 to simulate an infinite amount of drawings until it is the jackpot. *(No Excel file output)*");

            while (true)
            {
                try
                {
                    int userNum = Int32.Parse(Console.ReadLine());

                    if ((userNum >= 1 && userNum <= 1000000) || userNum == -1)
                    {
                        return userNum;
                    }

                    Console.WriteLine("Please enter an integer from (1 - 1,000,000)");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter an integer from (1 - 1,000,000)");
                }
            }
        }
        private void StartListeningForCommands()
        {
            worker = new BackgroundWorker { };
            worker.DoWork += ProcessCommands;
            worker.RunWorkerAsync();
        }

        // Will process console commands in a background thread
        private void ProcessCommands(object sender, EventArgs e)
        {
            while (SimulationVarManager.simulationInProgress)
            {
                var KP = Console.ReadKey();

                // Since the ReadKey can leave a gap in time, we need to check again if the worker needs to be cancelled
                if (!SimulationVarManager.simulationInProgress)
                {
                    break;
                }

                if (KP.Key == ConsoleKey.NumPad0)
                {
                    Console.WriteLine("------------------------MENU------------------------");
                    Console.WriteLine("0: Displays Menu");
                    Console.WriteLine("1: Pauses Simulation");
                    Console.WriteLine("2: Continues Simulation if it is turned off");
                    Console.WriteLine("3: Save");
                    Console.WriteLine("4: Print Information (Only can be used when the simulation is paused)");
                    Console.WriteLine("5: New Simulation");
                    Console.WriteLine("----------------------------------------------------");
                }
                else if (KP.Key == ConsoleKey.NumPad1)
                {
                    SimulationVarManager.runSimulation = false;
                    Console.WriteLine("Simulation Paused!");
                }
                else if (KP.Key == ConsoleKey.NumPad2 && !SimulationVarManager.runSimulation)
                {
                    SimulationVarManager.runSimulation = true;
                    Console.WriteLine("Simulation Unpaused!");
                }
                else if (KP.Key == ConsoleKey.NumPad3)
                {
                    SimulationVarManager.saveSimulation = true;
                    Console.WriteLine("Simulation Saved!");
                }
                else if (KP.Key == ConsoleKey.NumPad4 && !SimulationVarManager.runSimulation)
                {
                    SimulationVarManager.printCurrentSimInfo = true;
                }
                else if (KP.Key == ConsoleKey.NumPad5)
                {
                    // Make a new simulation here...
                    SimulationVarManager.runSimulation = false;
                    SimulationVarManager.simulationInProgress = false;
                    excel.UseExcel(false);
                    Console.WriteLine("Cancelling current simulation");
                }
                else
                {
                    Console.WriteLine("Invalid menu option, press '0' for the menu and please don't make the same mistake again :(");
                }
            }
        }

        private bool UserWantsExtraInfo()
        {
            Console.WriteLine("Do you want the simulation to print the information along the way? (Y/N)");
            Console.WriteLine("* SAYING YES WILL DRASTICALLY INCREASE THE TIME TO FINISH THE SIMULATION *");

            
            while (true)
            {
                string input = Console.ReadLine().ToString();

                if (input.ToLower().Equals("y"))
                {
                    return true;
                }
                else if (input.ToLower().Equals("n"))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Please only enter a 'Y' for yes or 'N' for no.");
                }
            }
        }
    }
}
