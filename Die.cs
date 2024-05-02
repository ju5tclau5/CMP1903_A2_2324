using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A2_2324
{
    internal class Die

    //this code is from my github repository (https://github.com/ju5tclau5/CMP1903_A1_2324/blob/master/CMP1903_A1_2324/Die.cs)

    {
        //Properties
        private static Random _random = new Random(); //holds a random number from the random class
        private int _dieValue; // holds the rolled die value

        //Method

        //rolls a random number and assigns it to _dieValue
        public int Roll() 
        {
            _dieValue = _random.Next(1,7); //rolls a random number between 1,7 including 1
            return _dieValue;              //and assigns it to _dieValue
        }
    }
}
