using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;


namespace CMP1903_A2_2324
{
    internal class Testing
    {
        //Properties
        private int _dieRoll;
        private int _diceSum;

        //Methods

        //method to write a message to a log file if there is an error
        public void DebugMessage(string message)
        {
            //creates a logfile if one does not exist, otherwise it appends to it
            string filePath = @"\DebugLog";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("At time: "+DateTime.Now+" Error Received: "+ message);
            }
        }

        //tests the individual die rolls, if they are within the range of 1-6
        public void TestDieRoll()
        {
            Die die = new Die();
            _dieRoll = die.Roll();
            try
            {
                if (!(_dieRoll >= 1 && _dieRoll <= 6))
                {
                    throw new Exception("Invalid die roll, out of range");
                }
            }
            catch (Exception ex)
            {
                DebugMessage(ex.Message);
                Debug.Assert(_dieRoll >= 1 && _dieRoll <= 6, "Invalid die roll, out of range");
            }
        }

        //tests the sevensout diceTotal method, should be within 2 - 12 (as its 2 die being rolled)
        public void TestSevensOut()
        {
            SevensOut sevensOut = new SevensOut();
            _diceSum = sevensOut.DiceTotal();
            try
            {
                if (!(_diceSum >= 2 && _diceSum <= 12))
                {
                    throw new Exception("Invalid dice sum of SevensOut class, out of range");
                }
            }
            catch (Exception ex)
            {
                DebugMessage(ex.Message);
                Debug.Assert(_diceSum >= 2 && _diceSum <= 12, "Invalid die sum of SevensOut class, out of range");
            }
        }

        //tests the threeormore diceTotal method, returned values should either be 0,3,6 or 12. no other value should be accepted
        public void TestThreeOrMore()
        {
            ThreeOrMore threeOrMore = new ThreeOrMore();
            _diceSum = threeOrMore.DiceTotal();
            try
            {
                if (!(_diceSum == 0 || _diceSum == 3 || _diceSum == 6 || _diceSum == 12))
                {
                    throw new Exception("Invalid dice sum of ThreeorMore class, out of range");
                }
            }
            catch (Exception ex)
            {
                DebugMessage(ex.Message);
                Debug.Assert(_diceSum == 0 || _diceSum == 3 || _diceSum == 6 || _diceSum == 12, "Invalid die sum of SevensOut class, out of range");
            }
        }
    }
}