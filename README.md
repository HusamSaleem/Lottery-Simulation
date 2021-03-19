# Lottery-Simulation

# Description
-> Will help a person see just how statistically low the chance of winning the jackpot lottery

# Features
-> Currently implemented three lottery tickets {Mega Millions, Powerball, SuperLotto}
-> Basically this program has two options for the user. You can either start simulating an infinite amount of 
   randomized lottery tickets to see how many draws, and total cost will take to win the jackpot. (WILL NOT WRITE TO EXCEL FILE)
-> Other option is that you can simulate a finite amount (1 - 1,000,000) randomized lottery tickets and this 
   will write to an excel file.
   -> The excel file will include 7 columns. The first 6 columns are for lottery tickets that matched an "n" amount of numbers
      The last column is the winning numbers.

# Requirements
-> Have Microsoft Excel installed on your computer so you wil be able to see the data from the finite amount of drawings. 
-> Have a full directory path of where you want the Excel files to be stored at

# Example to run this simulation
-> Simulation simulation = new Simulation(@"YOUR EXCEL DIRECTORY FULL PATH GOES HERE (WHATEVER PATH YOU WANT)");
   simulation.Start();

# Known Issues
-> There is a bug when entering a command when a simulation ends. The Console.ReadKey() will not stop until it has been given an input and so
   this causes the user to have to press the key twice before the program responds.
