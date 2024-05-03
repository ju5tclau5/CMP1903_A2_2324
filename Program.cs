using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A2_2324
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Create all objects that will be used
            SevensOut sevensOut = new SevensOut();
            ThreeOrMore threeOrMore = new ThreeOrMore();
            //seperate score stats for the different games and players (sv = SevensOut, tm = ThreeorMore)
            Statistics svPlayer1Statistics = new Statistics();
            Statistics tmPlayer1Statistics = new Statistics();
            Statistics svPlayer2Statistics = new Statistics();
            Statistics tmPlayer2Statistics = new Statistics();
            Testing testing = new Testing();

            //Run continuously until program is exited, allows the user to choose from a menu with an input on what they want to do, i.e play SevensOut or Run Tests
            while (true)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("1. Play Sevens Out");
                Console.WriteLine("2. Player Three or More");
                Console.WriteLine("3. Check Statistics");
                Console.WriteLine("4. Run Tests");
                Console.WriteLine("5. Exit");
                Console.WriteLine("---------------------------");

                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    //Sevens out game, stats are updated at the end
                    case "1":
                        Console.Clear();
                        sevensOut.StartGame();
                        int player1Score = sevensOut.GetScore(1);
                        int player2Score = sevensOut.GetScore(2);
                        int player1Wins = sevensOut.GetWins(1);
                        int player2Wins = sevensOut.GetWins(2);
                        svPlayer1Statistics.UpdateAll(player1Score, player1Wins);
                        svPlayer2Statistics.UpdateAll(player2Score, player2Wins);
                        break;
                    //Three or More game, stats are updated at the end
                    case "2":
                        Console.Clear();
                        threeOrMore.StartGame();
                        player1Score = threeOrMore.GetScore(1);
                        player2Score = threeOrMore.GetScore(2);
                        player1Wins = threeOrMore.GetWins(1);
                        player2Wins = threeOrMore.GetWins(2);
                        tmPlayer1Statistics.UpdateAll(player1Score, player1Wins);
                        tmPlayer2Statistics.UpdateAll(player2Score, player2Wins);
                        break;
                    //Check stats, choose either SevensOut or ThreeorMore
                    case "3":
                        Console.Clear();
                        Console.WriteLine("1. Sevens Out Stats");
                        Console.WriteLine("2. Three or More Stats");
                        Console.WriteLine("3. Go Back");

                        userChoice = Console.ReadLine();

                        switch (userChoice)
                        {
                            case "1":
                                Console.Clear();
                                Console.WriteLine("Player 1 Statistics:");
                                svPlayer1Statistics.DisplayStatistics();
                                Console.WriteLine("Player 2 Statistics:");
                                svPlayer2Statistics.DisplayStatistics();
                                break;

                            case "2":
                                Console.Clear();
                                Console.WriteLine("Player 1 Statistics:");
                                tmPlayer1Statistics.DisplayStatistics();
                                Console.WriteLine("Player 2 Statistics:");
                                tmPlayer2Statistics.DisplayStatistics();
                                break;
                            //exits the stats menu
                            case "3":
                                Console.Clear();
                                break;
                            //exception handling
                            default:
                                Console.WriteLine("Invalid selection, returning to the main menu.");
                                break;
                        }
                        break;
                    //Runs multiple tests, for more redunancy
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Running tests...");
                        testing.TestDieRoll();
                        testing.TestDieRoll();
                        testing.TestDieRoll();
                        testing.TestSevensOut();
                        testing.TestSevensOut();
                        testing.TestSevensOut();
                        testing.TestThreeOrMore();
                        testing.TestThreeOrMore();
                        testing.TestThreeOrMore();
                        Console.Clear();
                        Console.WriteLine("All tests ran successfully... Enter any button to continue");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    //exits the application
                    case "5":
                        return;
                    //exception handling
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid selection, please try again.");
                        break;
                }
            }

        }
    }
}
