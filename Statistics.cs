using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A2_2324
{
    internal class Statistics
    {

        //Properties
        private int _highScore = 0; 
        private int _playerwins = 0;
        private int _counter = 0;
        private List<int> _meanScore = new List<int>();

        //Methods

        //Takes in the score as the input and if its higher than the current high score it replaces it
        public void UpdateHighScore(int score)
        {
            if (score > _highScore)
            {
                _highScore = score;
            }
        }

        //takes in score as the input and adds it to a list to be used to calculate the mean score
        public void UpdateMeanScore(int score)
        {
            _meanScore.Add(score);
        }

        //takes in the win value and adds it to _playerwins
        public void UpdatePlayerwins(int wins)
        {
            _playerwins += wins;
        }

        //increments the counter for how many times the user has played the game
        public void UpdatePlayCounter() 
        {
            _counter++;
        }

        //runs all of the previous methods, mainly for the program class to look more tidy
        public void UpdateAll(int score, int wins)
        {
            UpdateHighScore(score);
            UpdateMeanScore(score);
            UpdatePlayerwins(wins);
            UpdatePlayCounter();
        }

        //displays all the statistics, as well as calculating the average of the score (if there are scores to be calculated)
        public void DisplayStatistics()
        {
            Console.WriteLine($"High Score: {_highScore}");
            Console.WriteLine($"Times Played: {_counter}");
            Console.WriteLine($"Times Won: {_playerwins}");
            if ( _meanScore.Count > 0 )
            {
                Console.WriteLine("Average Score: "+_meanScore.Average());
            }
        }
    }
}