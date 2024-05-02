using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMP1903_A2_2324
{
    //interface holds the methods to be inherited and to be used by children classes
    internal interface IGame
    {
        protected void RollDice(); //this method should roll and assign die values
        protected int DiceTotal(); //this method should assign a score or a total to the die values
        protected void StartGame(); //this method should run through the game that the user will play
        protected int GetScore(int player); //this method should get the score of each player at the end of the game
        protected int GetWins(int player);  //this method should see which player won at the end of the game
    }


    //Rules: 	Roll the two dice, noting the total rolled each time
    //          If it is a 7 - stop
    //          If it is any other number - add it to your total
    //          If it is a double - add double the total to your score (3,3 would add 12 to your total)
    internal class SevensOut : IGame
    {
        //Properties
        private int _dievalue1;
        private int _dievalue2;
        private int _dieTotal;
        private int _player1Score;
        private int _player2Score;
        private int _player1Wins;
        private int _player2Wins;

        //Methods

        //Creates die object and assigns a random die value to _dieValue 1 and _dieValue2
        public void RollDice()
        {
            Die die = new Die();

            _dievalue1 = die.Roll();
            _dievalue2 = die.Roll();

        }

        //Calls the previous method, sums the value of the 2 die values and assigns it to _dieTotal, returns _dieTotal
        public int DiceTotal()
        {
            RollDice();
            _dieTotal = _dievalue1 + _dievalue2;
            return _dieTotal;
        }

        //Runs through the game once, according to the rules
        public void StartGame()
        {
            _player1Score = 0;
            _player2Score = 0;
            _player1Wins = 0;
            _player2Wins = 0;
            _dieTotal = 0;

            while (true)
            {

                //player 1 side
                DiceTotal();
                if (_dieTotal == 7)
                {
                    _player1Score += _dieTotal;
                    Console.WriteLine($"Player 1 rolled a 7! End of game score: Player 1 = {_player1Score}, Player 2 = {_player2Score}");
                    _player1Wins++;
                    break; //exit condition
                }
                else if (_dievalue1 == _dievalue2)
                {
                    _dieTotal = _dieTotal * 2;
                    _player1Score += _dieTotal;
                    Console.WriteLine($"Player 1 rolled {_dievalue1} & {_dievalue2}, their total score is {_player1Score}");
                }
                else
                {
                    _player1Score += _dieTotal;
                    Console.WriteLine($"Player 1 rolled {_dievalue1} & {_dievalue2}, their total score is {_player1Score}");
                }

                //added for the user's conviencece to show them when the turn ends and that they can choose when to continue
                Console.WriteLine("End of player 1's turn, enter any button to continue");
                Console.ReadLine();
                Console.Clear();

                //player 2 side
                DiceTotal();
                if (_dieTotal == 7)
                {
                    _player2Score += _dieTotal;
                    Console.WriteLine($"Player 2 rolled a 7! End of game score: Player 1 = {_player1Score}, Player 2 = {_player2Score}");
                    _player2Wins++;
                    break; //exit condition
                }
                else if (_dievalue1 == _dievalue2)
                {
                    _dieTotal = _dieTotal * 2;
                    _player2Score += _dieTotal;
                    Console.WriteLine($"Player 2 rolled {_dievalue1} & {_dievalue2}, their total score is {_player2Score}");
                }
                else
                {
                    _player2Score += _dieTotal;
                    Console.WriteLine($"Player 2 rolled {_dievalue1} & {_dievalue2}, their total score is {_player2Score}");
                }

                //added for the user's conviencece to show them when the turn ends and that they can choose when to continue
                Console.WriteLine("End of player 1's turn, enter any button to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }

        //Gets the end of game score of either player 1 or 2, depending on the input, and returns it. To be used for the statistics class
        public int GetScore(int player)
        {
            if (player == 1)
            {
                return _player1Score;
            }
            else
            {
                return _player2Score;
            }
        }
             
        //Sees which player won the game, depending on the input, and returns it. To be used for the statistics class
        public int GetWins(int player)
        {
            if (player == 1)
            {
                return _player1Wins;
            }

            else
            {
                return _player2Wins;
            }
        }
    }

    //Rules:
	//Roll all 5 dice hoping for a 3-of-a-kind or better.
    //If 2-of-a-kind is rolled, player may choose to rethrow all, or the remaining dice.
	//3-of-a-kind: 3 points
	//4-of-a-kind: 6 points
	//5-of-a-kind: 12 points
    //First to a total of 20.
    internal class ThreeOrMore : IGame
    {

        //Properties

        private int[] _dieValues = new int[5];
        private int _dieTotal;
        private int _player1Score;
        private int _player2Score;
        private int _player1Wins;
        private int _player2Wins;
        bool pairChecked = false;

        //Methods

        //Creates a die object and assigns 5 random die values to an array called _dieValues
        public void RollDice()
        {
            Die die = new Die();
            for (int i = 0; i < 5; i++)
            {
                _dieValues[i] = die.Roll();
            }
        }

        //Calls the previous method, uses Linq GroupBy to group numbers in the array that are the same, returns a value depending on how large those groups are,
        //i.e - 3 of the same == 3, 4 of the same == 6 and 5 of the same == 12 
        public int DiceTotal()
        {
            RollDice();

            var groups = _dieValues.GroupBy(x => x);
            
            _dieTotal = 0;

            foreach (var group in groups)
            {
                int dieCount = group.Count();
                if (dieCount == 3)
                {
                    Console.WriteLine("Rolled a triple, you scored 3 points!");
                    _dieTotal = 3;
                    return _dieTotal;
                }
                else if (dieCount == 4) 
                {
                    Console.WriteLine("Rolled a quadruple, you scored 6 points!");
                    _dieTotal = 6;
                    return _dieTotal;
                }
                else if (dieCount == 5)
                {
                    Console.WriteLine("Rolled a quintuple, you scored 12 points!");
                    _dieTotal = 12;
                    return _dieTotal;
                }

                else if (dieCount == 2 && !pairChecked) 
                {
                    Console.WriteLine("Rolled a pair, rerolling...");
                    RollDice();
                    pairChecked = true;
                    return DiceTotal();

                }
            }
            return _dieTotal;
        }

        //Runs the game through once, according the rules
        public void StartGame()
        {
            _player1Score = 0;
            _player2Score = 0;
            _player1Wins = 0;
            _player2Wins = 0;
            _dieTotal = 0;

            while (true) 
            {

                //player 1 side
                DiceTotal();
                pairChecked = false;
                _player1Score = _player1Score + _dieTotal;
                if (_player1Score >= 20)
                {
                    Console.WriteLine("Player 1 Wins! They rolled: "+ string.Join(",", _dieValues));
                    _player1Wins++;
                    break; //exit condition
                }
                else
                {
                    Console.WriteLine("Player 1 rolled: "+ string.Join(",", _dieValues)+$". Their total is {_player1Score}.");
                }

                //added for the user's conviencece to show them when the turn ends and that they can choose when to continue
                Console.WriteLine("End of player 1's turn, enter any button to continue");
                Console.ReadLine();
                Console.Clear();

                //player 2 side
                DiceTotal();
                pairChecked = false;
                _player2Score = _player2Score + _dieTotal;
                if (_player2Score >= 20)
                {
                    Console.WriteLine("Player 2 Wins! They rolled: "+ string.Join(",", _dieValues));
                    _player2Wins++;
                    break; //exit condition
                }
                else
                {
                    Console.WriteLine("Player 2 rolled: " + string.Join(",", _dieValues) +$". Their total is {_player2Score}.");
                }

                //added for the user's conviencece to show them when the turn ends and that they can choose when to continue
                Console.WriteLine("End of player 2's turn, enter any button to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }

        //Gets the end of game score of either player 1 or 2, depending on the input, and returns it. To be used for the statistics class
        public int GetScore(int player)
        {
            if (player == 1) 
            {
                return _player1Score;
            }

            else
            {
                return _player2Score;
            }
        }
 
        //Sees which player won the game, depending on the input, and returns it. To be used for the statistics class
        public int GetWins(int player)
        {
            if (player == 1)
            {
                return _player1Wins;
            }

            else
            {
                return _player2Wins;
            }
        }
    }
}