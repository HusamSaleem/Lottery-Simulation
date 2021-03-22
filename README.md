# Lottery-Simulation

# Description
- Will help a person see just how statistically low the chance of winning the jackpot lottery

# Features
- Currently implemented three lottery tickets {Mega Millions, Powerball, SuperLotto}
- Basically this program has two options. You can either start simulating an infinite amount of 
   randomized lottery tickets to see how many draws, and total cost it will take to win the jackpot. (WILL NOT WRITE TO EXCEL FILE)
- Other option is that you can simulate a finite amount (1 - 1,000,000) randomized lottery tickets and this 
   will write to an excel file.
- The excel file will include 7 columns. The first 6 columns are for lottery tickets that matched an "n" amount of numbers
      The last column is the winning numbers.
      
# Good To know things
- Pressing '1' During a simulation will pause it
- Pressing '2' During a simulation will unpause it
- Pressing '3' During a simulation will Save the simulation (Best used when simulation is paused
- Pressing '4' Will print some additional information about the simulation. (*Only works when paused*)
- Pressing '5' will pause, save if possible and start a new simulation of your choosing

# Requirements
- Have Microsoft Excel installed on your computer so you wil be able to see the data from the finite amount of drawings. 
- Have a full directory path of where you want the Excel files to be stored at

# Example to run this simulation
- Simulation simulation = new Simulation(@"YOUR EXCEL DIRECTORY FULL PATH GOES HERE (WHATEVER PATH YOU WANT)");
   simulation.Start();

# Known Issues
- There is a bug when entering a command when a simulation ends. The Console.ReadKey() will not stop until it has been given an input and so
   this causes the user to have to press the key twice before the program responds.
