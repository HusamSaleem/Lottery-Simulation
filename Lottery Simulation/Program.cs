using System;
using System.ComponentModel;
using System.Threading;


/**
 * Bugs
 * -> There is a bug when entering a command when a simulation ends, since there will be two menus trying to listen for a command
 */
namespace Lottery_Simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Simulation simulation = new Simulation(@"YOUR EXCEL DIRECTORY PATH");
            simulation.Start();
        }
    }
}
