using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A2_2324
{
    internal class Die
    {
        //Properties
        private static Random _random = new Random();
        private int _dieValue;

        //Method
        public int Roll()
        {
            _dieValue = _random.Next(1,7);
            return _dieValue;
        }
    }
}
