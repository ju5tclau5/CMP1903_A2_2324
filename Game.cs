using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A2_2324
{
    internal interface IGame
    {
        protected void RollDice();
        protected int DiceTotal();
        protected void StartGame();
        protected int GetScore(int player);
        protected int GetWins(int player);  
    }

    internal class SevensOut : IGame
    {

        private int _dievalue1;
        private int _dievalue2;
        private int _dieTotal;
        private int _player1Score;
        private int _player2Score;
        private int _player1Wins;
        private int _player2Wins;

        public void RollDice()
        {
            Die die1 = new Die();
            Die die2 = new Die();
            _dievalue1 = die1.Roll();
            _dievalue2 = die2.Roll();

        }

        public int DiceTotal()
        {
            RollDice();
            _dieTotal = _dievalue1 + _dievalue2;
            return _dieTotal;
        }

        public void StartGame()
        {
            _player1Score = 0;
            _player2Score = 0;
            _player1Wins = 0;
            _player2Wins = 0;
            _dieTotal = 0;

            while (true)
            {
                DiceTotal();
                if (_dieTotal == 7)
                {
                    _player1Score += _dieTotal;
                    Console.WriteLine($"Player 1 rolled a 7! End of game score: Player 1 = {_player1Score}, Player 2 = {_player2Score}");
                    _player1Wins++;
                    break;
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

                Console.WriteLine("End of player 1's turn, enter any button to continue");
                Console.ReadLine();
                Console.Clear();

                DiceTotal();
                if (_dieTotal == 7)
                {
                    _player2Score += _dieTotal;
                    Console.WriteLine($"Player 2 rolled a 7! End of game score: Player 1 = {_player1Score}, Player 2 = {_player2Score}");
                    _player2Wins++;
                    break;
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

                Console.WriteLine("End of player 1's turn, enter any button to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }

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
    internal class ThreeOrMore : IGame
    {

        private int[] _dieValues = new int[5];
        private int _dieTotal;
        private int _player1Score;
        private int _player2Score;
        private int _player1Wins;
        private int _player2Wins;
        bool pairChecked = false;


        public void RollDice()
        {
            Die die = new Die();
            for (int i = 0; i < 5; i++)
            {
                _dieValues[i] = die.Roll();
            }
        }

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

        public void StartGame()
        {
            _player1Score = 0;
            _player2Score = 0;
            _player1Wins = 0;
            _player2Wins = 0;
            _dieTotal = 0;

            while (true) 
            {
                DiceTotal();
                pairChecked = false;
                _player1Score = _player1Score + _dieTotal;
                if (_player1Score >= 20)
                {
                    Console.WriteLine("Player 1 Wins! They rolled: "+ string.Join(",", _dieValues));
                    _player1Wins++;
                    break;
                }
                else
                {
                    Console.WriteLine("Player 1 rolled: "+ string.Join(",", _dieValues)+$". Their total is {_player1Score}.");
                }

                Console.WriteLine("End of player 1's turn, enter any button to continue");
                Console.ReadLine();
                Console.Clear();

                DiceTotal();
                pairChecked = false;
                _player2Score = _player2Score + _dieTotal;
                if (_player2Score >= 20)
                {
                    Console.WriteLine("Player 2 Wins! They rolled: "+ string.Join(",", _dieValues));
                    _player2Wins++;
                    break;
                }
                else
                {
                    Console.WriteLine("Player 2 rolled: " + string.Join(",", _dieValues) +$". Their total is {_player2Score}.");
                }

                Console.WriteLine("End of player 2's turn, enter any button to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }

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