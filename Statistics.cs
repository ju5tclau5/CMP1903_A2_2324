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

        private int _highScore = 0; 
        private int _playerwins = 0;
        private int _counter = 0;
        private List<int> _meanScore = new List<int>();

        public void UpdateHighScore(int score)
        {
            if (score > _highScore)
            {
                _highScore = score;
            }
        }

        public void UpdateMeanScore(int score)
        {
            _meanScore.Add(score);
        }

        public void UpdatePlayerwins(int wins)
        {
            _playerwins += wins;
        }

        public void UpdatePlayCounter() 
        {
            _counter++;
        }

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