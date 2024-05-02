using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A2_2324
{
    internal class Testing
    {

        private int _dieRoll;
        private int _diceSum;

        public void TestDieRoll()
        {
            Die die = new Die();
            _dieRoll = die.Roll();

            Debug.Assert(_dieRoll >= 1 && _dieRoll <= 6, "Invalid die roll, out of range");
        }

        public void TestSevensOut()
        {
            SevensOut sevensOut = new SevensOut();
            _diceSum = sevensOut.DiceTotal();

            Debug.Assert(_diceSum >= 2 && _diceSum <= 12, "Invalid dice sum, out of range");
        }

        public void TestThreeOrMore()
        {
            ThreeOrMore threeOrMore = new ThreeOrMore();
            _diceSum = threeOrMore.DiceTotal();

            Debug.Assert(_diceSum == 0 || _diceSum == 3 || _diceSum == 6 || _diceSum == 12, "Invalid dice sum, out of range");
        }
    }
}
